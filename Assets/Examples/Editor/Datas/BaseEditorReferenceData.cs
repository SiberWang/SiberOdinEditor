using System.Collections.Generic;
using System.Linq;
using Examples.Editor.Core;
using Examples.Editor.Names;
using Examples.Editor.Windows;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Examples.Editor.Datas
{
    /// <summary> 跟 Reference Data 有關的基底 <br/>
    /// </summary>
    public abstract class BaseEditorReferenceData
    {
    #region ========== [Reference Variables] ==========

        public abstract    string                        MainReferenceDataID { get; }
        protected abstract List<BaseEditorReferenceData> EditorDatas         { get; }

    #endregion

    #region ========== [Private Variables] ==========

        private string tempDataName;
        private bool   canClick_ChangeDataName;

    #endregion

    #region ========== [Public Variables] ==========

        public string DataName => dataName;

    #endregion

    #region ========== [Protected Variables] ==========

        [GUIColor("GetButtonColor")]
        [HorizontalGroup]
        [ValidateInput(nameof(IsNameExist), ContinuousValidationCheck = true)]
        [PropertyOrder(1)]
        [LabelText("顯示名稱: ")]
        [LabelWidth(60)]
        [EnableIf(nameof(EnableChangeDataName))]
        [SerializeField]
        protected string dataName;

        protected bool EnableChangeDataName;

    #endregion

    #region ========== [Public Methods] ==========

        public void SetDataName(string newDataName)
        {
            dataName = newDataName;
        }

    #endregion

    #region ========== [Private Methods] ==========

        private bool IsNameExist(string currentName, ref string errorMessage, ref InfoMessageType? messageType)
        {
            if (!string.IsNullOrEmpty(currentName))
            {
                var editorData =
                    EditorDatas.FirstOrDefault(data => data != this && string.Equals(data.DataName, currentName));
                if (editorData != null)
                {
                    errorMessage = $"{EditorWindowDescription.DataIsExist} (FindName: {editorData.DataName})";
                    messageType = InfoMessageType.Warning;
                    canClick_ChangeDataName = false;
                }
                else
                {
                    errorMessage            = EditorWindowDescription.DataCanUse;
                    messageType             = InfoMessageType.Info;
                    canClick_ChangeDataName = true;
                }
            }
            else
            {
                errorMessage            = EditorWindowDescription.StringEmpty;
                messageType             = InfoMessageType.Error;
                canClick_ChangeDataName = false;
            }

            return !EnableChangeDataName;
        }

        [ShowIf(nameof(EnableChangeDataName))]
        [EnableIf(nameof(canClick_ChangeDataName))]
        [PropertyOrder(0)]
        [HorizontalGroup(width: 25)]
        [GUIColor(1.5f, 1f, 1f)]
        [Button(SdfIconType.Hexagon, "")]
        private void Button_ChangeDataName() // 改名，確認！
        {
            if (!tempDataName.Equals(DataName))
            {
                DataManager.Window.SetMenuName(DataName);
                EditorSaveSystem.SaveFile.SetDisplayName(MainReferenceDataID, DataName);
                EditorSaveSystem.Save();
            }

            EnableChangeDataName = false;
            tempDataName         = string.Empty;
        }

        [HideIf(nameof(EnableChangeDataName))]
        [PropertyOrder(0)]
        [HorizontalGroup(width: 25)]
        [GUIColor(1f, 1f, 1f)]
        [Button(SdfIconType.HexagonFill, "")]
        private void Button_EnableChangeDataName() // 改名，啟動！
        {
            EnableChangeDataName = true;
            tempDataName         = DataName;
        }

        private Color GetButtonColor()
        {
            return !EnableChangeDataName ? Color.white : Color.yellow;
        }

    #endregion

    #region ========== [Editor] ==========

        [PropertyOrder(1)]
        [OnInspectorGUI]
        private void OnInspectorGUI()
        {
            GUILayout.Space(-6);
            GUILayout.Label("", GUI.skin.horizontalSlider);
            GUILayout.Space(10);
        }

    #endregion
    }
}