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
            this.ViewModelBindingSource.DataSource = viewModel.Model;

            this.Resize += MainWindow_Resize;
            this.Text += " v" + Application.ProductVersion;
            this.WindowState = FormWindowState.Minimized;
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
            btnStart.Click += (s, e) => ViewModel.StartCommand.Execute(null);

            // tlacitko stop
            ViewModel.StopCommand.CanExecuteChanged += (s, e) => btnStop.Enabled = ViewModel.StopCommand.CanExecute(null);
            btnStop.Click += (s, e) => ViewModel.StopCommand.Execute(null);

            // zpravy v gridu
            dataGridRows.AutoGenerateColumns = false;
            dataGridRows.DataBindings.Add("DataSource", ViewModel.Model, "Items", true, DataSourceUpdateMode.OnPropertyChanged);

            // spustim timer
            timerWatcher.Start();
        }

        #region Window events

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
