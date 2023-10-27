using System;
using System.Linq;
using Examples.Editor.Core;
using Examples.Editor.Names;
using Examples.Editor.Windows;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Examples.Editor.Datas
{
    /// <summary> OdinMenuItem - 創建 Data 頁面 </summary>
    [Serializable]
    public class CreateDataPanel
    {
    #region ========== [Private Variables] ==========

        [ValidateInput(nameof(CanCreate), "@message", InfoMessageType.Warning , ContinuousValidationCheck = true)]
        [LabelText("新建名稱: ")]
        [LabelWidth(60)]
        [SerializeField]
        private string newDataName;

        private OdinMenuTree tree;
        private string       titleName;
        private string       message;

        private DataManager DataManager => DataManager.Window;

    #endregion

    #region ========== [Constructor] ==========

        public CreateDataPanel(OdinMenuTree tree, string titleName)
        {
            this.tree      = tree;
            this.titleName = titleName;
        }

    #endregion

    #region ========== [Private Methods] ==========

        [ShowIf(nameof(CanCreate), false)]
        [Button]
        private void CreateNew()
        {
            // 建立 EditorData，並加到 tree 菜單中
            var editorData = new EditorReferenceData(newDataName);
            var resultName = $"{titleName}/{editorData.DataName}";
            tree.Add(resultName, editorData, SdfIconType.JournalPlus);
            DataManager.editorDatas.Add(editorData);

            // 把新建的資料，加進List (SetDirty)
            DataManager.characterDataContainer.Add(editorData.characterData);
            DataManager.exteriorDataContainer.Add(editorData.exteriorData);
            DataManager.SetDatasDirty();
            EditorSaveSystem.SaveFile.SetDisplayName(editorData.characterData.DataID, newDataName);
            EditorSaveSystem.Save();
            newDataName = string.Empty;
        }

        private bool CanCreate()
        {
            if (!string.IsNullOrEmpty(newDataName))
                return !IsSameDataName(newDataName);
            message = EditorWindowDescription.StringEmpty;
            return false;
        }

        private bool IsSameDataName(string dataName)
        {
            var editorData = DataManager.editorDatas.FirstOrDefault(data => string.Equals(data.DataName, dataName));
            if (editorData == null) return false;
            message = $"{EditorWindowDescription.DataIsExist} , Name: {dataName} , editorData:{editorData.DataName}";
            return true;
        }

    #endregion
    }
}