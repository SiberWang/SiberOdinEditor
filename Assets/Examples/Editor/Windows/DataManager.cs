using System.Collections.Generic;
using System.Linq;
using Examples.Editor.Core;
using Examples.Editor.Datas;
using Examples.Editor.Names;
using Examples.Scripts.Datas;
using SiberOdinEditor.Core;
using SiberOdinEditor.Tools;
using SiberUtility.Systems.FileSaves;
using SiberUtility.Tools;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace Examples.Editor.Windows
{
    public class DataManager : OdinMenuEditorWindow
    {
    #region ========== [Public Variables] ==========

        public static DataManager Window;

        public CharacterContainer        characterContainer; // 示範主要檔案 (ex : RobotDataContainer)
        public ExteriorContainer         exteriorContainer;
        public List<EditorReferenceData> editorDatas;

    #endregion

    #region ========== [Private Variables] ==========

        private EditorSaveFile oldSaveFile;

    #endregion

    #region ========== [Odin & Editor] ==========

        protected override void OnBeginDrawEditors()
        {
            if (Window == null)
                Window = GetWindow<DataManager>();
            var selectedItem  = MenuTree.Selection.FirstOrDefault();
            var toolbarHeight = MenuTree.Config.SearchToolbarHeight;
            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                OdinDrawTools.Draw_Label_WindowSize(Window.position);
                OdinDrawTools.Draw_Label_CurrentSelectName(selectedItem);
                OdinDrawTools.Draw_Button_DeleteCurrentObject(IsShowDelete(selectedItem),
                                                              () => DoDeleteAction(selectedItem));
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.DefaultMenuStyle = OdinMenuStyle.TreeViewStyle;

            AddTitleEditorData(tree);
            AddEditorDatas(tree);
            tree.SortMenuItemsByName();
            return tree;
        }

        /// <summary> 初始化 (Once) </summary>
        protected override void Initialize()
        {
            // Some Setting
            SaveHelper.EnableLog = false;

            // Main Datas Get
            characterContainer = EditorHelper.GetScriptableObject<CharacterContainer>();
            exteriorContainer  = EditorHelper.GetScriptableObject<ExteriorContainer>();
            Assert.IsNotNull(characterContainer, $"{nameof(characterContainer)} == null");
            Assert.IsNotNull(exteriorContainer, $"{nameof(exteriorContainer)} == null");
            editorDatas = new List<EditorReferenceData>();

            // 初始化就讀取檔案
            EditorSaveSystem.Load();
            oldSaveFile = EditorSaveSystem.SaveFile;
            CalibrationDataFile();
        }
        
        protected override void OnGUI()
        {
            base.OnGUI();

            // Ctrl+S 快捷鍵 (儲存)
            EditorHotKeys.CtrlS(() => Debug.Log($"Ctrl+S"));
            if (Window != null && !GUIHelper.CurrentWindowHasFocus)
                EditorHotKeys.Init();
        }

    #endregion
        
    #region ========== [Events] ==========
        public void OnMenuNameChanged(string newName)
        {
            var odinMenuItem = MenuTree?.Selection?.FirstOrDefault();
            if (odinMenuItem != null)
                odinMenuItem.Name = newName;
            MenuTree.SortMenuItemsByName();
        }

    #endregion

    #region ========== [Private Methods] ==========

        private void AddEditorDatas(OdinMenuTree tree)
        {
            // Child
            // 檔案建立
            foreach (var aRealData in characterContainer.Datas)
            {
                var editorData  = new EditorReferenceData(aRealData); // 雙向 References
                var displayName = oldSaveFile.GetEditorDisplayName(aRealData.DataID);
                editorData.DataName = displayName ?? $"{aRealData.Name}";
                var resultName = $"{MenuItemNames.TitleName_SubDatas}/{editorData.DataName}";
                tree.Add(resultName, editorData, SdfIconType.JournalPlus);
                editorDatas.Add(editorData);
            }

            foreach (var bRealData in exteriorContainer.Datas)
            {
                var editorData = editorDatas.FirstOrDefault(s => s.ReferenceDataID.Equals(bRealData.DataID));
                if (editorData == null) continue;
                editorData.BRealData = bRealData;
            }
        }

        private void AddTitleEditorData(OdinMenuTree tree)
        {
            var titleName = MenuItemNames.TitleName_SubDatas;
            var data      = new CreateDataPanel(tree, titleName);
            tree.Add(titleName, data, SdfIconType.Ticket);
        }

        // TODO:要刪除的對象會是 Json 裡面的，因為 List是我隨時在調整的對象
        /// <summary> 校準資料 (EditorData ← RealData) <br/>
        /// 依據真實資料，去做Editor紀錄調整 <br/>
        /// ---(自動覆蓋存檔)---
        /// </summary>
        private void CalibrationDataFile()
        {
            var newSaveFile = new EditorSaveFile();
            var subDatas    = characterContainer.Datas;
            foreach (var editorDisplayName in oldSaveFile.assets)
            {
                var subData = subDatas.FirstOrDefault(data => data.DataID.Equals(editorDisplayName.SearchID));
                if (subData != null) newSaveFile.SetDisplayName(subData.DataID, editorDisplayName.Name);
            }

            EditorSaveSystem.Save(newSaveFile);
        }

        private void DoDeleteAction(OdinMenuItem selected)
        {
            if (selected.Value is EditorReferenceData editorData)
            {
                characterContainer.Remove(editorData.ARealData);
                exteriorContainer.Remove(editorData.BRealData);
                EditorUtility.SetDirty(characterContainer);
                EditorUtility.SetDirty(exteriorContainer);
            }

            selected.Remove();
        }

        private bool IsShowDelete(OdinMenuItem selected)
        {
            if (selected == null) return false;
            if (selected.Value is EditorReferenceData) return true;
            return false;
        }

        [MenuItem(MenuHotKeys.ExampleEditorWindow)]
        private static void OpenEditor()
        {
            Window = Window.OpenWindow<DataManager>();
        }

    #endregion
    }
}