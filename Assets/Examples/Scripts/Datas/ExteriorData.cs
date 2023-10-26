using System;
using Examples.Scripts.Core;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Examples.Scripts.Datas
{
    [Serializable]
    public class ExteriorData : BaseData
    {
    #region ========== [Public Variables] ==========

        public Sprite bodySprite;
        public int    someValue;
        public string someContext;

    #endregion

    #region ========== [Private Methods] ==========

        public ExteriorData(string referenceDataID) : base(referenceDataID) { }

        protected override void Init()
        {
            bodySprite    = null;
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