using System.Collections.Generic;
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
    /// <summary> 跟 Create Data 有關的基底 <br/>
    /// </summary>
    public abstract class BaseEditorCreateData
    {
    #region ========== [Protected Variables] ==========

        [ValidateInput(nameof(IsNameExist), ContinuousValidationCheck = true)]
        [LabelText("新建名稱: ")]
        [LabelWidth(60)]
        [SerializeField]
        protected string newDataName;
        protected string titleName;
        protected OdinMenuTree tree;

        protected abstract List<BaseEditorReferenceData> EditorDatas { get; }

    #endregion

    #region ========== [Private Variables] ==========

        private bool canClick_CreateData;

    #endregion

    #region ========== [Constructor] ==========

        protected BaseEditorCreateData(OdinMenuTree tree, string titleName)
        {
            this.tree      = tree;
            this.titleName = titleName;
        }

    #endregion

    #region ========== [Protected Methods] ==========

        [ShowIf(nameof(canClick_CreateData))]
        [GUIColor(0.7f, 1.2f, 0.7f)]
        [Button("建立資料", ButtonSizes.Large)]
        protected abstract void Create();
        
        protected void SaveNewJson(BaseEditorReferenceData editorData)
        {
            // 加進 RealData , 並儲存 Json Editor的顯示名稱
            EditorRepository.SetAllDataDirty();
            EditorSaveSystem.SaveFile.SetDisplayName(editorData.MainReferenceDataID, newDataName);
            EditorSaveSystem.Save();
            DataManager.Window.ShowCustomNotification("Create Succeed!", Color.cyan, SdfIconType.HandThumbsUp);
            newDataName = string.Empty;
        }

    #endregion

    #region ========== [Private Methods] ==========

        private bool IsNameExist(string currentName, ref string errorMessage, ref InfoMessageType? messageType)
        {
            if (!string.IsNullOrEmpty(currentName))
            {
                var editorData = EditorDatas.FirstOrDefault(data => string.Equals(data.DataName, currentName));
                if (editorData != null)
                {
                    errorMessage       = $"{EditorWindowDescription.DataIsExist} (FindName: {editorData.DataName})";
                    messageType        = InfoMessageType.Warning;
                    canClick_CreateData = false;
                }
                else
                {
                    errorMessage       = EditorWindowDescription.DataCanUse;
                    messageType        = InfoMessageType.Info;
                    canClick_CreateData = true;
                }
            }
            else
            {
                errorMessage       = EditorWindowDescription.StringEmpty;
                messageType        = InfoMessageType.Error;
                canClick_CreateData = false;
            }

            return false;
        }

    #endregion
    }
}