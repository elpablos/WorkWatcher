using LiteDB;
using Lorenzo.WorkWatcher.Core.DbModels;
using System.Collections.Generic;

namespace Lorenzo.WorkWatcher.Core.Services
{
    public class RawDataService : BaseService, IRawDataService
    {
        public IEnumerable<RawData> GetAll()
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = db.GetCollection<RawData>("rawdata");
                return collection.FindAll();
            }
        }

        public int InserBulk(IEnumerable<RawData> data)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = db.GetCollection<RawData>("rawdata");

                // index
                collection.EnsureIndex(x => x.DateCreated);

                // vlozim
                return collection.Insert(data); ;
            }
        }
    }
}
