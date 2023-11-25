#pragma once
#include <Windows.h>

#define VMABH_API  __stdcall
#define VMABH_RegistryKeyPath  L"SOFTWARE\\MaiSoft\\VMAudioBackHost"

EXTERN_C WCHAR g_thisDllFilePath[MAX_PATH];

EXTERN_C void logging_init(void);
EXTERN_C void logd(WCHAR const *pszFmt, ...);
EXTERN_C void logi(WCHAR const *pszFmt, ...);


EXTERN_C HRESULT VMABH_API VMABH_Enable_SE_DEBUG(void);

EXTERN_C HRESULT VMABH_API VMABH_EnforceTimerRes(void);

EXTERN_C HRESULT VMABH_API VMABH_InjectProcess(DWORD targetPID);
