namespace Frontend
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
            System.Windows.Forms.Label label2;
            this.btnStart = new System.Windows.Forms.Button();
            this.grpOptions = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.numDesiredTimerRes = new System.Windows.Forms.NumericUpDown();
            this.chkSetTimerResOfVmwareVmx = new System.Windows.Forms.CheckBox();
            this.chkSetTimerResOfCurrProc = new System.Windows.Forms.CheckBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.chkWantLogFile = new System.Windows.Forms.CheckBox();
            this.chkWantDebugLog = new System.Windows.Forms.CheckBox();
            this.txtLogFilePath = new System.Windows.Forms.TextBox();
            this.grpLogging = new System.Windows.Forms.GroupBox();
            this.btnRestoreDefault = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            this.grpOptions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDesiredTimerRes)).BeginInit();
            this.grpLogging.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(313, 34);
            label2.Margin = new System.Windows.Forms.Padding(3);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(28, 20);
            label2.TabIndex = 4;
            label2.Text = "ms";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(719, 151);
            this.btnStart.Margin = new System.Windows.Forms.Padding(4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(132, 32);
            this.btnStart.TabIndex = 99;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // grpOptions
            // 
            this.grpOptions.Controls.Add(this.label3);
            this.grpOptions.Controls.Add(label2);
            this.grpOptions.Controls.Add(this.numDesiredTimerRes);
            this.grpOptions.Controls.Add(this.chkSetTimerResOfVmwareVmx);
            this.grpOptions.Controls.Add(this.chkSetTimerResOfCurrProc);
            this.grpOptions.Location = new System.Drawing.Point(11, 11);
            this.grpOptions.Margin = new System.Windows.Forms.Padding(4);
            this.grpOptions.Name = "grpOptions";
            this.grpOptions.Padding = new System.Windows.Forms.Padding(2);
            this.grpOptions.Size = new System.Drawing.Size(374, 134);
            this.grpOptions.TabIndex = 0;
            this.grpOptions.TabStop = false;
            this.grpOptions.Text = "Timer Resolution";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 34);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(212, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Recommended value is 1.0 ms.";
            // 
            // numDesiredTimerRes
            // 
            this.numDesiredTimerRes.DecimalPlaces = 3;
            this.numDesiredTimerRes.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numDesiredTimerRes.Location = new System.Drawing.Point(234, 32);
            this.numDesiredTimerRes.Maximum = new decimal(new int[] {
            15625,
            0,
            0,
            196608});
            this.numDesiredTimerRes.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.numDesiredTimerRes.Name = "numDesiredTimerRes";
            this.numDesiredTimerRes.Size = new System.Drawing.Size(73, 27);
            this.numDesiredTimerRes.TabIndex = 1;
            this.numDesiredTimerRes.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // chkSetTimerResOfVmwareVmx
            // 
            this.chkSetTimerResOfVmwareVmx.AutoSize = true;
            this.chkSetTimerResOfVmwareVmx.Location = new System.Drawing.Point(20, 95);
            this.chkSetTimerResOfVmwareVmx.Name = "chkSetTimerResOfVmwareVmx";
            this.chkSetTimerResOfVmwareVmx.Size = new System.Drawing.Size(303, 24);
            this.chkSetTimerResOfVmwareVmx.TabIndex = 3;
            this.chkSetTimerResOfVmwareVmx.Text = "Set timer resolution of \"vmware-vmx.exe\"";
            this.chkSetTimerResOfVmwareVmx.UseVisualStyleBackColor = true;
            // 
            // chkSetTimerResOfCurrProc
            // 
            this.chkSetTimerResOfCurrProc.AutoSize = true;
            this.chkSetTimerResOfCurrProc.Location = new System.Drawing.Point(20, 66);
            this.chkSetTimerResOfCurrProc.Name = "chkSetTimerResOfCurrProc";
            this.chkSetTimerResOfCurrProc.Size = new System.Drawing.Size(280, 24);
            this.chkSetTimerResOfCurrProc.TabIndex = 2;
            this.chkSetTimerResOfCurrProc.Text = "Set timer resolution of current process";
            this.chkSetTimerResOfCurrProc.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            // 
            // chkWantLogFile
            // 
            this.chkWantLogFile.AutoSize = true;
            this.chkWantLogFile.Location = new System.Drawing.Point(18, 35);
            this.chkWantLogFile.Name = "chkWantLogFile";
            this.chkWantLogFile.Size = new System.Drawing.Size(124, 24);
            this.chkWantLogFile.TabIndex = 4;
            this.chkWantLogFile.Text = "Enable log file";
            this.chkWantLogFile.UseVisualStyleBackColor = true;
            this.chkWantLogFile.CheckedChanged += new System.EventHandler(this.chkWantLogFile_CheckedChanged);
            // 
            // chkWantDebugLog
            // 
            this.chkWantDebugLog.AutoSize = true;
            this.chkWantDebugLog.Location = new System.Drawing.Point(162, 35);
            this.chkWantDebugLog.Name = "chkWantDebugLog";
            this.chkWantDebugLog.Size = new System.Drawing.Size(146, 24);
            this.chkWantDebugLog.TabIndex = 5;
            this.chkWantDebugLog.Text = "Enable debug log";
            this.chkWantDebugLog.UseVisualStyleBackColor = true;
            // 
            // txtLogFilePath
            // 
            this.txtLogFilePath.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtLogFilePath.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystem;
            this.txtLogFilePath.Location = new System.Drawing.Point(18, 76);
            this.txtLogFilePath.Name = "txtLogFilePath";
            this.txtLogFilePath.Size = new System.Drawing.Size(423, 27);
            this.txtLogFilePath.TabIndex = 6;
            // 
            // grpLogging
            // 
            this.grpLogging.Controls.Add(this.chkWantDebugLog);
            this.grpLogging.Controls.Add(this.chkWantLogFile);
            this.grpLogging.Controls.Add(this.txtLogFilePath);
            this.grpLogging.Location = new System.Drawing.Point(391, 11);
            this.grpLogging.Margin = new System.Windows.Forms.Padding(2);
            this.grpLogging.Name = "grpLogging";
            this.grpLogging.Padding = new System.Windows.Forms.Padding(2);
            this.grpLogging.Size = new System.Drawing.Size(460, 134);
            this.grpLogging.TabIndex = 5;
            this.grpLogging.TabStop = false;
            this.grpLogging.Text = "Logging";
            // 
            // btnRestoreDefault
            // 
            this.btnRestoreDefault.Location = new System.Drawing.Point(11, 151);
            this.btnRestoreDefault.Name = "btnRestoreDefault";
            this.btnRestoreDefault.Size = new System.Drawing.Size(145, 32);
            this.btnRestoreDefault.TabIndex = 98;
            this.btnRestoreDefault.Text = "Restore Default";
            this.btnRestoreDefault.UseVisualStyleBackColor = true;
            this.btnRestoreDefault.Click += new System.EventHandler(this.btnRestoreDefault_Click);
            // 
            // Form1
            // 
            this.AcceptButton = this.btnStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(864, 191);
            this.Controls.Add(this.btnRestoreDefault);
            this.Controls.Add(this.grpLogging);
            this.Controls.Add(this.grpOptions);
            this.Controls.Add(this.btnStart);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VMAudioBackHost";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.grpOptions.ResumeLayout(false);
            this.grpOptions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDesiredTimerRes)).EndInit();
            this.grpLogging.ResumeLayout(false);
            this.grpLogging.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox grpOptions;
        private System.Windows.Forms.CheckBox chkSetTimerResOfVmwareVmx;
        private System.Windows.Forms.CheckBox chkSetTimerResOfCurrProc;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.NumericUpDown numDesiredTimerRes;
        private System.Windows.Forms.CheckBox chkWantLogFile;
        private System.Windows.Forms.CheckBox chkWantDebugLog;
        private System.Windows.Forms.TextBox txtLogFilePath;
        private System.Windows.Forms.GroupBox grpLogging;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnRestoreDefault;
    }
}

