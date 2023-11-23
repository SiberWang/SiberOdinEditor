namespace SiberOdinEditor.Datas
{
    public interface IDataRepository
    {
        void Add(object data);

        void Remove(object data);

        void RemoveAt(int i);

        IData GetDataByID(string dataId);

        bool EqualDataType(IData data);
    }
}