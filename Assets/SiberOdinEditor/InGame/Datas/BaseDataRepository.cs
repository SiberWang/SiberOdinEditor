using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace SiberOdinEditor.Datas
{
    public abstract class BaseDataRepository<T> : SerializedScriptableObject, IDataRepository
    where T : IData
    {
    #region ========== [Public Variables] ==========

        public List<T> Datas => datas;

        protected virtual string LabelText => $"{typeof(T).Name}s";

    #endregion

    #region ========== [Private Variables] ==========

        [SerializeField]
        protected List<T> datas = new List<T>();

    #endregion

    #region ========== [Public Methods] ==========

        public void AddOrSet(object data)
        {
            var baseData = (T)data;
            if (!datas.Contains(baseData))
                datas.Add(baseData);
            else
            {
                var i = datas.IndexOf(baseData);
                datas[i].SetData(baseData);
            }
        }

        public void Set(object data)
        {
            var baseData = (T)data;
            if (!datas.Contains(baseData)) return;
            var i = datas.IndexOf(baseData);
            datas[i].SetData(baseData);
        }

        public void Add(object data)
        {
            var baseData = (T)data;
            if (datas.Contains(baseData)) return;
            datas.Add(baseData);
        }

        public void Remove(object data)
        {
            var baseData = (T)data;
            if (!datas.Contains(baseData)) return;
            datas.Remove(baseData);
        }

        public void RemoveAt(int i)
        {
            if (i >= 0 && i < datas.Count)
                datas.RemoveAt(i);
        }

        public void RemoveByID(string dataId)
        {
            var data = GetDataByID(dataId);
            Remove(data);
        }

        public IData GetDataByID(string dataId)
        {
            var data = datas.FirstOrDefault(s => s.DataID.Equals(dataId));
            return data;
        }

        public bool EqualDataType(IData data)
        {
            return data.GetType() == typeof(T);
        }

        public List<IData> GetDatas()
        {
            return Datas.Cast<IData>().ToList();
        }

    #endregion
    }
}