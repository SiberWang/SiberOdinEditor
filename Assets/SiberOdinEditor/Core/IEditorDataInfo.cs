namespace Examples.Scripts.Core
{
    public interface IEditorDataInfo
    {
        string DataName { get; }

        void SetDataName(string dataName);

        bool EqualDataName(string dataName);
    }

    public interface IEditorDataSetter<T> where T : class
    {
        string FilePath { get; }
        T    Data { get; }
        void SetData(T data);
    }

    public interface IEditorData<T> : IEditorDataInfo, IEditorDataSetter<T>
    where T : class { }
}