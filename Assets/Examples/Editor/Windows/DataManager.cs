using System.Linq;
using Examples.Editor.Core;
using Examples.Editor.Datas;
using SiberOdinEditor.Core;
using SiberOdinEditor.Tools;
using SiberUtility.Systems.FileSaves;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;

namespace Examples.Editor.Windows
{
    /// <summary> 資料編輯器 </summary>
    public class DataManager : OdinMenuEditorWindow
    {
    #region ========== [Public Variables] ==========

        public static DataManager Window;

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
            // 每次 Initialize 防止 Static Window 跑掉
            if (Window == null) OpenEditor();
            // Setting
            EditorRepository.Init();
            SaveHelper.EnableLog = false;
            EditorSaveSystem.Load(); // 初始化就讀取檔案
            CalibrationDataFile();   // 校準資料
        }

        protected override void OnBeginDrawEditors()
        {
            var selectedItem  = MenuTree.Selection.FirstOrDefault();
            var toolbarHeight = MenuTree.Config.SearchToolbarHeight;
            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);
            {
                OdinDrawTools.Draw_Label_WindowSize(Window.position);
                OdinDrawTools.Draw_Label_CurrentSelectName(selectedItem);
                OdinDrawTools.Draw_Button_DeleteCurrentObject(IsShowDelete(selectedItem),
                                                              () => DoDeleteAction(selectedItem));
                OdinDrawTools.Draw_Button_AllDataSave(EditorRepository.SetAllDataDirty);
            }
            SirenixEditorGUI.EndHorizontalToolbar();
        }

        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.SetCustomMenuItems(); // 分成另一包
            return tree;
        }

        protected override void OnGUI()
        {
            base.OnGUI();

            // Ctrl+S 快捷鍵 (儲存)
            EditorHotKeys.CtrlS(() => OdinDrawTools.DoAllDataSave(EditorRepository.SetAllDataDirty));
            if (Window != null && !GUIHelper.CurrentWindowHasFocus)
                EditorHotKeys.Init();
        }

    #endregion

    #region ========== [Public Methods] ==========

        public void SetMenuName(string newName)
        {
            var odinMenuItem = MenuTree?.Selection?.FirstOrDefault();
            if (odinMenuItem != null)
                odinMenuItem.Name = newName;
        }

    #endregion

    #region ========== [Private Methods] ==========

        /// <summary> 校準資料 (EditorData ← RealData) <br/>
        /// 依據真實資料，重新去做 Editor名稱紀錄 <br/>
        /// ---(自動覆蓋存檔)---
        /// </summary>
        private void CalibrationDataFile()
        {
            var newSaveFile    = new EditorSaveFile();
            var infoDataList   = EditorSaveSystem.SaveFile.infoDataList;
            var characterDatas = EditorRepository.CharacterDataContainer.Datas;
            var weaponDatas    = EditorRepository.WeaponDataContainer.Datas;

            foreach (var infoData in infoDataList)
            {
                var searchID = infoData.SearchID;
                var dataName = infoData.Name;

                var characterData = characterDatas.FirstOrDefault(data => data.DataID.Equals(searchID));
                if (characterData != null)
                    newSaveFile.SetDisplayName(characterData.DataID, dataName);

                var weaponData = weaponDatas.FirstOrDefault(data => data.DataID.Equals(searchID));
                if (weaponData != null)
                    newSaveFile.SetDisplayName(weaponData.DataID, dataName);
            }

            EditorSaveSystem.Save(newSaveFile);
        }

        private void DoDeleteAction(OdinMenuItem selected)
        {
            if (selected == null) return;
            if (selected.Value is EditorReferenceData_Character editorData)
                EditorRepository.DeleteCharacterData(editorData);

            if (selected.Value is EditorReferenceData_Weapon weapon)
                EditorRepository.DeleteWeaponData(weapon);

            selected.Remove();
            EditorRepository.SetAllDataDirty();
            CalibrationDataFile();
        }

        private bool IsShowDelete(OdinMenuItem selected)
        {
            if (selected?.Value is BaseEditorReferenceData) return true;
            return false;
        }

    #endregion
    }
}