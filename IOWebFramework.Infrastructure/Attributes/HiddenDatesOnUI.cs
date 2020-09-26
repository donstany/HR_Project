using System;

namespace IOWebFramework.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class HiddenDatesOnUI : Attribute
    {
        public HiddenDatesOnUI()
        {
        }
    }
}
