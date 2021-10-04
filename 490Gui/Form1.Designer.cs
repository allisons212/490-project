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
            this.processName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.serviceTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.parserBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.sysProcess = new System.Diagnostics.Process();
            this.timeUnit = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.ms = new System.Windows.Forms.Label();
            this.sysStatLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.eventLog1 = new System.Diagnostics.EventLog();
            ((System.ComponentModel.ISupportInitialize)(this.parserBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
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
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.processName,
            this.serviceTime});
            this.dataGridView1.DataSource = this.parserBindingSource;
            this.dataGridView1.Location = new System.Drawing.Point(42, 201);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(333, 188);
            this.dataGridView1.TabIndex = 5;
            // 
            // processName
            // 
            this.processName.HeaderText = "Process Name";
            this.processName.MinimumWidth = 6;
            this.processName.Name = "processName";
            this.processName.Width = 125;
            // 
            // serviceTime
            // 
            this.serviceTime.HeaderText = "Service Time";
            this.serviceTime.MinimumWidth = 6;
            this.serviceTime.Name = "serviceTime";
            this.serviceTime.Width = 125;
            // 
            // parserBindingSource
            // 
            this.parserBindingSource.DataSource = typeof(Parser);
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
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(63, 22);
            this.numericUpDown1.TabIndex = 7;
            this.numericUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
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
            this.sysStatLabel.Size = new System.Drawing.Size(150, 25);
            this.sysStatLabel.TabIndex = 9;
            this.sysStatLabel.Text = "System Paused";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.textBox1);
            this.panel1.Location = new System.Drawing.Point(37, 347);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(721, 81);
            this.panel1.TabIndex = 10;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(3, 59);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(715, 22);
            this.textBox1.TabIndex = 0;
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
            ((System.ComponentModel.ISupportInitialize)(this.parserBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button startSysButton; // start system
        private System.Windows.Forms.Button pauseSysButton; // pause system
        private System.Windows.Forms.Label waitingProcessQueue;
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
        private System.Windows.Forms.BindingSource parserBindingSource;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Diagnostics.EventLog eventLog1;
    }
}

