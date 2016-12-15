using LiveCharts;
using Lorenzo.WorkWatcher.Common;
using System;

namespace Lorenzo.WorkWatcher.Models
{
    public class ChartModel : BaseModel
    {
        private SortableBindingList<ChartItemModel> _Items;
        public SortableBindingList<ChartItemModel> Items
        {
            get { return _Items; }
            set
            {
                _Items = value;
                NotifyOfPropertyChange();
            }
        }

        private ChartItemModel _SelectedItem;
        public ChartItemModel SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                NotifyOfPropertyChange();
            }
        }

        private DateTime _DateActual;
        public DateTime DateActual
        {
            get { return _DateActual; }
            set
            {
                _DateActual = value;
                NotifyOfPropertyChange();
            }
        }

        private SeriesCollection _SeriesCollection;
        public SeriesCollection SeriesCollection
        {
            get { return _SeriesCollection; }
            set
            {
                _SeriesCollection = value;
                NotifyOfPropertyChange();
            }
        }

        private string[] _Labels;
        public string[] Labels
        {
            get { return _Labels; }
            set
            {
                _Labels = value;
                NotifyOfPropertyChange();
            }
        }
    }
}
