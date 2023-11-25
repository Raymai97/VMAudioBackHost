#include "base.h"

WCHAR g_thisDllFilePath[MAX_PATH];
static WCHAR g_currentProcessFilePath[MAX_PATH];
static PCWSTR g_currentProcessFileName;
static void init_currentProcess(HINSTANCE hInstDLL)
{
	WCHAR *p, *pSlash = NULL;
	GetModuleFileNameW(hInstDLL, g_thisDllFilePath, MAX_PATH);
	GetModuleFileNameW(0, g_currentProcessFilePath, MAX_PATH);
	for (p = g_currentProcessFilePath; *p; ++p)
	{
		if (*p == '\\') pSlash = p;
	}
	g_currentProcessFileName = pSlash ? &pSlash[1] : L"";
}


BOOL WINAPI DllMain(HINSTANCE hInstDLL, DWORD reason, LPVOID reserved)
{
	UNREFERENCED_PARAMETER(reserved);
	if (reason == DLL_PROCESS_ATTACH)
	{
		logging_init();
		init_currentProcess(hInstDLL);
		logi(L"DllMain: Process ID = %lu", GetCurrentProcessId());
		logd(L"DllMain: Process = \"%s\"", g_currentProcessFilePath);
		
		if (lstrcmpiW(g_currentProcessFileName, L"VMAudioBackHost.exe") == 0)
			return TRUE;

		// If this DLL is not loaded by Frontend, it is injected to remote process
		// to enforce timer resolution of the remote process.
		VMABH_EnforceTimerRes();
	}
	return FALSE; // to prevent DLL stay in memory
}
