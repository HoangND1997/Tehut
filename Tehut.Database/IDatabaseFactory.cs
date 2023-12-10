using System.Data;

namespace Tehut.Database
{
    public interface IDatabaseFactory
    {
        public IDbConnection CreateConnection(); 
    }
}
