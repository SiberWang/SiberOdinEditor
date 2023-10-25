using System;
using System.Linq;
using Examples.Editor.Core;
using Examples.Editor.Names;
using Examples.Editor.Windows;
using Examples.Scripts;
using Examples.Scripts.Core;
using Sirenix.OdinInspector;

namespace Examples.Editor.Datas
{
    /// <summary> For Editor Window 使用 , 非真實資料 </summary>
    public class Example_EditorData
    {
    #region ========== [Public Variables] ==========

        [HorizontalGroup]
        [ValidateInput(nameof(IsNameExist), "@message", InfoMessageType.Warning)]
        [PropertyOrder(1)]
        [LabelText("顯示名稱: ")]
        [LabelWidth(60)]
        [EnableIf(nameof(enableChangeDataName))]
        public string DataName;

        [PropertyOrder(10)]
        [FoldoutGroup("角色資料")]
        [HideLabel]
        public ARealData ARealData;

        [PropertyOrder(10)]
        [FoldoutGroup("其他資料")]
        [HideLabel]
        public BRealData BRealData;

        public string ReferenceDataID => ARealData?.DataID;

        private Action<string> onChangeNameAction;

        private string message;
        private bool   enableChangeDataName;
        private string tempDataName;

    #endregion

    #region ========== [Constructor] ==========

        /// <summary> 新建立 </summary>
        public Example_EditorData()
        {
            ARealData = new ARealData();
            BRealData = new BRealData(ARealData.DataID);
        }

        /// <summary> 新建立 (DataName) </summary>
        public Example_EditorData(string dataName)
        {
            DataName  = dataName;
            ARealData = new ARealData(DataName);
            BRealData = new BRealData(ARealData.DataID);
        }

        /// <summary> 直接 References (Editor ~ Real Data) </summary>
        /// <param name="aRealData"> 參考目標 </param>
        public Example_EditorData(ARealData aRealData)
        {
            ARealData = aRealData;
            BRealData = new BRealData(null);
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
                onChangeNameAction?.Invoke(DataName);
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

        private bool IsNotReferenceDataID()
        {
            var dataID = BRealData.DataID;
            if (string.IsNullOrEmpty(dataID)) return true;
            return !dataID.Equals(ReferenceDataID);
        }

        [ShowIf(nameof(IsNotReferenceDataID))]
        [Button]
        private void Set_BDataID()
        {
            BRealData = new BRealData(ReferenceDataID);
            Example_EditorWindow.BDataSoData.Datas.Add(BRealData);
        }

    #endregion

        public void SetChangeNameAction(Action<string> action)
        {
            onChangeNameAction = action;
        }

        private bool IsNameExist()
        {
            if (!string.IsNullOrEmpty(DataName))
                return !IsSameDataName(DataName);
            message = EditorWindowDescription.StringEmpty;
            return false;
        }

        private bool IsSameDataName(string dataName)
        {
            var editorDatas = Example_EditorWindow.EditorDatas;
            var editorData = editorDatas.FirstOrDefault(data => data != this && string.Equals(data.DataName, dataName));
            if (editorData == null) return false;
            message = $"{EditorWindowDescription.DataIsExist} , Name: {dataName} , editorData:{editorData.DataName}";
            return true;
        }
    }
}