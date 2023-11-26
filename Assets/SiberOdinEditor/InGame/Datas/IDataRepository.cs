using System.Collections.Generic;

namespace SiberOdinEditor.Datas
{
    public interface IDataRepository
    {
        void Set(object data);

        void Add(object data);

        void AddOrSet(object data);

        void Remove(object data);

        void RemoveAt(int i);

        IData GetDataByID(string dataId);

        void RemoveByID(string dataId);

        bool EqualDataType(IData data);

        List<IData> GetDatas();
    }
}