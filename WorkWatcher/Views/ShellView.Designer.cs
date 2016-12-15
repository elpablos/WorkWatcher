namespace Lorenzo.WorkWatcher.Views
{
    partial class ShellView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShellView));
            this.btnStart = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.WatcherNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.timerWatcher = new System.Windows.Forms.Timer(this.components);
            this.ViewModelBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridRows = new System.Windows.Forms.DataGridView();
            this.ProcessId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WindowTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.ViewModelBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRows)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(545, 12);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "To DB";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(626, 12);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 2;
            this.btnStop.Text = "To CSV";
            this.btnStop.UseVisualStyleBackColor = true;
            // 
            // WatcherNotifyIcon
            // 
            this.WatcherNotifyIcon.BalloonTipText = "Stále běžím";
            this.WatcherNotifyIcon.BalloonTipTitle = "Work Watcher";
            this.WatcherNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("WatcherNotifyIcon.Icon")));
            this.WatcherNotifyIcon.Text = "WorkWatcher";
            this.WatcherNotifyIcon.Visible = true;
            this.WatcherNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.WatcherNotifyIcon_MouseDoubleClick);
            // 
            // dataGridRows
            // 
            this.dataGridRows.AllowUserToAddRows = false;
            this.dataGridRows.AllowUserToDeleteRows = false;
            this.dataGridRows.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridRows.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridRows.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ProcessId,
            this.ProcessName,
            this.WindowTitle,
            this.Description,
            this.DateCreated});
            this.dataGridRows.Location = new System.Drawing.Point(12, 41);
            this.dataGridRows.Name = "dataGridRows";
            this.dataGridRows.ReadOnly = true;
            this.dataGridRows.Size = new System.Drawing.Size(689, 427);
            this.dataGridRows.TabIndex = 3;
            // 
            // ProcessId
            // 
            this.ProcessId.DataPropertyName = "ProcessId";
            this.ProcessId.HeaderText = "ProcessId";
            this.ProcessId.Name = "ProcessId";
            this.ProcessId.ReadOnly = true;
            // 
            // ProcessName
            // 
            this.ProcessName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ProcessName.DataPropertyName = "ProcessName";
            this.ProcessName.HeaderText = "ProcessName";
            this.ProcessName.Name = "ProcessName";
            this.ProcessName.ReadOnly = true;
            // 
            // WindowTitle
            // 
            this.WindowTitle.DataPropertyName = "WindowTitle";
            this.WindowTitle.HeaderText = "WindowTitle";
            this.WindowTitle.Name = "WindowTitle";
            this.WindowTitle.ReadOnly = true;
            // 
            // Description
            // 
            this.Description.DataPropertyName = "Description";
            this.Description.HeaderText = "Description";
            this.Description.Name = "Description";
            this.Description.ReadOnly = true;
            // 
            // DateCreated
            // 
            this.DateCreated.DataPropertyName = "DateCreated";
            this.DateCreated.HeaderText = "DateCreated";
            this.DateCreated.Name = "DateCreated";
            this.DateCreated.ReadOnly = true;
            // 
            // ShellView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(713, 480);
            this.Controls.Add(this.dataGridRows);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.btnStart);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ShellView";
            this.ShowInTaskbar = false;
            this.Text = "Don\'t be a slacker!";
            ((System.ComponentModel.ISupportInitialize)(this.ViewModelBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRows)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.NotifyIcon WatcherNotifyIcon;
        private System.Windows.Forms.Timer timerWatcher;
        private System.Windows.Forms.BindingSource ViewModelBindingSource;
    private System.Windows.Forms.DataGridView dataGridRows;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessName;
        private System.Windows.Forms.DataGridViewTextBoxColumn WindowTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateCreated;
    }
}