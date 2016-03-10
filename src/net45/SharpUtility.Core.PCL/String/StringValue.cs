using System;

namespace SharpUtility.String
{
    public class StringValue : Attribute
    {
        private readonly string _value;

        public string Value
        {
            get { return _value; }
        }

        public StringValue(string value)
        {
            _value = value;
        }
    }
}