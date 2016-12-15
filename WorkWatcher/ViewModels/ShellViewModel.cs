using Lorenzo.WorkWatcher.Common;
using Lorenzo.WorkWatcher.Core.Managers;
using Lorenzo.WorkWatcher.Core.Services;
using Lorenzo.WorkWatcher.Models;
using System.Windows.Input;
using System;
using System.Linq;
using Lorenzo.WorkWatcher.Core.DbModels;

namespace Lorenzo.WorkWatcher.ViewModels
{
    public class ShellViewModel : BaseModel, IShellViewModel
    {
        #region Fields

        private readonly ICommandManager _CommandManager;
        private readonly IWindowManager _WindowManager;
        private readonly IRawDataService _RawDataServise;
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

        #endregion

        #region Constructors

        public ShellViewModel(ICommandManager commandManager, IWindowManager windowManager,
            IRawDataService rawDataServise)
        {
            _CommandManager = commandManager;
            _WindowManager = windowManager;
            _RawDataServise = rawDataServise;
        }

        /// <summary>
        /// Nacteni dat
        /// </summary>
        public void LoadData()
        {
            Model.Items = new SortableBindingList<ShellListItemModel>();
            // zachytavani udalosti
            _WindowManager.ActiveWindowChanged += _WindowManager_ActiveWindowChanged;
        }

        /// <summary>
        /// Ukonceni okna
        /// </summary>
        public void ClosingWindow()
        {
            _WindowManager.ActiveWindowChanged -= _WindowManager_ActiveWindowChanged;
            _WindowManager.Dispose();

            ImportDataToDb();
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

        public void ExportData()
        {
            var items = _RawDataServise.GetAll();
            var export = new CsvExport<RawData>(items);
            export.ExportToFile("export.csv");
        }

        public void ImportDataToDb()
        {
            var oldItems = Model.Items;
            Model.Items = new SortableBindingList<ShellListItemModel>();

            var rawDataItems = oldItems.Select(x => new RawData {
               ProcessId = x.ProcessId,
               ProcessName = x.ProcessName,
               WindowTitle = x.WindowTitle,
               DateCreated = x.DateCreated,
               Description = x.Description
            });

            _RawDataServise.InserBulk(rawDataItems);
            _LastSave = DateTime.Now;
        }

        #endregion
    }
}
