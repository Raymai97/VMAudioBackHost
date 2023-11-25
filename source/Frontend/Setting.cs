namespace Frontend
{
    internal class Setting
    {
        public string Name { get; }

        public SettingValueType ValueType { get; }

        public object DefValue { get; }

        public Setting(string name, SettingValueType valueType, object defValue)
        {
            this.Name = name;
            this.ValueType = valueType;
            this.DefValue = defValue;
        }
    }
}
