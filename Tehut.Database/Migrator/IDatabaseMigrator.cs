using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tehut.Database.Migrator
{
    public interface IDatabaseMigrator
    {
        void MigrateUp(); 
    }
}
