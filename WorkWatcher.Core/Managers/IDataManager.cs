using Lorenzo.WorkWatcher.Core.DbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lorenzo.WorkWatcher.Core.Managers
{
    public interface IDataManager
    {
        IList<string> BlackList { get; set; }

        int SaveData(IEnumerable<RawData> original, bool isForce = false);
        IEnumerable<RawData> GetAll();

        IEnumerable<RawDataGrouped> GroupedTitleData(DateTime dateFilter);
        IEnumerable<RawDataGrouped> GroupedProcessData(DateTime dateFilter);
    }
}
