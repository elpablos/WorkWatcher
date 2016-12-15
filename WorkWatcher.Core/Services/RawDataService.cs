using LiteDB;
using Lorenzo.WorkWatcher.Core.DbModels;
using System.Collections.Generic;

namespace Lorenzo.WorkWatcher.Core.Services
{
    public class RawDataService : BaseService, IRawDataService
    {
        protected readonly string TABLENAME = "rawdata";

        public IEnumerable<RawData> GetAll()
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = db.GetCollection<RawData>(TABLENAME);
                return collection.FindAll();
            }
        }

        public int InserBulk(IEnumerable<RawData> data)
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                var collection = db.GetCollection<RawData>(TABLENAME);

                // index
                collection.EnsureIndex(x => x.DateCreated);

                // vlozim
                return collection.Insert(data);
            }
        }

        public bool Truncate()
        {
            using (var db = new LiteDatabase(ConnectionString))
            {
                return db.DropCollection(TABLENAME);
            }
        }
    }
}
