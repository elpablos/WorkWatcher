using System;

namespace Lorenzo.WorkWatcher.Models
{
    public class ChartItemModel : BaseModel
    {
        private string _ProcessName;
        public string ProcessName
        {
            get { return _ProcessName; }
            set
            {
                _ProcessName = value;
                NotifyOfPropertyChange();
            }
        }

        private TimeSpan _Amount;
        public TimeSpan Amount
        {
            get { return _Amount; }
            set
            {
                _Amount = value;
                NotifyOfPropertyChange();
            }
        }

        private string _WindowTitle;
        public string WindowTitle
        {
            get { return _WindowTitle; }
            set
            {
                _WindowTitle = value;
                NotifyOfPropertyChange();
            }
        }

        private DateTime _Date;
        public DateTime Date
        {
            get { return _Date; }
            set
            {
                _Date = value;
                NotifyOfPropertyChange();
            }
        }

        private int _Count;
        public int Count
        {
            get { return _Count; }
            set
            {
                _Count = value;
                NotifyOfPropertyChange();
            }
        }
    }
}
