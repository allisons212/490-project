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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.processName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serviceTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.sysProcess = new System.Diagnostics.Process();
            this.timeUnit = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.ms = new System.Windows.Forms.Label();
            this.sysStatLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.SuspendLayout();
            // 
            // startSysButton
            // 
            this.startSysButton.Location = new System.Drawing.Point(26, 28);
            this.startSysButton.Name = "startSysButton";
            this.startSysButton.Size = new System.Drawing.Size(212, 52);
            this.startSysButton.TabIndex = 0;
            this.startSysButton.Text = "Start System";
            this.startSysButton.UseVisualStyleBackColor = true;
            this.startSysButton.Click += new System.EventHandler(this.StartSys_Click);
            this.startSysButton.MouseCaptureChanged += new System.EventHandler(this.StartSys_MouseCaptureChanged);
            // 
            // pauseSysButton
            // 
            this.pauseSysButton.Location = new System.Drawing.Point(279, 28);
            this.pauseSysButton.Name = "pauseSysButton";
            this.pauseSysButton.Size = new System.Drawing.Size(212, 52);
            this.pauseSysButton.TabIndex = 1;
            this.pauseSysButton.Text = "Pause System";
            this.pauseSysButton.UseVisualStyleBackColor = true;
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
            this.waitingProcessQueue.Click += new System.EventHandler(this.Label1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.processName,
            this.serviceTime});
            this.dataGridView1.Location = new System.Drawing.Point(37, 161);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(296, 150);
            this.dataGridView1.TabIndex = 5;
            // 
            // processName
            // 
            this.processName.HeaderText = "Process Name";
            this.processName.Name = "processName";
            // 
            // serviceTime
            // 
            this.serviceTime.HeaderText = "Service Time";
            this.serviceTime.Name = "serviceTime";
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
            this.timeUnit.Click += new System.EventHandler(this.Label2_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(541, 113);
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(63, 22);
            this.numericUpDown1.TabIndex = 7;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // ms
            // 
            this.ms.AutoSize = true;
            this.ms.Location = new System.Drawing.Point(609, 115);
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
            this.sysStatLabel.Size = new System.Drawing.Size(150, 25);
            this.sysStatLabel.TabIndex = 9;
            this.sysStatLabel.Text = "System Paused";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(39, 368);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(702, 60);
            this.panel1.TabIndex = 10;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 32.62455F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 67.37545F));
            this.tableLayoutPanel1.Location = new System.Drawing.Point(433, 188);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(262, 107);
            this.tableLayoutPanel1.TabIndex = 11;
            this.tableLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.TableLayoutPanel1_Paint);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startSysButton; // start system
        private System.Windows.Forms.Button pauseSysButton; // pause system
        private System.Windows.Forms.Label waitingProcessQueue;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn processName;
        private System.Windows.Forms.DataGridViewTextBoxColumn serviceTime;
        private System.Windows.Forms.Timer timer1;
        private System.Diagnostics.Process sysProcess;
        private System.Windows.Forms.Label timeUnit;
        private System.Windows.Forms.Label ms;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label sysStatLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
    }
}

