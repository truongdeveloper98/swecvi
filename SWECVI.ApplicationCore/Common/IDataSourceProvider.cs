namespace SWECVI.ApplicationCore.Common
{
    public interface IDataSourceProvider
    {
        string GetConnectionString();

        void SetConnectionString(string connection);
    }
}
