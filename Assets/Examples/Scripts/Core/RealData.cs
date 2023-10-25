using System;
using UnityEngine;

namespace Examples.Scripts.Core
{
    public abstract class RealData : IBaseData
    {
    #region ========== [Public Variables] ==========

        // [PropertyOrder(-10)]
        // [ShowInInspector]
        public string DataID => dataID;

    #endregion

    #region ========== [Private Variables] ==========

        [HideInInspector]
        [SerializeField]
        protected string dataID;

        protected bool IsNullID => string.IsNullOrEmpty(DataID);

    #endregion

    #region ========== [Constructor] ==========

        protected RealData()
        {
            dataID ??= Guid.NewGuid().ToString();
            Init();
        }

        protected RealData(string referenceDataID)
        {
            dataID = referenceDataID;
            Init();
        }

    #endregion

    #region ========== [Private Methods] ==========

        protected abstract void Init();

    #endregion
    }
}