namespace SiberOdinEditor.Datas
{
    public interface IData
    {
        string  DataID { get; }
        void    Init();
        void    SetData(object data);
    }
}