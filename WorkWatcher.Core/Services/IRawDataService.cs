using Lorenzo.WorkWatcher.Core.DbModels;
using System.Collections.Generic;

namespace Lorenzo.WorkWatcher.Core.Services
{
    public interface IRawDataService
    {
        IEnumerable<RawData> GetAll();

        int InserBulk(IEnumerable<RawData> data);
    }
}