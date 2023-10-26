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
    /// <summary> 資料編輯器 </summary>
    public class DataManager : OdinMenuEditorWindow
    {
    #region ========== [Public Variables] ==========

        public static DataManager Window;

        public CharacterDataContainer    characterDataContainer;
        public ExteriorDataContainer     exteriorDataContainer;
        public List<EditorReferenceData> editorDatas;

    #endregion

    #region ========== [Private Variables] ==========

        private EditorSaveFile saveFile;

    #endregion

    #region ========== [Odin & Editor] ==========

        [MenuItem(MenuHotKeys.ExampleEditorWindow)]
        private static void OpenEditor()
        {
            Window = Window.OpenWindow<DataManager>();
        }

        /// <summary> 初始化 (Once) </summary>
        protected override void Initialize()
        {
            // Some Setting
            SaveHelper.EnableLog = false;

            // MainData Get
            characterDataContainer = EditorHelper.GetScriptableObject<CharacterDataContainer>();
            exteriorDataContainer  = EditorHelper.GetScriptableObject<ExteriorDataContainer>();
            Assert.IsNotNull(characterDataContainer, $"{nameof(characterDataContainer)} == null");
            Assert.IsNotNull(exteriorDataContainer, $"{nameof(exteriorDataContainer)} == null");
            editorDatas = new List<EditorReferenceData>();
            EditorSaveSystem.Load(); // 初始化就讀取檔案
            saveFile = EditorSaveSystem.SaveFile;
            CalibrationDataFile();   // 校準資料
        }

        protected override void OnBeginDrawEditors()
        {
            Window ??= GetWindow<DataManager>();
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
            // 這個會讓整個按鈕小小的
            // tree.DefaultMenuStyle = OdinMenuStyle.TreeViewStyle;

            AddTitleEditorData(tree);
            AddEditorDatas(tree);
            tree.SortMenuItemsByName();
            return tree;
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
            // 編輯檔案建立
            foreach (var characterData in characterDataContainer.Datas)
            {
                // 雙向 References
                var dataName   = saveFile.GetEditorDisplayName(characterData.DataID);
                var editorData = new EditorReferenceData(characterData);
                editorData.SetDataName(dataName ?? $"{characterData.Name}");
                var resultName = $"{MenuItemNames.TitleName_SubDatas}/{editorData.DataName}";
                tree.Add(resultName, editorData, SdfIconType.JournalPlus);
                editorDatas.Add(editorData);
            }

            foreach (var bRealData in exteriorDataContainer.Datas)
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

        /// <summary> 校準資料 (EditorData ← RealData) <br/>
        /// 依據真實資料，去做Editor紀錄調整 <br/>
        /// ---(自動覆蓋存檔)---
        /// </summary>
        private void CalibrationDataFile()
        {
            var newSaveFile = new EditorSaveFile();
            var subDatas    = characterDataContainer.Datas;
            foreach (var editorDisplayName in saveFile.assets)
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
                characterDataContainer.Remove(editorData.ARealData);
                exteriorDataContainer.Remove(editorData.BRealData);
                editorDatas.Remove(editorData);
                EditorUtility.SetDirty(characterDataContainer);
                EditorUtility.SetDirty(exteriorDataContainer);
            }

            selected.Remove();
            Window.ShowCustomNotification("Succeed Deleted!", Color.green, SdfIconType.Trash);
        }

        private bool IsShowDelete(OdinMenuItem selected)
        {
            if (selected == null) return false;
            if (selected.Value is not EditorReferenceData) return false;
            return true;
        }

    #endregion
    }
}