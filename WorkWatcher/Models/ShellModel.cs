﻿using LiveCharts;
using Lorenzo.WorkWatcher.Common;
using System;

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

        private SortableBindingList<GraphItemModel> _GroupItems;
        public SortableBindingList<GraphItemModel> GroupItems
        {
            get { return _GroupItems; }
            set
            {
                _GroupItems = value;
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

        private DateTime _DateActual = DateTime.Now;
        public DateTime DateActual
        {
            get { return _DateActual; }
            set
            {
                _DateActual = value;
                NotifyOfPropertyChange();
            }
        }
    }
}
