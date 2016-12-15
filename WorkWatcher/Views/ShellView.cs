using LiveCharts;
using LiveCharts.Wpf;
using Lorenzo.WorkWatcher.Common;
using Lorenzo.WorkWatcher.Models;
using Lorenzo.WorkWatcher.ViewModels;
using System;
using System.Windows.Forms;

namespace Lorenzo.WorkWatcher.Views
{
    public partial class ShellView : Form, IShellView
    {
        public IShellViewModel ViewModel { get; set; }

        public ShellView(IShellViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;

            this.Resize += MainWindow_Resize;
            this.toolStripStatusVersion.Text = "v" + Application.ProductVersion;
#if !DEBUG
            this.WindowState = FormWindowState.Minimized;
            this.ShowInTaskbar = false;
#endif  
            this.Load += ShellView_Load;
            this.FormClosing += ShellView_FormClosing;
            this.Shown += ShellView_Shown;
        }

        private void InicializingBindings()
        {
            // timer vyvolavajici command Tick
            timerWatcher.Interval = 5000;
            timerWatcher.Tick += (s, e) => { ViewModel.TickCommand.Execute(null); };

            // tlacitko start
            ViewModel.StartCommand.CanExecuteChanged += (s, e) => btnStart.Enabled = ViewModel.StartCommand.CanExecute(null);
            btnStart.Click += (s, e) =>
            {
                using (var cursor = new WaitCursor())
                {
                    ViewModel.StartCommand.Execute(null);
                }
            };

            // tlacitko stop
            ViewModel.StopCommand.CanExecuteChanged += (s, e) => btnStop.Enabled = ViewModel.StopCommand.CanExecute(null);
            btnStop.Click += (s, e) =>
            {
                using (var cursor = new WaitCursor())
                {
                    ViewModel.StopCommand.Execute(null);
                }
            };

            // tlacitko Test
            ViewModel.TestCommand.CanExecuteChanged += (s, e) => btnFilter.Enabled = ViewModel.TestCommand.CanExecute(null);
            btnFilter.Click += (s, e) =>
            {
                using (var cursor = new WaitCursor())
                {
                    ViewModel.TestCommand.Execute(null);
                }
            };

            // filtr datum
            datePickerDate.DataBindings.Add("Value", ViewModel.Model, "DateActual", false, DataSourceUpdateMode.OnPropertyChanged);
            // zpravy v gridu
            dataGridRows.AutoGenerateColumns = false;
            dataGridRows.DataBindings.Add("DataSource", ViewModel.Model, "Items", false, DataSourceUpdateMode.OnPropertyChanged);

            // data grafu v gridu
            dataGridGraph.AutoGenerateColumns = false;
            dataGridGraph.DataBindings.Add("DataSource", ViewModel.Model, "GroupItems", false, DataSourceUpdateMode.OnPropertyChanged);

            // spustim timer
            timerWatcher.Start();

            // graf
            chartWorking.DataBindings.Add("Series", ViewModel.Model, "SeriesCollection", false, DataSourceUpdateMode.OnPropertyChanged);
            chartWorking.AxisY.Add(new Axis
            {
                Title = "Usage",
                LabelFormatter = value => new DateTime((long)value * TimeSpan.TicksPerMillisecond).ToString("t")
            });

            // tooltip jen pro prave prohlizeny zaznam, legenda dole
            ((DefaultTooltip)chartWorking.DataTooltip).SelectionMode = TooltipSelectionMode.OnlySender;
            chartWorking.LegendLocation = LegendLocation.Right;
            chartWorking.DisableAnimations = true;

            ViewModel.DetailCommand.CanExecuteChanged += (s, e) => chartWorking.Enabled = ViewModel.DetailCommand.CanExecute(null);
            chartWorking.DataClick += (s, e) =>
            {
                var asPixels = chartWorking.Base.ConvertToPixels(e.AsPoint());
                ViewModel.DetailCommand.Execute(new GraphDetailModel {
                    X = e.X, // asPixels.X,
                    Y = e.Y //  asPixels.Y
                });
            };
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Labels")
            {
                chartWorking.AxisX.Clear();
                chartWorking.AxisX.Add(new Axis
                {
                    Title = "Date",
                    Labels = ViewModel.Model.Labels, // poruseni bindingu
                    Separator = DefaultAxes.CleanSeparator,
                });
            }
        }

        #region Window events

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.WindowState = FormWindowState.Minimized;
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Ukoncovani okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShellView_FormClosing(object sender, FormClosingEventArgs e)
        {
            ViewModel.ClosingWindow();
        }

        /// <summary>
        /// Nacitani okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ShellView_Load(object sender, EventArgs args)
        {
            ViewModel.Model.PropertyChanged += Model_PropertyChanged;
            ViewModel.LoadData();
            InicializingBindings();
        }

        /// <summary>
        /// Pri zobrazeni okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShellView_Shown(object sender, EventArgs e)
        {
            ViewModel.ViewShown();
        }

        #endregion

        #region NotifyIconEvents

        /// <summary>
        /// Zmenca stavu okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
            {
                WatcherNotifyIcon.Visible = true;
                WatcherNotifyIcon.ShowBalloonTip(500);
                this.ShowInTaskbar = false;
                this.Hide();
            }

            else if (FormWindowState.Normal == WindowState)
            {
                WatcherNotifyIcon.Visible = false;
                this.ShowInTaskbar = true;
            }
        }

        /// <summary>
        /// Double click na ikonku vedle okna
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WatcherNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            this.ShowInTaskbar = true;
            WatcherNotifyIcon.Visible = false;
            this.Show();
        }

        #endregion
    }
}
