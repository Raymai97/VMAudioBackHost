using System;
using System.Runtime.InteropServices;

namespace Frontend
{
    internal class Backend
    {
        private const string BackendDllName = "VMAudioBackHost.dll";


        [DllImport(BackendDllName)]
        private static extern Int32 VMABH_EnforceTimerRes();
        
        public delegate void OnErrorEnforceTimerRes_t(Int32 hr);

        public event OnErrorEnforceTimerRes_t OnErrorEnforceTimerRes;

        public bool EnforceTimerRes()
        {
            Int32 hr = VMABH_EnforceTimerRes();
            if (hr < 0)
            {
                OnErrorEnforceTimerRes?.Invoke(hr);
                return false;
            }
            return true;
        }


        [DllImport(BackendDllName)]
        private static extern Int32 VMABH_InjectProcess(UInt32 processId);

        public delegate void OnErrorInjectProcess_t(Int32 hr, UInt32 processId);

        public event OnErrorInjectProcess_t OnErrorInjectProcess;

        public bool InjectProcess(UInt32 processId)
        {
            Int32 hr = VMABH_InjectProcess(processId);
            if (hr < 0)
            {
                OnErrorInjectProcess?.Invoke(hr, processId);
                return false;
            }
            return true;
        }


        [DllImport(BackendDllName)]
        private static extern Int32 VMABH_Enable_SE_DEBUG();

        public delegate void OnErrorEnable_SE_DEBUG_t(Int32 hr);

        public event OnErrorEnable_SE_DEBUG_t OnErrorEnable_SE_DEBUG;

        public bool Enable_SE_DEBUG()
        {
            Int32 hr = VMABH_Enable_SE_DEBUG();
            if (hr < 0)
            {
                OnErrorEnable_SE_DEBUG?.Invoke(hr);
                return false;
            }
            return true;
        }
    }
}
