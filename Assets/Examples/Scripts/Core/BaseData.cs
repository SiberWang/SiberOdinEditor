using System;
using UnityEngine;

namespace Examples.Scripts.Core
{
    public abstract class BaseData : IBaseData
    {
    #region ========== [Public Variables] ==========

        // [PropertyOrder(-10)]
        // [ShowInInspector]
        public string DataID => dataID;

    #endregion

    #region ========== [Protected Variables] ==========

        [HideInInspector]
        [SerializeField]
        protected string dataID;

        protected bool IsNullID => string.IsNullOrEmpty(DataID);

    #endregion

    #region ========== [Constructor] ==========

        protected BaseData()
        {
            dataID ??= Guid.NewGuid().ToString();
            Init();
        }

        protected BaseData(string referenceDataID)
        {
            dataID = referenceDataID;
            Init();
        }

    #endregion

    #region ========== [Protected Methods] ==========

        protected abstract void Init();

    #endregion
    }
}