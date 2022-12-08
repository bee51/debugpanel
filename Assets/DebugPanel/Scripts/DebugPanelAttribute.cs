using System;

namespace DebugPanel.Scripts
{
    [AttributeUsage(AttributeTargets.Field)]
    public class DebugPanelAttribute : Attribute
    {
        public string DisplayValue;
        public string DisplayName;
        public bool IsString;


        public DebugPanelAttribute(string displayValue, string displayName,bool isString=false)
        {
            DisplayValue = displayValue;
            DisplayName = displayName;
            IsString = isString;
        }
    }
}