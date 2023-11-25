using Microsoft.Win32;
using System;

namespace Frontend
{
    internal class Settings
    {
        private const string RegKeyPath = "Software\\MaiSoft\\VMAudioBackHost";

        private object RegGet(Setting setting)
        {
            object valData;
            try
            {
                var key = Registry.CurrentUser.OpenSubKey(RegKeyPath);
                valData = key?.GetValue(setting.Name, null, RegistryValueOptions.DoNotExpandEnvironmentNames);
            }
            catch (Exception ex)
            {
                OnErrorLoadValue?.Invoke(setting, ex);
                return setting.DefValue;
            }
            if (valData == null)
            {
                OnExistingValueNull?.Invoke(setting);
                return setting.DefValue;
            }
            try
            {
                if (setting.ValueType == SettingValueType.EXPAND_SZ)
                {
                    return (string)valData;
                }
                else if (setting.ValueType == SettingValueType.DWORD)
                {
                    return (UInt32)(Int32)valData;
                }
                else if (setting.ValueType == SettingValueType.DWORD_bool)
                {
                    return (Int32)valData == 1;
                }
            }
            catch (InvalidCastException)
            {
                OnExistingValueWrongType?.Invoke(setting);
                return setting.DefValue;
            }
            throw new SettingBadValueTypeException(setting);
        }

        private void RegSet(Setting setting, object newValue)
        {
            RegistryValueKind kind;
            if (setting.ValueType == SettingValueType.EXPAND_SZ)
            {
                newValue = (string)newValue;
                kind = RegistryValueKind.ExpandString;
            }
            else if (setting.ValueType == SettingValueType.DWORD)
            {
                newValue = (UInt32)newValue;
                kind = RegistryValueKind.DWord;
            }
            else if (setting.ValueType == SettingValueType.DWORD_bool)
            {
                newValue = ((bool)newValue) ? 1 : 0;
                kind = RegistryValueKind.DWord;
            }
            else
            {
                throw new SettingBadValueTypeException(setting);
            }
            try
            {
                var key = Registry.CurrentUser.CreateSubKey(RegKeyPath);
                if (key == null)
                    throw new Exception("RegistryKey.CreateSubKey() returned null" +
                        " without throwing exception. This should never happen.");
                else
                    key.SetValue(setting.Name, newValue, kind);
            }
            catch (Exception ex)
            {
                OnErrorSaveValue?.Invoke(setting, ex);
            }
        }

        public void RestoreDefault()
        {
            DesiredTimerRes = (UInt32)_DesiredTimerRes.DefValue;
            WantSetTimerResOfCurrProc = (bool)_WantSetTimerResOfCurrProc.DefValue;
            WantSetTimerResOfVmwareVmx = (bool)_WantSetTimerResOfVmwareVmx.DefValue;
            LogFilePath = (string)_LogFilePath.DefValue;
            WantLogFile = (bool)_WantLogFile.DefValue;
            WantDebugLog = (bool)_WantDebugLog.DefValue;
        }


        // System API expects value in 100-ns units. For example, 5000 means 0.5 milliseconds.
        private Setting _DesiredTimerRes = new Setting("DesiredTimerRes", SettingValueType.DWORD, 10000u);
        public UInt32 DesiredTimerRes
        {
            get => (UInt32)RegGet(_DesiredTimerRes);
            set => RegSet(_DesiredTimerRes, value);
        }

        private Setting _WantSetTimerResOfCurrProc = new Setting("WantSetTimerResOfCurrProc", SettingValueType.DWORD_bool, false);
        public bool WantSetTimerResOfCurrProc
        {
            get => (bool)RegGet(_WantSetTimerResOfCurrProc);
            set => RegSet(_WantSetTimerResOfCurrProc, value);
        }

        private Setting _WantSetTimerResOfVmwareVmx = new Setting("WantSetTimerResOfVmwareVmx", SettingValueType.DWORD_bool, true);
        public bool WantSetTimerResOfVmwareVmx
        {
            get => (bool)RegGet(_WantSetTimerResOfVmwareVmx);
            set => RegSet(_WantSetTimerResOfVmwareVmx, value);
        }

        public const string DefaultLogFilePath = "%localappdata%\\VMAudioBackHost.log";
        private Setting _LogFilePath = new Setting("LogFilePath", SettingValueType.EXPAND_SZ, DefaultLogFilePath);
        public string LogFilePath
        {
            get => (string)RegGet(_LogFilePath);
            set => RegSet(_LogFilePath, value);
        }

        private Setting _WantLogFile = new Setting("WantLogFile", SettingValueType.DWORD_bool, true);
        public bool WantLogFile
        {
            get => (bool)RegGet(_WantLogFile);
            set => RegSet(_WantLogFile, value);
        }

        private Setting _WantDebugLog = new Setting("WantDebugLog", SettingValueType.DWORD_bool, false);
        public bool WantDebugLog
        {
            get => (bool)RegGet(_WantDebugLog);
            set => RegSet(_WantDebugLog, value);
        }

        public delegate void OnExistingValueNull_t(Setting setting);
        public event OnExistingValueNull_t OnExistingValueNull;

        public delegate void OnExistingValueWrongType_t(Setting setting);
        public event OnExistingValueWrongType_t OnExistingValueWrongType;

        public delegate void OnErrorLoadValue_t(Setting setting, Exception ex);
        public event OnErrorLoadValue_t OnErrorLoadValue;

        public delegate void OnErrorSaveValue_t(Setting setting, Exception ex);
        public event OnErrorSaveValue_t OnErrorSaveValue;
    }
}
