using System;
using Examples.Scripts.Core;
using Sirenix.OdinInspector;

namespace Examples.Scripts.Datas
{
    [Serializable]
    public class WeaponData : BaseData
    {
    #region ========== [Public Variables] ==========

        public string Name;

    #endregion

    #region ========== [Constructor] ==========

        public WeaponData() : base() { }

        public WeaponData(string name) : base()
        {
            Name = name;
        }

    #endregion

    #region ========== [Protected Methods] ==========

        protected override void Init()
        {
            Name ??= "New Data";
        }

    #endregion

    #region ========== [Private Methods] ==========

        [ShowIf(nameof(IsNullID))]
        [Button("新 DataID")]
        private void NewDataID()
        {
            dataID = Guid.NewGuid().ToString();
        }

    #endregion
    }
}