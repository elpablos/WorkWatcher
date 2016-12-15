using Lorenzo.WorkWatcher.Models;

namespace Lorenzo.WorkWatcher.ViewModels
{
    public interface IChartViewModel
    {
        ChartModel Model { get; }

        void LoadData();
        void ClosingWindow();
        void ViewShown();
    }
}
