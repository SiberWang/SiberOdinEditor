using System;
using System.Linq;
using Examples.Editor.Core;
using Examples.Editor.Names;
using Examples.Editor.Windows;
using SiberOdinEditor.Tools;
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

        [ValidateInput(nameof(IsNameExist), ContinuousValidationCheck = true)]
        [LabelText("新建名稱: ")]
        [LabelWidth(60)]
        [SerializeField]
        private string newDataName;

        private OdinMenuTree tree;
        private string       titleName;
        private bool         canClick_CreateNew;

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

        [ShowIf(nameof(canClick_CreateNew))]
        [GUIColor(0.7f, 1.2f, 0.7f)]
        [Button("建立資料", ButtonSizes.Large)]
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

            DataManager.ShowCustomNotification("Create Succeed!", Color.cyan, SdfIconType.HandThumbsUp);
        }

        private bool IsNameExist(string currentName, ref string errorMessage, ref InfoMessageType? messageType)
        {
            if (!string.IsNullOrEmpty(currentName))
            {
                var editorDatas = DataManager.editorDatas;
                var editorData  = editorDatas.FirstOrDefault(data => string.Equals(data.DataName, currentName));
                if (editorData != null)
                {
                    errorMessage = $"{EditorWindowDescription.DataIsExist} (FindName: {editorData.DataName})";
                    messageType             = InfoMessageType.Warning;
                    canClick_CreateNew = false;
                }
                else
                {
                    errorMessage            = EditorWindowDescription.DataCanUse;
                    messageType             = InfoMessageType.Info;
                    canClick_CreateNew = true;
                }
            }
            else
            {
                errorMessage            = EditorWindowDescription.StringEmpty;
                messageType             = InfoMessageType.Error;
                canClick_CreateNew = false;
            }
            return false;
        }

    #endregion
    }
}