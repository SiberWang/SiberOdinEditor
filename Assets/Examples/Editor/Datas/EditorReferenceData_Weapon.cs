using System.Collections.Generic;
using System.Linq;
using Examples.Editor.Windows;
using Examples.Scripts.Datas;
using Sirenix.OdinInspector;

namespace Examples.Editor.Datas
{
    public class EditorReferenceData_Weapon : BaseEditorReferenceData
    {
    #region ========== [Public Variables] ==========

        [DisableIf(nameof(EnableChangeDataName))]
        [PropertyOrder(10)]
        [FoldoutGroup("武器資料")]
        [HideLabel]
        public WeaponData weaponData;

        public override string MainReferenceDataID => weaponData?.DataID;

    #endregion

    #region ========== [Protected Variables] ==========

        protected override List<BaseEditorReferenceData> EditorDatas =>
            EditorRepository.EditorWeaponDatas.ToList<BaseEditorReferenceData>();

    #endregion

    #region ========== [Constructor] ==========

        /// <summary> 新建立 </summary>
        public EditorReferenceData_Weapon()
        {
            weaponData = new WeaponData();
        }

        /// <summary> 新建立 (DataName) </summary>
        public EditorReferenceData_Weapon(string dataName)
        {
            this.dataName = dataName;
            weaponData    = new WeaponData(dataName);
        }

        /// <summary> 直接 References (Editor ~ Real Data) </summary>
        /// <param name="weaponData"> 參考目標 </param>
        public EditorReferenceData_Weapon(WeaponData weaponData)
        {
            this.weaponData = weaponData;
        }

    #endregion

    #region ========== [Private Methods] ==========

        private bool IsNotReferenceDataID()
        {
            // var dataID = exteriorData.DataID;
            // if (string.IsNullOrEmpty(dataID)) return true;
            // if (!dataID.Equals(ReferenceDataID)) return true;
            return false;
        }

        [ShowIf(nameof(IsNotReferenceDataID))]
        [Button("設定參考 DataID")]
        private void SetReferenceDataID()
        {
            // exteriorData = new ExteriorData(ReferenceDataID);
            // DataManager.exteriorDataContainer.Datas.Add(exteriorData);
        }

    #endregion
    }
}