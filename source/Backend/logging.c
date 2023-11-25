#include "base.h"
#include <stdio.h>

static WCHAR g_LogFilePath[MAX_PATH];
static BOOL g_WantLogFile;
static BOOL g_WantDebugLog;

EXTERN_C void logging_init(void)
{
	DWORD cbData, dword;
	
	cbData = sizeof g_LogFilePath;
	RegGetValueW(HKEY_CURRENT_USER, VMABH_RegistryKeyPath, L"LogFilePath",
		RRF_RT_REG_SZ | RRF_ZEROONFAILURE, NULL, g_LogFilePath, &cbData);

	cbData = sizeof dword; dword = 0;
	RegGetValueW(HKEY_CURRENT_USER, VMABH_RegistryKeyPath, L"WantLogFile",
		RRF_RT_REG_DWORD | RRF_ZEROONFAILURE, NULL, &dword, &cbData);
	g_WantLogFile = (dword != 0);

	cbData = sizeof dword; dword = 0;
	RegGetValueW(HKEY_CURRENT_USER, VMABH_RegistryKeyPath, L"WantDebugLog",
		RRF_RT_REG_DWORD | RRF_ZEROONFAILURE, NULL, &dword, &cbData);
	g_WantDebugLog = (dword != 0);
}

static void append_log_file(WCHAR const *pszFmt, va_list ap)
{
	HANDLE hFile = CreateFileW(g_LogFilePath, FILE_APPEND_DATA, FILE_SHARE_READ, 0, OPEN_ALWAYS, 0, 0);
	LARGE_INTEGER existing_file_size;
	if ((hFile != INVALID_HANDLE_VALUE) && GetFileSizeEx(hFile, &existing_file_size))
	{
		DWORD const tid = GetCurrentThreadId();
		SYSTEMTIME st = { 0 };
		int cchWrote = 0;
		// wvsprintfW() never write more than 1024 WCHARs
		WCHAR szLine[256 + 1024 + 1] = L"";
		DWORD cbWri = 0;

		GetLocalTime(&st);
		cchWrote += wsprintfW(&szLine[cchWrote], L"%04u-%02u-%02u %02u:%02u:%02u.%03u",
			st.wYear, st.wMonth, st.wDay, st.wHour, st.wMinute, st.wSecond, st.wMilliseconds);
		cchWrote += wsprintfW(&szLine[cchWrote], L" thread[%5lu] ", tid);
		cchWrote += wvsprintfW(&szLine[cchWrote], pszFmt, ap);
		szLine[cchWrote++] = '\r';
		szLine[cchWrote++] = '\n';

		if (existing_file_size.QuadPart == 0)
		{
			WriteFile(hFile, "\xFF\xFE", 2, &cbWri, NULL);
		}
		WriteFile(hFile, szLine, cchWrote * sizeof(WCHAR), &cbWri, NULL);
		CloseHandle(hFile);
	}
}

EXTERN_C void logd(WCHAR const *pszFmt, ...)
{
	if (g_WantLogFile && g_WantDebugLog)
	{
		va_list ap;
		va_start(ap, pszFmt);
		append_log_file(pszFmt, ap);
		va_end(ap);
	}
}

EXTERN_C void logi(WCHAR const *pszFmt, ...)
{
	if (g_WantLogFile)
	{
		va_list ap;
		va_start(ap, pszFmt);
		append_log_file(pszFmt, ap);
		va_end(ap);
	}
}
