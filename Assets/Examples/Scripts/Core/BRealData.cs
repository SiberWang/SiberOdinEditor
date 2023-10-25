using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Examples.Scripts.Core
{
    [Serializable]
    public class BRealData : RealData
    {
    #region ========== [Public Variables] ==========

        public Sprite someIcon;
        public int    someValue;
        public string someContext;

    #endregion

    #region ========== [Private Methods] ==========

        public BRealData(string referenceDataID) : base(referenceDataID) { }

        protected override void Init()
        {
            someIcon    = null;
            someValue   = 5;
            someContext = "預設內容";
        }
        
        [PropertyOrder(-11)]
        [ShowIf(nameof(IsNullID))]
        [Button("設定參考對象的DataID")]
        private void NewGuid(string referenceDataID)
        {
            dataID = referenceDataID;
        }

    #endregion
    }
}