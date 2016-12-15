using System;

namespace Lorenzo.WorkWatcher.Core.DbModels
{
    public class RawDataGrouped
    {
        public string ProcessName { get; set; }

        public string WindowTitle { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Amount { get; set; }

        public int Count { get; set; }
    }
}
