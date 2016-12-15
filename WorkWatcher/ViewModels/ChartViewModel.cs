using LiveCharts;
using LiveCharts.Wpf;
using Lorenzo.WorkWatcher.Common;
using Lorenzo.WorkWatcher.Core.Managers;
using Lorenzo.WorkWatcher.Models;
using System;
using System.Linq;

namespace Lorenzo.WorkWatcher.ViewModels
{
    public class ChartViewModel : BaseModel, IChartViewModel, IDisposable
    {
        private readonly ICommandManager _CommandManager;
        private readonly IDataManager _DataManager;

        private ChartModel _Model = new ChartModel();
        public ChartModel Model
        {
            get { return _Model; }
            set { _Model = value; }
        }

        public ChartViewModel(ICommandManager commandManager, IDataManager dataManager)
        {
            _CommandManager = commandManager;
            _DataManager = dataManager;
        }

        public void LoadData()
        {
            Model.Items = new SortableBindingList<ChartItemModel>(
            _DataManager.GroupedTitleData(Model.DateActual).Select(x => new ChartItemModel
            {
                Date = x.Date,
                Amount = x.Amount,
                ProcessName = x.ProcessName,
                WindowTitle = x.WindowTitle,
                Count = x.Count
            }).ToList());

            var dict = Model.Items
            .GroupBy(x => new { x.ProcessName })
            .Select(x => new { ProcessName = x.Key.ProcessName, Amount = x.Sum(y => y.Amount.Ticks / TimeSpan.TicksPerMillisecond) })
            .ToDictionary(x => x.ProcessName, x => x.Amount);

            Model.SeriesCollection = new SeriesCollection();
            foreach (var pair in dict)
            {
                Model.SeriesCollection.Add(new PieSeries
                {
                    Title = pair.Key,
                    Values = new ChartValues<long> { pair.Value },
                    DataLabels = true,
                    LabelPoint = value => new DateTime((long)value.Y * TimeSpan.TicksPerMillisecond).ToString("t")
                });
            }
        }

        public void ViewShown()
        {
            _CommandManager.RaiseAllCanExecute();
        }

        public void ClosingWindow()
        {
        }

        public void Dispose()
        {
        }
    }
}
