using System.Data;

namespace Tehut.Database
{
    public interface IDatabaseFactory
    {
        IDbConnection CreateConnection();
    }
}
