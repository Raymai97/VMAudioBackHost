#include "base.h"

EXTERN_C HRESULT VMABH_API VMABH_Enable_SE_DEBUG(void)
{
	HRESULT hr = 0;
	DWORD w32err = 0;
	HANDLE hToken = 0;
	TOKEN_PRIVILEGES tp = { 1 };
	logging_init();
	OpenProcessToken((HANDLE)-1, TOKEN_ADJUST_PRIVILEGES, &hToken);
	if (!hToken)
	{
		w32err = GetLastError();
		logi(L"VMABH_Enable_SE_DEBUG: OpenProcessToken() error %lu", w32err);
		goto eof;
	}
	tp.Privileges[0].Attributes = SE_PRIVILEGE_ENABLED;
	tp.Privileges[0].Luid.LowPart = 20; // SE_DEBUG
	if (!AdjustTokenPrivileges(hToken, FALSE, &tp, 0, NULL, NULL))
	{
		w32err = GetLastError();
		logi(L"VMABH_Enable_SE_DEBUG: AdjustTokenPrivileges() error %lu", w32err);
		goto eof;
	}
eof:
	if (w32err) hr = HRESULT_FROM_WIN32(w32err);
	if (hToken) CloseHandle(hToken);
	return hr;
}


typedef NTSTATUS(WINAPI *NtQueryTimerResolution_t)(ULONG *pMin, ULONG *pMax, ULONG *pCurr);
typedef NTSTATUS(WINAPI *NtSetTimerResolution_t)(ULONG desired, BOOL doSet, ULONG *pCurr);

static ULONG get_desired_res(void)
{
	DWORD dwData = 0;
	DWORD cbData = sizeof dwData;
	RegGetValueW(HKEY_CURRENT_USER, VMABH_RegistryKeyPath, L"DesiredTimerRes",
		RRF_RT_DWORD | RRF_ZEROONFAILURE, NULL, &dwData, &cbData);
	return (ULONG)dwData;
}

EXTERN_C HRESULT VMABH_API VMABH_EnforceTimerRes(void)
{
	HRESULT hr = 0;
	WCHAR ntdll_path[MAX_PATH] = L"";
	HMODULE ntdll_hMod = 0;
	NtQueryTimerResolution_t pfnNtQueryTimerResolution = NULL;
	NtSetTimerResolution_t pfnNtSetTimerResolution = NULL;
	ULONG minRes = 0, maxRes = 0, currRes = 0, desiredRes = 0;
	NTSTATUS ntstatus = 0;
	logging_init();
	logd(L"VMABH_EnforceTimerRes: begin");

	GetSystemDirectoryW(ntdll_path, MAX_PATH);
	lstrcatW(ntdll_path, L"\\ntdll.dll");
	ntdll_hMod = GetModuleHandleW(ntdll_path);
	if (!ntdll_hMod)
	{
		DWORD w32err = GetLastError();
		hr = HRESULT_FROM_WIN32(w32err);
		logi(L"VMABH_EnforceTimerRes: GetModuleHandleW(\"%s\") error %lu", ntdll_path, w32err);
		goto eof;
	}
	pfnNtQueryTimerResolution = (NtQueryTimerResolution_t)GetProcAddress(ntdll_hMod, "NtQueryTimerResolution");
	if (!pfnNtQueryTimerResolution)
	{
		hr = E_NOTIMPL;
		logi(L"VMABH_EnforceTimerRes: NtQueryTimerResolution not found");
		goto eof;
	}
	pfnNtSetTimerResolution = (NtSetTimerResolution_t)GetProcAddress(ntdll_hMod, "NtSetTimerResolution");
	if (!pfnNtSetTimerResolution)
	{
		hr = E_NOTIMPL;
		logi(L"VMABH_EnforceTimerRes: NtSetTimerResolution not found");
		goto eof;
	}

	ntstatus = pfnNtQueryTimerResolution(&minRes, &maxRes, &currRes);
	if (ntstatus)
	{
		hr = E_FAIL;
		logi(L"VMABH_EnforceTimerRes: QueryTimerRes error, ntstatus = %ld", ntstatus);
		goto eof;
	}
	logi(L"VMABH_EnforceTimerRes: QueryTimerRes OK, Min = %lu, Max = %lu, Curr = %lu",
		minRes, maxRes, currRes);

	desiredRes = get_desired_res();
	ntstatus = pfnNtSetTimerResolution(desiredRes, TRUE, &currRes);
	if (ntstatus)
	{
		hr = E_FAIL;
		logi(L"VMABH_EnforceTimerRes: SetTimerRes error, ntstatus = %ld"
			L", Desired = %u", ntstatus, desiredRes);
		goto eof;
	}
	logi(L"VMABH_EnforceTimerRes: SetTimerRes OK, Desired = %u", desiredRes);
eof:
	logd(L"VMABH_EnforceTimerRes: end, hr = 0x%08lX", hr);
	return hr;
}


