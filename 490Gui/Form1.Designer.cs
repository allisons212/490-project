namespace _490Gui
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.startSysButton = new System.Windows.Forms.Button();
            this.pauseSysButton = new System.Windows.Forms.Button();
            this.waitingProcessQueue = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.sysProcess = new System.Diagnostics.Process();
            this.timeUnit = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.ms = new System.Windows.Forms.Label();
            this.sysStatLabel = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.currTPutLabel = new System.Windows.Forms.Label();
            this.cpu1header = new System.Windows.Forms.Label();
            this.cpu1execheader = new System.Windows.Forms.Label();
            this.cpu1TR = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cpu1TRShow = new System.Windows.Forms.Label();
            this.cpu1ProcessExec = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cpu2TRShow = new System.Windows.Forms.Label();
            this.cpu2ProcessExec = new System.Windows.Forms.Label();
            this.cpu2TR = new System.Windows.Forms.Label();
            this.cpu2execheader = new System.Windows.Forms.Label();
            this.cpu2header = new System.Windows.Forms.Label();
            this.parserBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.parserBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // startSysButton
            // 
            this.startSysButton.Location = new System.Drawing.Point(37, 28);
            this.startSysButton.Name = "startSysButton";
            this.startSysButton.Size = new System.Drawing.Size(212, 52);
            this.startSysButton.TabIndex = 0;
            this.startSysButton.Text = "Start System";
            this.startSysButton.UseVisualStyleBackColor = true;
            this.startSysButton.Click += new System.EventHandler(this.StartSys_Click);
            // 
            // pauseSysButton
            // 
            this.pauseSysButton.Location = new System.Drawing.Point(279, 28);
            this.pauseSysButton.Name = "pauseSysButton";
            this.pauseSysButton.Size = new System.Drawing.Size(212, 52);
            this.pauseSysButton.TabIndex = 1;
            this.pauseSysButton.Text = "Pause System";
            this.pauseSysButton.UseVisualStyleBackColor = true;
            this.pauseSysButton.Click += new System.EventHandler(this.PauseSysButton_Click);
            // 
            // waitingProcessQueue
            // 
            this.waitingProcessQueue.AutoSize = true;
            this.waitingProcessQueue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.waitingProcessQueue.Location = new System.Drawing.Point(32, 117);
            this.waitingProcessQueue.Name = "waitingProcessQueue";
            this.waitingProcessQueue.Size = new System.Drawing.Size(225, 25);
            this.waitingProcessQueue.TabIndex = 4;
            this.waitingProcessQueue.Text = "Waiting Process Queue:";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(36, 158);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.ShowEditingIcon = false;
            this.dataGridView1.Size = new System.Drawing.Size(333, 207);
            this.dataGridView1.TabIndex = 5;
            // 
            // sysProcess
            // 
            this.sysProcess.StartInfo.Domain = "";
            this.sysProcess.StartInfo.LoadUserProfile = false;
            this.sysProcess.StartInfo.Password = null;
            this.sysProcess.StartInfo.StandardErrorEncoding = null;
            this.sysProcess.StartInfo.StandardOutputEncoding = null;
            this.sysProcess.StartInfo.UserName = "";
            this.sysProcess.SynchronizingObject = this;
            // 
            // timeUnit
            // 
            this.timeUnit.AutoSize = true;
            this.timeUnit.Location = new System.Drawing.Point(415, 115);
            this.timeUnit.Name = "timeUnit";
            this.timeUnit.Size = new System.Drawing.Size(126, 17);
            this.timeUnit.TabIndex = 6;
            this.timeUnit.Text = "One (1) time unit =";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(547, 113);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(63, 22);
            this.numericUpDown1.TabIndex = 7;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown1.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.NumericUpDown1_ValueChanged);
            // 
            // ms
            // 
            this.ms.AutoSize = true;
            this.ms.Location = new System.Drawing.Point(612, 115);
            this.ms.Name = "ms";
            this.ms.Size = new System.Drawing.Size(26, 17);
            this.ms.TabIndex = 8;
            this.ms.Text = "ms";
            // 
            // sysStatLabel
            // 
            this.sysStatLabel.AutoSize = true;
            this.sysStatLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.sysStatLabel.Location = new System.Drawing.Point(575, 39);
            this.sysStatLabel.Name = "sysStatLabel";
            this.sysStatLabel.Size = new System.Drawing.Size(155, 25);
            this.sysStatLabel.TabIndex = 9;
            this.sysStatLabel.Text = "System Running";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // eventLog1
            // 
            this.eventLog1.Log = "System";
            this.eventLog1.SynchronizingObject = this;
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(37, 404);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(696, 158);
            this.dataGridView2.TabIndex = 12;
            // 
            // currTPutLabel
            // 
            this.currTPutLabel.AutoSize = true;
            this.currTPutLabel.Location = new System.Drawing.Point(34, 569);
            this.currTPutLabel.Name = "currTPutLabel";
            this.currTPutLabel.Size = new System.Drawing.Size(274, 17);
            this.currTPutLabel.TabIndex = 14;
            this.currTPutLabel.Text = "Current Throughput: x process/unit of time";
            // 
            // cpu1header
            // 
            this.cpu1header.AutoSize = true;
            this.cpu1header.Location = new System.Drawing.Point(107, 0);
            this.cpu1header.Name = "cpu1header";
            this.cpu1header.Size = new System.Drawing.Size(52, 17);
            this.cpu1header.TabIndex = 15;
            this.cpu1header.Text = "CPU 1:";
            // 
            // cpu1execheader
            // 
            this.cpu1execheader.AutoSize = true;
            this.cpu1execheader.Location = new System.Drawing.Point(3, 32);
            this.cpu1execheader.Name = "cpu1execheader";
            this.cpu1execheader.Size = new System.Drawing.Size(73, 17);
            this.cpu1execheader.TabIndex = 16;
            this.cpu1execheader.Text = "Executing:";
            // 
            // cpu1TR
            // 
            this.cpu1TR.AutoSize = true;
            this.cpu1TR.Location = new System.Drawing.Point(3, 60);
            this.cpu1TR.Name = "cpu1TR";
            this.cpu1TR.Size = new System.Drawing.Size(114, 17);
            this.cpu1TR.TabIndex = 17;
            this.cpu1TR.Text = "Time Remaining:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cpu1TRShow);
            this.panel1.Controls.Add(this.cpu1ProcessExec);
            this.panel1.Controls.Add(this.cpu1TR);
            this.panel1.Controls.Add(this.cpu1header);
            this.panel1.Controls.Add(this.cpu1execheader);
            this.panel1.Location = new System.Drawing.Point(437, 170);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(257, 90);
            this.panel1.TabIndex = 18;
            // 
            // cpu1TRShow
            // 
            this.cpu1TRShow.AutoSize = true;
            this.cpu1TRShow.Location = new System.Drawing.Point(157, 60);
            this.cpu1TRShow.Name = "cpu1TRShow";
            this.cpu1TRShow.Size = new System.Drawing.Size(16, 17);
            this.cpu1TRShow.TabIndex = 20;
            this.cpu1TRShow.Text = "0";
            // 
            // cpu1ProcessExec
            // 
            this.cpu1ProcessExec.AutoSize = true;
            this.cpu1ProcessExec.Location = new System.Drawing.Point(155, 32);
            this.cpu1ProcessExec.Name = "cpu1ProcessExec";
            this.cpu1ProcessExec.Size = new System.Drawing.Size(42, 17);
            this.cpu1ProcessExec.TabIndex = 18;
            this.cpu1ProcessExec.Text = "None";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.cpu2TRShow);
            this.panel2.Controls.Add(this.cpu2ProcessExec);
            this.panel2.Controls.Add(this.cpu2TR);
            this.panel2.Controls.Add(this.cpu2execheader);
            this.panel2.Controls.Add(this.cpu2header);
            this.panel2.Location = new System.Drawing.Point(437, 286);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(256, 78);
            this.panel2.TabIndex = 19;
            // 
            // cpu2TRShow
            // 
            this.cpu2TRShow.AutoSize = true;
            this.cpu2TRShow.Location = new System.Drawing.Point(157, 53);
            this.cpu2TRShow.Name = "cpu2TRShow";
            this.cpu2TRShow.Size = new System.Drawing.Size(16, 17);
            this.cpu2TRShow.TabIndex = 21;
            this.cpu2TRShow.Text = "0";
            // 
            // cpu2ProcessExec
            // 
            this.cpu2ProcessExec.AutoSize = true;
            this.cpu2ProcessExec.Location = new System.Drawing.Point(155, 30);
            this.cpu2ProcessExec.Name = "cpu2ProcessExec";
            this.cpu2ProcessExec.Size = new System.Drawing.Size(42, 17);
            this.cpu2ProcessExec.TabIndex = 20;
            this.cpu2ProcessExec.Text = "None";
            // 
            // cpu2TR
            // 
            this.cpu2TR.AutoSize = true;
            this.cpu2TR.Location = new System.Drawing.Point(3, 53);
            this.cpu2TR.Name = "cpu2TR";
            this.cpu2TR.Size = new System.Drawing.Size(114, 17);
            this.cpu2TR.TabIndex = 20;
            this.cpu2TR.Text = "Time Remaining:";
            // 
            // cpu2execheader
            // 
            this.cpu2execheader.AutoSize = true;
            this.cpu2execheader.Location = new System.Drawing.Point(3, 30);
            this.cpu2execheader.Name = "cpu2execheader";
            this.cpu2execheader.Size = new System.Drawing.Size(73, 17);
            this.cpu2execheader.TabIndex = 20;
            this.cpu2execheader.Text = "Executing:";
            // 
            // cpu2header
            // 
            this.cpu2header.AutoSize = true;
            this.cpu2header.Location = new System.Drawing.Point(107, 0);
            this.cpu2header.Name = "cpu2header";
            this.cpu2header.Size = new System.Drawing.Size(52, 17);
            this.cpu2header.TabIndex = 20;
            this.cpu2header.Text = "CPU 2:";
            // 
            // parserBindingSource
            // 
            this.parserBindingSource.DataSource = typeof(Parser);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 595);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.currTPutLabel);
            this.Controls.Add(this.dataGridView2);
            this.Controls.Add(this.sysStatLabel);
            this.Controls.Add(this.ms);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.timeUnit);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.waitingProcessQueue);
            this.Controls.Add(this.pauseSysButton);
            this.Controls.Add(this.startSysButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "490 Process Simulator";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.parserBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startSysButton; // start system
        private System.Windows.Forms.Button pauseSysButton; // pause system
        private System.Windows.Forms.Label waitingProcessQueue; // "Waiting Process Queue:" Header
        private System.Windows.Forms.DataGridView dataGridView1; // table that shows waiting process
        private System.Windows.Forms.Timer timer1;
        private System.Diagnostics.Process sysProcess;
        private System.Windows.Forms.Label timeUnit; // "One (1) time unit = "
        private System.Windows.Forms.Label ms; // "ms"
        private System.Windows.Forms.NumericUpDown numericUpDown1; // table to set time unit value
        private System.Windows.Forms.Label sysStatLabel; // status label for if system is running or not
        private System.Windows.Forms.BindingSource parserBindingSource;
        private System.Windows.Forms.OpenFileDialog openFileDialog1; // opens on program load to ask user to select file
        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.DataGridView dataGridView2; // data grid that holds log of processes
        private System.Windows.Forms.Label currTPutLabel; // current throughput line at bottom
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label cpu2TR; // "Time remaining" in CPU 2 panel
        private System.Windows.Forms.Label cpu2execheader; // "Executing" in cpu 2 panel
        private System.Windows.Forms.Label cpu2header; // "CPU 2"
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label cpu1TR; // "Time remaining" in CPU 1 panel
        private System.Windows.Forms.Label cpu1header; // "CPU 1"
        private System.Windows.Forms.Label cpu1execheader; // "Executing" in cpu 1 panel
        private System.Windows.Forms.Label cpu2TRShow; // shows time remaining in CPU 2
        private System.Windows.Forms.Label cpu2ProcessExec; // shows name of process executing in CPU 2 panel
        private System.Windows.Forms.Label cpu1TRShow; // shows time remaining in CPU 1 
        private System.Windows.Forms.Label cpu1ProcessExec; // shows process executing in CPU 1 panel
    }
}

