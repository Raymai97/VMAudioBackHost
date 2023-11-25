namespace Frontend
{
    internal enum SettingValueType
    {
        /// <summary>
        /// String that may contain %env_var%.
        /// </summary>
        EXPAND_SZ,
        /// <summary>
        /// 32-bit unsigned int
        /// </summary>
        DWORD,
        /// <summary>
        /// 32-bit unsigned int, specialized for boolean
        /// </summary>
        DWORD_bool,
    }
}