EXTERN_C HRESULT VMABH_API VMABH_InjectProcess(DWORD targetPID)
{
	HRESULT hr = S_OK;
	DWORD desiredAccess = 0;
	HANDLE targetProcess = 0;
	void *remoteBuffer = NULL;
	HANDLE remoteThread = 0;
	DWORD remoteThreadId = 0;
	DWORD waitResult = 0;
	logging_init();
	logd(L"VMABH_InjectProcess: begin");
	logi(L"VMABH_InjectProcess: targetPID = %lu", targetPID);

	desiredAccess |= PROCESS_QUERY_LIMITED_INFORMATION;
	desiredAccess |= PROCESS_CREATE_THREAD;
	desiredAccess |= PROCESS_VM_OPERATION;
	desiredAccess |= PROCESS_VM_WRITE;
	targetProcess = OpenProcess(desiredAccess, FALSE, targetPID);
	if (!targetProcess)
	{
		hr = HRESULT_FROM_WIN32(GetLastError());
		logi(L"VMABH_InjectProcess: OpenProcess() error, hr = 0x%08lX", hr);
		goto eof;
	}
	logd(L"VMABH_InjectProcess: OpenProcess() OK");

	remoteBuffer = VirtualAllocEx(targetProcess, NULL, sizeof g_thisDllFilePath,
		MEM_COMMIT, PAGE_EXECUTE_READWRITE);
	if (!remoteBuffer)
	{
		hr = HRESULT_FROM_WIN32(GetLastError());
		logi(L"VMABH_InjectProcess: VirtualAllocEx() error, hr = 0x%08lX", hr);
		goto eof;
	}
	logd(L"VMABH_InjectProcess: VirtualAllocEx() OK");

	if (!WriteProcessMemory(targetProcess, remoteBuffer, g_thisDllFilePath,
		sizeof g_thisDllFilePath, NULL))
	{
		hr = HRESULT_FROM_WIN32(GetLastError());
		logi(L"VMABH_InjectProcess: WriteProcessMemory() error, hr = 0x%08lX", hr);
		goto eof;
	}
	logd(L"VMABH_InjectProcess: WriteProcessMemory() OK");

	remoteThread = CreateRemoteThread(targetProcess, NULL, 4096,
		(LPTHREAD_START_ROUTINE)LoadLibraryW, remoteBuffer,
		0, &remoteThreadId);
	if (!remoteThread)
	{
		hr = HRESULT_FROM_WIN32(GetLastError());
		logi(L"VMABH_InjectProcess: CreateRemoteThread() error, hr = 0x%08lX", hr);
		goto eof;
	}
	logd(L"VMABH_InjectProcess: CreateRemoteThread() OK, tid = %lu", remoteThreadId);

	waitResult = WaitForSingleObject(remoteThread, 3000);
	if (waitResult == WAIT_TIMEOUT)
	{
		hr = RPC_E_TIMEOUT;
		logi(L"VMABH_InjectProcess: Thread exec timeout!");
		goto eof;
	}
	logd(L"VMABH_InjectProcess: Thread exec ended within 3 seconds.");
eof:
	if (remoteThread) CloseHandle(remoteThread);
	if (remoteBuffer) VirtualFreeEx(targetProcess, remoteBuffer, 0, MEM_RELEASE);
	if (targetProcess) CloseHandle(targetProcess);
	logd(L"VMABH_InjectProcess: end, hr = 0x%08lX", hr);
	return hr;
}
