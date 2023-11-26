using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SiberOdinEditor.Datas
{
    /// <summary> 資料基底 </summary>
    [HideReferenceObjectPicker]
    public abstract class SubData : IData
    {
    #region ========== [Public Variables] ==========
        
        public string DataID => dataID;

    #endregion

    #region ========== [Protected Variables] ==========

        [HideInInspector]
        [SerializeField]
        protected string dataID;

        protected bool IsNullID => string.IsNullOrEmpty(DataID);

    #endregion

    #region ========== [Constructor] ==========

        protected SubData()
        {
            SetDataID(Guid.NewGuid().ToString());
            Init();
        }
        
        protected SubData(string referenceDataID)
        {
            SetDataID(referenceDataID);
            Init();
        }

        protected SubData(SubData data)
        {
            SetDataID(data.DataID);
            SetData(data);
        }

    #endregion

    #region ========== [Public Methods] ==========

        public abstract void Init();

        public abstract void SetData(object data);

        public void SetDataID(string newDataID) => dataID = newDataID;

    #endregion

    #region ========== [Protected Methods] ==========

        public abstract object InDataClone();

    #endregion
    }
}