using System;
using System.Linq;
using Examples.Editor.Core;
using Examples.Editor.Names;
using Examples.Editor.Windows;
using Examples.Scripts.Datas;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Examples.Editor.Datas
{
    /// <summary> For Editor Window 使用 , 非真實資料 <br/>
    /// 這邊可以設置 Odin 的各種 Attribute , 與真實資料脫鉤 <br/>
    /// </summary>
    [Serializable]
    public class EditorReferenceData
    {
    #region ========== [Public Variables] ==========

        [PropertyOrder(10)]
        [FoldoutGroup("角色資料")]
        [HideLabel]
        public CharacterData ARealData;

        [PropertyOrder(10)]
        [FoldoutGroup("其他資料")]
        [HideLabel]
        public ExteriorData BRealData;

        public string DataName => dataName;

        public string ReferenceDataID => ARealData?.DataID;

    #endregion

    #region ========== [Private Variables] ==========

        [HorizontalGroup]
        [ValidateInput(nameof(IsNameExist), "@message", InfoMessageType.Warning)]
        [PropertyOrder(1)]
        [LabelText("顯示名稱: ")]
        [LabelWidth(60)]
        [EnableIf(nameof(enableChangeDataName))]
        [SerializeField]
        private string dataName;

        private string message;
        private string tempDataName;
        private bool   enableChangeDataName;

        private Action<string> OnChangeNameAction => DataManager.Window.OnMenuNameChanged;

    #endregion

    #region ========== [Constructor] ==========

        /// <summary> 新建立 </summary>
        public EditorReferenceData()
        {
            ARealData = new CharacterData();
            BRealData = new ExteriorData(ARealData.DataID);
        }

        /// <summary> 新建立 (DataName) </summary>
        public EditorReferenceData(string dataName)
        {
            this.dataName = dataName;
            ARealData     = new CharacterData(dataName);
            BRealData     = new ExteriorData(ARealData.DataID);
        }

        /// <summary> 直接 References (Editor ~ Real Data) </summary>
        /// <param name="aRealData"> 參考目標 </param>
        public EditorReferenceData(CharacterData aRealData)
        {
            ARealData = aRealData;
            BRealData = new ExteriorData(null);
        }

    #endregion

    #region ========== [Public Methods] ==========

        public void SetDataName(string newDataName)
        {
            dataName = newDataName;
        }

    #endregion

    #region ========== [Private Methods] ==========

        [EnableIf(nameof(IsNameExist), false)]
        [ShowIf(nameof(enableChangeDataName))]
        [PropertyOrder(0)]
        [HorizontalGroup(width: 30)]
        [GUIColor(1f, 1.5f, 1f)]
        [Button(SdfIconType.FileEarmarkDiff, "")]
        private void ChangeDataName()
        {
            if (!tempDataName.Equals(DataName))
            {
                OnChangeNameAction?.Invoke(DataName);
                EditorSaveSystem.SaveFile.SetDisplayName(ARealData.DataID, DataName);
                EditorSaveSystem.Save();
            }

            enableChangeDataName = false;
            tempDataName         = string.Empty;
        }

        [HideIf(nameof(enableChangeDataName))]
        [PropertyOrder(0)]
        [HorizontalGroup(width: 30)]
        [GUIColor(1f, 1.5f, 1.5f)]
        [Button(SdfIconType.FileEarmarkCheck, "")]
        private void EnableChangeDataName()
        {
            enableChangeDataName = true;
            tempDataName         = DataName;
        }

        private bool IsNameExist()
        {
            if (!string.IsNullOrEmpty(DataName))
                return !IsSameDataName(DataName);
            message = EditorWindowDescription.StringEmpty;
            return false;
        }

        private bool IsNotReferenceDataID()
        {
            var dataID = BRealData.DataID;
            if (string.IsNullOrEmpty(dataID)) return true;
            return !dataID.Equals(ReferenceDataID);
        }

        private bool IsSameDataName(string dataName)
        {
            var editorDatas = DataManager.Window.editorDatas;
            var editorData = editorDatas.FirstOrDefault(data => data != this && string.Equals(data.DataName, dataName));
            if (editorData == null) return false;
            message = $"{EditorWindowDescription.DataIsExist} , Name: {dataName} , editorData:{editorData.DataName}";
            return true;
        }

        [ShowIf(nameof(IsNotReferenceDataID))]
        [Button]
        private void Set_BDataID()
        {
            BRealData = new ExteriorData(ReferenceDataID);
            DataManager.Window.exteriorDataContainer.Datas.Add(BRealData);
        }

    #endregion
    }
}