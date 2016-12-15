using System;

namespace Lorenzo.WorkWatcher.Models
{
    public class GraphItemModel : BaseModel
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

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
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
    }
}