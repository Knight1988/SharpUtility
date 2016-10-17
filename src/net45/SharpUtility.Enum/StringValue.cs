using System;

namespace SharpUtility.Enum
{
    public class StringValue : Attribute
    {
        public string Value { get; }

        public StringValue(string value)
        {
            Value = value;
        }
    }
}