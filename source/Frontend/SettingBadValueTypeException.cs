using System;

namespace Frontend
{
    internal class SettingBadValueTypeException : Exception
    {
        public Setting Setting { get; }
        public SettingBadValueTypeException(Setting setting)
        {
            this.Setting = setting;
        }
    }
}
