using Lorenzo.WorkWatcher.Common;

namespace Lorenzo.WorkWatcher.Models
{
    /// <summary>
    /// Model hlavniho okna
    /// </summary>
    public class ShellModel : BaseModel
    {
        private SortableBindingList<ShellListItemModel> _Items;
        public SortableBindingList<ShellListItemModel> Items
        {
            get { return _Items; }
            set
            {
                _Items = value;
                NotifyOfPropertyChange();
            }
        }

        private ShellListItemModel _SelectedItem;
        public ShellListItemModel SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                NotifyOfPropertyChange();
            }
        }
    }
}
