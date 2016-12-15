using System;

namespace Lorenzo.WorkWatcher.Core.DbModels
{
    public class RawData
    {
        public int Id { get; set; }

        public long ProcessId { get; set; }

        public string ProcessName { get; set; }

        public DateTime DateCreated { get; set; }

        public string WindowTitle { get; set; }

        public string Description { get; set; }
    }
}
