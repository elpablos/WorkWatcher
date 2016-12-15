using System;

namespace Lorenzo.WorkWatcher.Models
{
    /// <summary>
    /// Model polozky zmeny okna
    /// </summary>
    public class ShellListItemModel : BaseModel
    {
        private long _ProcessId;
        public long ProcessId
        {
            get { return _ProcessId; }
            set
            {
                _ProcessId = value;
                NotifyOfPropertyChange();
            }
        }

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

        private DateTime _DateCreated;
        public DateTime DateCreated
        {
            get { return _DateCreated; }
            set
            {
                _DateCreated = value;
                NotifyOfPropertyChange();
            }
        }
    }
}
