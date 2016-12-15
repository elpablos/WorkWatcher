using Lorenzo.WorkWatcher.Common;
using Lorenzo.WorkWatcher.Core.Managers;
using Lorenzo.WorkWatcher.Models;
using System.Windows.Input;
using System;
using System.Linq;
using Lorenzo.WorkWatcher.Core.DbModels;
using LiveCharts;
using LiveCharts.Wpf;
using System.Collections.Generic;
using SimpleInjector;
using Lorenzo.WorkWatcher.Views;
using System.Windows.Forms;

namespace Lorenzo.WorkWatcher.ViewModels
{
    public class ShellViewModel : BaseModel, IShellViewModel
    {
        #region Fields

        private readonly ICommandManager _CommandManager;
        private readonly IWindowManager _WindowManager;
        private readonly IDataManager _DataManager;
        private readonly Container _Container;
        private DateTime _LastSave = DateTime.Now;

        #endregion

        #region Properties

        private ShellModel _Model = new ShellModel();
        public ShellModel Model
        {
            get { return _Model; }
        }

        #endregion

        #region Commands

        private ICommand _StopCommand;
        public ICommand StopCommand
        {
            get
            {
                if (_StopCommand == null)
                {
                    _StopCommand = _CommandManager.CreateCommand((x) => CanStopAction(), (x) => StopAction());
                }
                return _StopCommand;
            }
        }

        private ICommand _StartCommand;
        public ICommand StartCommand
        {
            get
            {
                if (_StartCommand == null)
                {
                    _StartCommand = _CommandManager.CreateCommand((x) => CanStartAction(), (x) => StartAction());
                }
                return _StartCommand;
            }
        }

        private ICommand _TickCommand;
        public ICommand TickCommand
        {
            get
            {
                if (_TickCommand == null)
                {
                    _TickCommand = _CommandManager.CreateCommand((x) => CanTickAction(), (x) => TickAction());
                }
                return _TickCommand;
            }
        }

        private ICommand _TestCommand;
        public ICommand TestCommand
        {
            get
            {
                if (_TestCommand == null)
                {
                    _TestCommand = _CommandManager.CreateCommand((x) => CanTestAction(), (x) => TestAction());
                }
                return _TestCommand;
            }
        }

        private ICommand _DetailCommand;
        public ICommand DetailCommand
        {
            get
            {
                if (_DetailCommand == null)
                {
                    _DetailCommand = _CommandManager.CreateCommand((x) => CanDetailAction(), (x) => DetailAction(x));
                }
                return _DetailCommand;
            }
        }

        #endregion

        #region Constructors

        public ShellViewModel(ICommandManager commandManager, IWindowManager windowManager,
            IDataManager dataManager, Container container)
        {
            _CommandManager = commandManager;
            _WindowManager = windowManager;
            _DataManager = dataManager;
            _Container = container;
        }

        /// <summary>
        /// Nacteni dat
        /// </summary>
        public void LoadData()
        {
            Model.Items = new SortableBindingList<ShellListItemModel>();
            // zachytavani udalosti
            _WindowManager.ActiveWindowChanged += _WindowManager_ActiveWindowChanged;

            // grafy
            LoadGraph(Model.DateActual);
        }

        /// <summary>
        /// Ukonceni okna
        /// </summary>
        public void ClosingWindow()
        {
            _WindowManager.ActiveWindowChanged -= _WindowManager_ActiveWindowChanged;
            _WindowManager.Dispose();

            ImportDataToDb(true);
            ExportData();
        }

        /// <summary>
        /// Při aktivaci okna
        /// </summary>
        public void ViewShown()
        {
            _CommandManager.RaiseAllCanExecute();
        }

        #endregion

        #region Actions

        public bool CanStopAction()
        {
            return true;
        }

        public void StopAction()
        {
            ExportData();
        }

        public bool CanStartAction()
        {
            return true;
        }

        public void StartAction()
        {
            ImportDataToDb();
        }

        public bool CanTickAction()
        {
            return true;
        }

        public void TickAction()
        {
            _WindowManager.TryActiveWindow();
        }

        public bool CanTestAction()
        {
            return true;
        }

        public void TestAction()
        {
            LoadGraph(Model.DateActual);
        }

        public bool CanDetailAction()
        {
            return true;
        }

        public void DetailAction(object obj)
        {
            // TODO - DO without Container
            GraphDetailModel model = (GraphDetailModel)obj;
            var date = Model.GroupItems.Select(x => x.Date).Distinct().ToArray()[(int)model.X];
            using (_Container.BeginLifetimeScope())
            {
                var view = _Container.GetInstance<IChartView>();
                view.ViewModel.Model.DateActual = date;
                if (((Form)view).ShowDialog() == DialogResult.OK)
                {

                }
            }


        }

        #endregion

        #region WindowManager events

        /// <summary>
        /// Zachyceni zmeny okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="windowHeader"></param>
        /// <param name="processName"></param>
        /// <param name="hwnd"></param>
        private void _WindowManager_ActiveWindowChanged(object sender, string windowHeader, string processName, IntPtr hwnd)
        {
            Model.Items.Add(new ShellListItemModel
            {
                DateCreated = DateTime.Now,
                ProcessId = (long)hwnd,
                ProcessName = processName,
                WindowTitle = windowHeader,
                Description = null
            });

            if (_LastSave.AddMinutes(2) <= DateTime.Now)
            {
                ImportDataToDb();
            }
        }

        #endregion

        #region Private methods

        public void LoadGraph(DateTime dateFilter)
        {
            var originalGroupedData = _DataManager.GroupedProcessData(dateFilter);
            Model.GroupItems = new SortableBindingList<GraphItemModel>(originalGroupedData.Select(x => new GraphItemModel
            {
                Amount = x.Amount,
                Date = x.Date,
                ProcessName = x.ProcessName,
                WindowTitle = x.WindowTitle
            }).ToList());

            var processes = originalGroupedData.Select(x => x.ProcessName).Distinct();
            var dates = originalGroupedData.Select(x => x.Date).Distinct();

            var series = new SeriesCollection();
            foreach (var process in processes)
            {
                var groups = originalGroupedData.Where(x => x.ProcessName == process).ToDictionary(x => x.Date, y => y.Amount);
                var list = new List<long>();
                foreach (var date in dates)
                {
                    list.Add(groups.ContainsKey(date) ? groups[date].Ticks / TimeSpan.TicksPerMillisecond : 0);
                }

                var serie = new StackedColumnSeries();
                serie.Title = process;
                serie.StackMode = StackMode.Values;
                serie.DataLabels = true;
                var chartValues = new ChartValues<long>();
                chartValues.AddRange(list);
                serie.Values = chartValues;

                series.Add(serie);
            }

            Model.SeriesCollection = series;
            Model.Labels = dates.Select(x => x.ToString()).ToArray();
        }

        public void ExportData()
        {
            var items = _DataManager.GetAll();
            var export = new CsvExport<RawData>(items);
            export.ExportToFile("export.csv");
        }

        public void ImportDataToDb(bool isForce = false)
        {
            var oldItems = Model.Items;
            Model.Items = new SortableBindingList<ShellListItemModel>();

            var rawDataItems = oldItems.Select(x => new RawData {
               ProcessId = x.ProcessId,
               ProcessName = x.ProcessName,
               WindowTitle = x.WindowTitle,
               DateCreated = x.DateCreated,
               Description = x.Description,
               DateFinished = x.DateCreated
            });

            _DataManager.SaveData(rawDataItems, isForce);
            _LastSave = DateTime.Now;
        }

        #endregion
    }
}
