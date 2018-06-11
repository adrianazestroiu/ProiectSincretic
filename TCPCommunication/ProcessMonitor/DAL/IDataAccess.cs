using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessMonitor.DAL
{
    interface IDataAccess
    {
        void Insert(Entries logs);

        IList<Entries> GetAllLogs();
    }
}
