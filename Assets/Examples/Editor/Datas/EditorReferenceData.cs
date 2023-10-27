using System;
using System.Linq;
using Examples.Editor.Core;
using Examples.Editor.Names;
using Examples.Editor.Windows;
using Examples.Scripts.Datas;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Examples.Editor.Datas
{
    /// <summary> For Editor Window 使用 , 非真實資料 <br/>
    /// 這邊可以設置 Odin 的各種 Attribute , 與真實資料脫鉤 <br/>
    /// </summary>
    [Serializable]
    public class EditorReferenceData
    {
    #region ========== [Public Variables] ==========

        [DisableIf(nameof(enableChangeDataName))]
        [FormerlySerializedAs("ARealData")]
        [PropertyOrder(10)]
        [FoldoutGroup("角色資料")]
        [HideLabel]
        public CharacterData characterData;

        [DisableIf(nameof(enableChangeDataName))]
        [FormerlySerializedAs("BRealData")]
        [PropertyOrder(10)]
        [FoldoutGroup("其他資料")]
        [HideLabel]
        public ExteriorData exteriorData;

        public string DataName => dataName;

        public string ReferenceDataID => characterData?.DataID;

    #endregion

    #region ========== [Private Variables] ==========

        [GUIColor("GetButtonColor")]
        [HorizontalGroup]
        [ValidateInput(nameof(IsNameExist), ContinuousValidationCheck = true)]
        [PropertyOrder(1)]
        [LabelText("顯示名稱: ")]
        [LabelWidth(60)]
        [EnableIf(nameof(enableChangeDataName))]
        [SerializeField]
        private string dataName;

        private string tempDataName;
        private bool   enableChangeDataName;
        private bool   canClick_ChangeDataName;

        private DataManager    DataManager        => DataManager.Window;
        private Action<string> OnChangeNameAction => DataManager.OnMenuNameChanged;

    #endregion

    #region ========== [Constructor] ==========

        /// <summary> 新建立 </summary>
        public EditorReferenceData()
        {
            characterData = new CharacterData();
            exteriorData  = new ExteriorData(characterData.DataID);
        }

        /// <summary> 新建立 (DataName) </summary>
        public EditorReferenceData(string dataName)
        {
            this.dataName = dataName;
            characterData = new CharacterData(dataName);
            exteriorData  = new ExteriorData(characterData.DataID);
        }

        /// <summary> 直接 References (Editor ~ Real Data) </summary>
        /// <param name="characterData"> 參考目標 </param>
        public EditorReferenceData(CharacterData characterData)
        {
            this.characterData = characterData;
            exteriorData       = new ExteriorData(null);
        }

    #endregion

    #region ========== [Public Methods] ==========

        public void SetDataName(string newDataName)
        {
            dataName = newDataName;
        }

    #endregion

    #region ========== [Private Methods] ==========

        [ShowIf(nameof(enableChangeDataName))]
        [EnableIf(nameof(canClick_ChangeDataName))]
        [PropertyOrder(0)]
        [HorizontalGroup(width: 25)]
        [GUIColor(1.5f, 1f, 1f)]
        [Button(SdfIconType.Hexagon, "")]
        private void Button_ChangeDataName() // 改名，確認！
        {
            if (!tempDataName.Equals(DataName))
            {
                OnChangeNameAction?.Invoke(DataName);
                EditorSaveSystem.SaveFile.SetDisplayName(characterData.DataID, DataName);
                EditorSaveSystem.Save();
            }

            enableChangeDataName = false;
            tempDataName         = string.Empty;
        }

        [HideIf(nameof(enableChangeDataName))]
        [PropertyOrder(0)]
        [HorizontalGroup(width: 25)]
        [GUIColor(1f, 1f, 1f)]
        [Button(SdfIconType.HexagonFill, "")]
        private void Button_EnableChangeDataName() // 改名，啟動！
        {
            enableChangeDataName = true;
            tempDataName         = DataName;
        }

        [PropertyOrder(1)]
        [OnInspectorGUI]
        private void OnInspectorGUI()
        {
            GUILayout.Space(-6);
            GUILayout.Label("", GUI.skin.horizontalSlider);
            GUILayout.Space(10);
        }

        private bool IsNameExist(string currentName, ref string errorMessage, ref InfoMessageType? messageType)
        {
            if (!string.IsNullOrEmpty(currentName))
            {
                var editorDatas = DataManager.editorDatas;
                var editorData =
                    editorDatas.FirstOrDefault(data => data != this && string.Equals(data.DataName, currentName));
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

            return !enableChangeDataName;
        }

        private bool IsNotReferenceDataID()
        {
            var dataID = exteriorData.DataID;
            if (string.IsNullOrEmpty(dataID)) return true;
            if (!dataID.Equals(ReferenceDataID)) return true;
            return false;
        }

        [ShowIf(nameof(IsNotReferenceDataID))]
        [Button("設定參考 DataID")]
        private void Set_BDataID()
        {
            exteriorData = new ExteriorData(ReferenceDataID);
            DataManager.exteriorDataContainer.Datas.Add(exteriorData);
        }

        private Color GetButtonColor()
        {
            return !enableChangeDataName ? Color.white : Color.yellow;
        }

    #endregion
    }
}