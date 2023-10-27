using System.Collections.Generic;
using System.Linq;
using Examples.Editor.Windows;
using Examples.Scripts.Datas;
using Sirenix.OdinInspector;

namespace Examples.Editor.Datas
{
    /// <summary> For Editor Window 使用 , 非真實資料 <br/>
    /// 這邊可以設置 Odin 的各種 Attribute , 與真實資料脫鉤 <br/>
    /// </summary>
    public class EditorReferenceData_Character : BaseEditorReferenceData
    {
    #region ========== [Public Variables] ==========

        [DisableIf(nameof(EnableChangeDataName))]
        [PropertyOrder(10)]
        [FoldoutGroup("角色資料")]
        [HideLabel]
        public CharacterData characterData;

        [DisableIf(nameof(EnableChangeDataName))]
        [PropertyOrder(10)]
        [FoldoutGroup("其他資料")]
        [HideLabel]
        public ExteriorData exteriorData;


        public override string MainReferenceDataID => characterData?.DataID;

    #endregion

    #region ========== [Protected Variables] ==========

        protected override List<BaseEditorReferenceData> EditorDatas =>
            EditorRepository.EditorCharacterDatas.ToList<BaseEditorReferenceData>();

    #endregion

    #region ========== [Constructor] ==========

        /// <summary> 新建立 </summary>
        public EditorReferenceData_Character()
        {
            characterData = new CharacterData();
            exteriorData  = new ExteriorData(characterData.DataID);
        }

        /// <summary> 新建立 (DataName) </summary>
        public EditorReferenceData_Character(string dataName)
        {
            this.dataName = dataName;
            characterData = new CharacterData(dataName);
            exteriorData  = new ExteriorData(characterData.DataID);
        }

        /// <summary> 直接 References (Editor ~ Real Data) </summary>
        /// <param name="characterData"> 參考目標 </param>
        public EditorReferenceData_Character(CharacterData characterData)
        {
            this.characterData = characterData;
            exteriorData       = new ExteriorData(null);
        }

    #endregion

    #region ========== [Private Methods] ==========

        private bool IsNotReferenceDataID()
        {
            var dataID = exteriorData.DataID;
            if (string.IsNullOrEmpty(dataID)) return true;
            if (!dataID.Equals(MainReferenceDataID)) return true;
            return false;
        }

        [ShowIf(nameof(IsNotReferenceDataID))]
        [Button("設定參考 DataID")]
        private void SetReferenceDataID()
        {
            exteriorData = new ExteriorData(MainReferenceDataID);
            EditorRepository.ExteriorDataContainer.Datas.Add(exteriorData);
            EditorRepository.SetAllDataDirty();
        }

    #endregion
    }
}