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
            this.btnFilter = new System.Windows.Forms.Button();
            this.tabCtrl = new System.Windows.Forms.TabControl();
            this.tabSummary = new System.Windows.Forms.TabPage();
            this.chartWorking = new LiveCharts.WinForms.CartesianChart();
            this.tabData = new System.Windows.Forms.TabPage();
            this.dataGridGraph = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Amount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabProcesses = new System.Windows.Forms.TabPage();
            this.dataGridRows = new System.Windows.Forms.DataGridView();
            this.ProcessId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProcessName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WindowTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Description = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateCreated = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.menuWorkWatcher = new System.Windows.Forms.MenuStrip();
            this.datePickerDate = new System.Windows.Forms.DateTimePicker();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.statusWorkWatcher = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLeft = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusSpacer = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabCtrl.SuspendLayout();
            this.tabSummary.SuspendLayout();
            this.tabData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridGraph)).BeginInit();
            this.tabProcesses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRows)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.statusWorkWatcher.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(3, 3);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "To DB";
            this.btnStart.UseVisualStyleBackColor = true;
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(84, 3);
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
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(413, 3);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(75, 23);
            this.btnFilter.TabIndex = 4;
            this.btnFilter.Text = "Select";
            this.btnFilter.UseVisualStyleBackColor = true;
            // 
            // tabCtrl
            // 
            this.tabCtrl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCtrl.Controls.Add(this.tabSummary);
            this.tabCtrl.Controls.Add(this.tabData);
            this.tabCtrl.Controls.Add(this.tabProcesses);
            this.tabCtrl.Location = new System.Drawing.Point(12, 61);
            this.tabCtrl.Name = "tabCtrl";
            this.tabCtrl.SelectedIndex = 0;
            this.tabCtrl.Size = new System.Drawing.Size(984, 643);
            this.tabCtrl.TabIndex = 5;
            // 
            // tabSummary
            // 
            this.tabSummary.Controls.Add(this.chartWorking);
            this.tabSummary.Location = new System.Drawing.Point(4, 22);
            this.tabSummary.Name = "tabSummary";
            this.tabSummary.Padding = new System.Windows.Forms.Padding(3);
            this.tabSummary.Size = new System.Drawing.Size(976, 617);
            this.tabSummary.TabIndex = 0;
            this.tabSummary.Text = "Summary";
            this.tabSummary.UseVisualStyleBackColor = true;
            // 
            // chartWorking
            // 
            this.chartWorking.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chartWorking.Location = new System.Drawing.Point(6, 6);
            this.chartWorking.Name = "chartWorking";
            this.chartWorking.Size = new System.Drawing.Size(964, 605);
            this.chartWorking.TabIndex = 0;
            this.chartWorking.Text = "cartesianChart1";
            // 
            // tabData
            // 
            this.tabData.Controls.Add(this.dataGridGraph);
            this.tabData.Location = new System.Drawing.Point(4, 22);
            this.tabData.Name = "tabData";
            this.tabData.Padding = new System.Windows.Forms.Padding(3);
            this.tabData.Size = new System.Drawing.Size(976, 617);
            this.tabData.TabIndex = 2;
            this.tabData.Text = "Data";
            this.tabData.UseVisualStyleBackColor = true;
            // 
            // dataGridGraph
            // 
            this.dataGridGraph.AllowUserToAddRows = false;
            this.dataGridGraph.AllowUserToDeleteRows = false;
            this.dataGridGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridGraph.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridGraph.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn5,
            this.Amount});
            this.dataGridGraph.Location = new System.Drawing.Point(6, 6);
            this.dataGridGraph.Name = "dataGridGraph";
            this.dataGridGraph.ReadOnly = true;
            this.dataGridGraph.Size = new System.Drawing.Size(964, 605);
            this.dataGridGraph.TabIndex = 5;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.dataGridViewTextBoxColumn2.DataPropertyName = "ProcessName";
            this.dataGridViewTextBoxColumn2.HeaderText = "ProcessName";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "Date";
            this.dataGridViewTextBoxColumn5.FillWeight = 150F;
            this.dataGridViewTextBoxColumn5.HeaderText = "Date";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // Amount
            // 
            this.Amount.DataPropertyName = "Amount";
            this.Amount.HeaderText = "Amount";
            this.Amount.Name = "Amount";
            this.Amount.ReadOnly = true;
            // 
            // tabProcesses
            // 
            this.tabProcesses.Controls.Add(this.dataGridRows);
            this.tabProcesses.Location = new System.Drawing.Point(4, 22);
            this.tabProcesses.Name = "tabProcesses";
            this.tabProcesses.Padding = new System.Windows.Forms.Padding(3);
            this.tabProcesses.Size = new System.Drawing.Size(976, 617);
            this.tabProcesses.TabIndex = 1;
            this.tabProcesses.Text = "Processing";
            this.tabProcesses.UseVisualStyleBackColor = true;
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
            this.dataGridRows.Location = new System.Drawing.Point(3, 6);
            this.dataGridRows.Name = "dataGridRows";
            this.dataGridRows.ReadOnly = true;
            this.dataGridRows.Size = new System.Drawing.Size(967, 605);
            this.dataGridRows.TabIndex = 4;
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
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnStart);
            this.flowLayoutPanel1.Controls.Add(this.btnStop);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 30);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(487, 28);
            this.flowLayoutPanel1.TabIndex = 6;
            // 
            // menuWorkWatcher
            // 
            this.menuWorkWatcher.Location = new System.Drawing.Point(0, 0);
            this.menuWorkWatcher.Name = "menuWorkWatcher";
            this.menuWorkWatcher.Size = new System.Drawing.Size(1008, 24);
            this.menuWorkWatcher.TabIndex = 8;
            this.menuWorkWatcher.Text = "menuStrip1";
            // 
            // datePickerDate
            // 
            this.datePickerDate.CustomFormat = "dd.MM.yyyy";
            this.datePickerDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerDate.Location = new System.Drawing.Point(246, 3);
            this.datePickerDate.Name = "datePickerDate";
            this.datePickerDate.Size = new System.Drawing.Size(161, 20);
            this.datePickerDate.TabIndex = 9;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flowLayoutPanel2.Controls.Add(this.btnFilter);
            this.flowLayoutPanel2.Controls.Add(this.datePickerDate);
            this.flowLayoutPanel2.Location = new System.Drawing.Point(505, 30);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.flowLayoutPanel2.Size = new System.Drawing.Size(491, 28);
            this.flowLayoutPanel2.TabIndex = 7;
            // 
            // statusWorkWatcher
            // 
            this.statusWorkWatcher.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLeft,
            this.toolStripStatusSpacer,
            this.toolStripStatusVersion});
            this.statusWorkWatcher.Location = new System.Drawing.Point(0, 707);
            this.statusWorkWatcher.Name = "statusWorkWatcher";
            this.statusWorkWatcher.Size = new System.Drawing.Size(1008, 22);
            this.statusWorkWatcher.TabIndex = 9;
            this.statusWorkWatcher.Text = "statusStrip1";
            // 
            // toolStripStatusLeft
            // 
            this.toolStripStatusLeft.Name = "toolStripStatusLeft";
            this.toolStripStatusLeft.Size = new System.Drawing.Size(79, 17);
            this.toolStripStatusLeft.Text = "WorkWatcher";
            // 
            // toolStripStatusSpacer
            // 
            this.toolStripStatusSpacer.Name = "toolStripStatusSpacer";
            this.toolStripStatusSpacer.Size = new System.Drawing.Size(877, 17);
            this.toolStripStatusSpacer.Spring = true;
            // 
            // toolStripStatusVersion
            // 
            this.toolStripStatusVersion.Name = "toolStripStatusVersion";
            this.toolStripStatusVersion.Size = new System.Drawing.Size(37, 17);
            this.toolStripStatusVersion.Text = "v1.0.0";
            // 
            // ShellView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.statusWorkWatcher);
            this.Controls.Add(this.menuWorkWatcher);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.flowLayoutPanel2);
            this.Controls.Add(this.tabCtrl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuWorkWatcher;
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "ShellView";
            this.Text = "Don\'t be a slacker!";
            this.tabCtrl.ResumeLayout(false);
            this.tabSummary.ResumeLayout(false);
            this.tabData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridGraph)).EndInit();
            this.tabProcesses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridRows)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.statusWorkWatcher.ResumeLayout(false);
            this.statusWorkWatcher.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.NotifyIcon WatcherNotifyIcon;
        private System.Windows.Forms.Timer timerWatcher;
        private System.Windows.Forms.Button btnFilter;
        private System.Windows.Forms.TabControl tabCtrl;
        private System.Windows.Forms.TabPage tabSummary;
        private System.Windows.Forms.TabPage tabProcesses;
        private System.Windows.Forms.DataGridView dataGridRows;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessId;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProcessName;
        private System.Windows.Forms.DataGridViewTextBoxColumn WindowTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn Description;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateCreated;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private LiveCharts.WinForms.CartesianChart chartWorking;
        private System.Windows.Forms.TabPage tabData;
        private System.Windows.Forms.DataGridView dataGridGraph;
        private System.Windows.Forms.MenuStrip menuWorkWatcher;
        private System.Windows.Forms.DateTimePicker datePickerDate;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.StatusStrip statusWorkWatcher;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLeft;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusSpacer;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusVersion;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn Amount;
    }
}