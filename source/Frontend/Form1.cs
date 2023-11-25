using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Frontend
{
    public partial class Form1 : Form
    {
        private Backend Backend { get; } = new Backend();
        private Logging Log { get; } = new Logging();
        private Settings Settings { get; } = new Settings();

        private void LoadSettings()
        {
            numDesiredTimerRes.Value = Settings.DesiredTimerRes / 10000M;
            chkSetTimerResOfCurrProc.Checked = Settings.WantSetTimerResOfCurrProc;
            chkSetTimerResOfVmwareVmx.Checked = Settings.WantSetTimerResOfVmwareVmx;
            txtLogFilePath.Text = Settings.LogFilePath;
            chkWantLogFile.Checked = Settings.WantLogFile;
            chkWantDebugLog.Checked = Settings.WantDebugLog;
            chkWantLogFile_CheckedChanged(this, new EventArgs());
        }

        private void SaveSettings()
        {
            Settings.DesiredTimerRes = (UInt32)(numDesiredTimerRes.Value * 10000);
            Settings.WantSetTimerResOfCurrProc = chkSetTimerResOfCurrProc.Checked;
            Settings.WantSetTimerResOfVmwareVmx = chkSetTimerResOfVmwareVmx.Checked;
            Settings.LogFilePath = txtLogFilePath.Text;
            Settings.WantLogFile = chkWantLogFile.Checked;
            Settings.WantDebugLog = chkWantDebugLog.Checked;
        }


        public Form1()
        {
            InitializeComponent();
            Backend.OnErrorEnforceTimerRes += Backend_OnErrorEnforceTimerRes;
            Backend.OnErrorInjectProcess += Backend_OnErrorInjectProcess;
            Settings.OnExistingValueNull += Settings_OnExistingValueNull;
            Settings.OnExistingValueWrongType += Settings_OnExistingValueWrongType;
            Settings.OnErrorLoadValue += Settings_OnErrorLoadValue;
            Settings.OnErrorSaveValue += Settings_OnErrorSaveValue;
        }

        private void Backend_OnErrorInjectProcess(Int32 hr, UInt32 processId)
        {
            UI.MsgErr($"Error 0x{hr:X8} when trying to set timer resolution" +
                $" of vmware-vmx.exe (PID {processId}).");
        }

        private void Backend_OnErrorEnforceTimerRes(Int32 hr)
        {
            UI.MsgErr($"Error 0x{hr:X8} when trying to set timer resolution" +
                $" of current process.");
        }

        private void Settings_OnErrorSaveValue(Setting setting, Exception ex)
        {
            Log.d($"Error saving value of Settings[\"{setting.Name}\"]: " + ex.Message);
            UI.MsgErr($"Error saving value of Settings[\"{setting.Name}\"]: " + ex.Message);
        }

        private void Settings_OnErrorLoadValue(Setting setting, Exception ex)
        {
            Log.d($"Error loading value of Settings[\"{setting.Name}\"]: " + ex.Message);
            Log.d($"Fallback to default value: {setting.DefValue}");
        }

        private void Settings_OnExistingValueWrongType(Setting setting)
        {
            Log.d($"Existing value of Settings[\"{setting.Name}\"] has wrong value type.");
            Log.d($"Fallback to default value: {setting.DefValue}");
        }

        private void Settings_OnExistingValueNull(Setting setting)
        {
            Log.d($"Existing value of Settings[\" {setting.Name} \"] is null.");
            Log.d($"Fallback to default value: {setting.DefValue}");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                this.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            }
            catch (Exception)
            {
            }
            try
            {
                LoadSettings();
            }
            catch (Exception ex)
            {
                UI.MsgErr(ex.ToString(), "Unexpected Error");
                Close();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                SaveSettings();
            }
            catch (Exception ex)
            {
                UI.MsgErr(ex.ToString(), "Unexpected Error");
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            bool ok = false;
            try
            {
                SaveSettings();
                Backend.Enable_SE_DEBUG();
                if (chkSetTimerResOfCurrProc.Checked) 
                {
                    ok = Backend.EnforceTimerRes();
                    if (!ok) return;
                }
                if (chkSetTimerResOfVmwareVmx.Checked)
                {
                    foreach (var process in Process.GetProcessesByName("vmware-vmx"))
                    {
                        ok = Backend.InjectProcess((UInt32)process.Id);
                        if (!ok) return;
                    }
                }
                if (ok)
                    UI.MsgInfo("Task completed successfully.");
                else
                    UI.MsgErr("There is nothing to do.");
            }
            catch (Exception ex)
            {
                UI.MsgErr(ex.ToString(), "Unexpected Error");
            }
        }

        private void lnkResetLogFilePathToDefault_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtLogFilePath.Text = Settings.DefaultLogFilePath;
            txtLogFilePath.Select();
        }

        private void chkWantLogFile_CheckedChanged(object sender, EventArgs e)
        {
            chkWantDebugLog.Enabled = chkWantLogFile.Checked;
            txtLogFilePath.Enabled = chkWantLogFile.Checked;
        }

        private void btnRestoreDefault_Click(Object sender, EventArgs e)
        {
            try
            {
                Settings.RestoreDefault();
                LoadSettings();
            }
            catch (Exception ex)
            {
                UI.MsgErr(ex.ToString(), "Unexpected Error");
            }
        }
    }
}
