using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lorenzo.WorkWatcher.Core.Services
{
    public abstract class BaseService
    {
        #region Properties

        public string ConnectionString { get; private set; }

        #endregion

        #region Constructors

        public BaseService()
            : this("ConnectionString")
        { }

        public BaseService(string connString)
        {
            var connection = System.Configuration.ConfigurationManager
                .ConnectionStrings[connString].ConnectionString;
            ConnectionString = connection;
        }

        #endregion

        #region MyRegion

        #endregion
    }
}
