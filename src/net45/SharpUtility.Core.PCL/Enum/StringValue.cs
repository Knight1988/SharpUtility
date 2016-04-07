using System;

namespace SharpUtility.Enum
{
    public class StringValue : Attribute
    {
        private readonly string _value;

        public string Value => _value;

        public StringValue(string value)
        {
            _value = value;
        }
    }
}