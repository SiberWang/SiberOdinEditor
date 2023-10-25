using System;
using Sirenix.OdinInspector;

namespace Examples.Scripts.Core
{
    /// <summary> 真正的純資料！ </summary>
    /// 頂多加個順序 PropertyOrder 或是 LabelText
    [Serializable]
    public class ARealData : RealData
    {
    #region ========== [Public Variables] ==========

        public string Name;
        public int    HP;
        public float  MoveSpeed;

    #endregion

    #region ========== [Private Methods] ==========

        public ARealData() : base() { }

        public ARealData(string name) : base()
        {
            Name = name;
        }

        protected override void Init()
        {
            Name      ??= "New Data";
            HP        =   100;
            MoveSpeed =   3;
        }
        
        [ShowIf(nameof(IsNullID))]
        [Button("新 DataID")]
        private void NewDataID()
        {
            dataID = Guid.NewGuid().ToString();
        }
        
    #endregion
        
    }
}