using System.Collections.Generic;
using System.Linq;
using Examples.Editor.Core;
using Examples.Editor.Datas;
using Examples.Editor.Names;
using Examples.Scripts.Core;
using Examples.Scripts.OdinWindows.Tools;
using SiberOdinEditor.Core;
using SiberOdinEditor.Tools;
using SiberUtility.Systems.FileSaves;
using SiberUtility.Tools;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Examples.Editor.Windows
{
    public class Example_EditorWindow : OdinMenuEditorWindow
    {
    #region ========== [Private Variables] ==========

        public static Example_EditorWindow Window;

        private EditorSaveFile oldSaveFile;

        public static Example_RealSOData_A     ADataSoData; // 示範主要檔案 (ex : RobotDataContainer)
        public static Example_RealSOData_B     BDataSoData;
        public static List<Example_EditorData> EditorDatas;

    #endregion

    #region ========== [Events] ==========

        protected override void OnGUI()
        {
            base.OnGUI();

            // Ctrl+S 快捷鍵 (儲存)
            EditorHotKeys.CtrlS(() => Debug.Log($"Ctrl+S"));
            if (!GUIHelper.CurrentWindowHasFocus)
                EditorHotKeys.Init();
        }
        
        protected override void Initialize()
        {
            // Some Setting
            SaveHelper.EnableLog = false;

            // Main Get
            ADataSoData = EditorHelper.GetScriptableObject<Example_RealSOData_A>();
            BDataSoData = EditorHelper.GetScriptableObject<Example_RealSOData_B>();
            EditorDatas = new List<Example_EditorData>();

            // 初始化就讀取檔案
            EditorSaveSystem.Load();
            oldSaveFile = EditorSaveSystem.SaveFile;
            CalibrationDataFile();
        }

        // TODO:比對清單項目 (紀錄 比對 現有清單)
        // TODO:要刪除的對象會是 Json 裡面的，因為 List是我隨時在調整的對象
        /// <summary> 校準資料 (Editor ~ Real)</summary>
        private void CalibrationDataFile()
        {
            var newSaveFile = new EditorSaveFile();
            var subDatas    = ADataSoData.Datas;
            foreach (var editorDisplayName in oldSaveFile.assets)
            {
                var subData = subDatas.FirstOrDefault(data => data.DataID.Equals(editorDisplayName.SearchID));
                if (subData != null) newSaveFile.SetDisplayName(subData.DataID, editorDisplayName.Name);
            }

            EditorSaveSystem.SaveFile = newSaveFile;
            EditorSaveSystem.Save();
        }

        protected override void OnBeginDrawEditors()
        {
            if (Window == null)
                Window = GetWindow<Example_EditorWindow>();
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

    #endregion

    #region ========== [Protected Methods] ==========

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.DefaultMenuStyle = OdinMenuStyle.TreeViewStyle;

            AddTitleEditorData(tree);
            AddEditorDatas(tree);
            tree.SortMenuItemsByName();
            return tree;
        }

    #endregion

    #region ========== [Private Methods] ==========

        private bool IsShowDelete(OdinMenuItem selected)
        {
            if (selected == null) return false;
            if (selected.Value is Example_EditorData) return true;
            return false;
        }

        private void DoDeleteAction(OdinMenuItem selected)
        {
            if (selected.Value is Example_EditorData editorData)
            {
                ADataSoData.Remove(editorData.ARealData);
                BDataSoData.Remove(editorData.BRealData);
            }

            selected.Remove();
            EditorUtility.SetDirty(ADataSoData);
            EditorUtility.SetDirty(BDataSoData);
        }

        private void AddTitleEditorData(OdinMenuTree tree)
        {
            var titleName = Example_EditorNames.TitleName_SubDatas;
            var data      = new Example_CreateData(tree, titleName);
            tree.Add(titleName, data, SdfIconType.Ticket);
        }

        private void AddEditorDatas(OdinMenuTree tree)
        {
            // Child
            // 檔案建立
            foreach (var aRealData in ADataSoData.Datas)
            {
                var editorData  = new Example_EditorData(aRealData); // 雙向 References
                var displayName = oldSaveFile.GetEditorDisplayName(aRealData.DataID);
                editorData.DataName = displayName ?? $"{aRealData.Name}";
                editorData.SetChangeNameAction(OnMenuNameChanged);
                var resultName = $"{Example_EditorNames.TitleName_SubDatas}/{editorData.DataName}";
                tree.Add(resultName, editorData, SdfIconType.JournalPlus);
                EditorDatas.Add(editorData);
            }

            foreach (var bRealData in BDataSoData.Datas)
            {
                var editorData = EditorDatas.FirstOrDefault(s => s.ReferenceDataID.Equals(bRealData.DataID));
                if (editorData == null) continue;
                editorData.BRealData = bRealData;
            }
        }

        public void OnMenuNameChanged(string newName)
        {
            var odinMenuItem = MenuTree?.Selection?.FirstOrDefault();
            if (odinMenuItem != null)
                odinMenuItem.Name = newName;
            MenuTree.SortMenuItemsByName();
        }

        [MenuItem(MenuHotKeys.ExampleEditorWindow)]
        private static void OpenEditor()
        {
            Window = Window.OpenWindow<Example_EditorWindow>();

            // TODO: 可以秀出東西
            if (Window != null)
            {
                var context    = $"欸好酷喔！\n第一次用這個\n先放在這邊試試看\n之後可以用，很讚\n水喔~";
                var texture2D  = OdinStyleTools.GetSdfIcon(SdfIconType.Twitch, Color.green, 50);
                var guiContent = new GUIContent(context, texture2D, "What can i do?");
                Window.ShowNotification(guiContent, 1f);
            }
        }

    #endregion
    }
}