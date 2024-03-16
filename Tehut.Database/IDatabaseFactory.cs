using System.Data;

namespace Tehut.Database
{
    public interface IDatabaseFactory
    {
        string GetConnectionString(); 

        IDbConnection CreateConnection();
    }
}
