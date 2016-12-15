using LiveCharts;
using LiveCharts.Wpf;
using Lorenzo.WorkWatcher.ViewModels;
using System;
using System.Windows.Forms;

namespace Lorenzo.WorkWatcher.Views
{
    public partial class ChartView : Form, IChartView
    {
        public IChartViewModel ViewModel { get; private set; }

        public ChartView(IChartViewModel viewModel)
        {
            InitializeComponent();
            Load += ChartView_Load;
            FormClosing += ChartView_FormClosing;
            Shown += ChartView_Shown;
            ViewModel = viewModel;

            DialogResult = DialogResult.OK;
        }

        private void InicializingBindings()
        {
            // zpravy v gridu
            dataGridDetail.AutoGenerateColumns = false;
            dataGridDetail.DataBindings.Add("DataSource", ViewModel.Model, "Items", false, DataSourceUpdateMode.OnPropertyChanged);

            // graf
            pieChartDetail.DataBindings.Add("Series", ViewModel.Model, "SeriesCollection", false, DataSourceUpdateMode.OnPropertyChanged);
            pieChartDetail.LegendLocation = LegendLocation.Bottom;
            ((DefaultTooltip)pieChartDetail.DataTooltip).SelectionMode = TooltipSelectionMode.OnlySender;
        }

        #region Window events

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ChartView_Shown(object sender, EventArgs e)
        {
            ViewModel.ViewShown();
        }

        private void ChartView_FormClosing(object sender, FormClosingEventArgs e)
        {
            ViewModel.ClosingWindow();
        }

        private void ChartView_Load(object sender, EventArgs e)
        {
            ViewModel.LoadData();
            InicializingBindings();
        }

        #endregion
    }
}
