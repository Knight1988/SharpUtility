namespace SharpUtility.Enum
{
    public static class EnumExtensions
    {
        /// <summary>
        ///     Get string value from enum
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStringValue(this System.Enum value)
        {
            string output = null;
            var type = value.GetType();

            //Check first in our cached results...

            //Look for our 'StringValueAttribute' 

            //in the field's custom attributes

            var fi = type.GetField(value.ToString());
            var attrs =
                fi.GetCustomAttributes(typeof (StringValue),
                    false) as StringValue[];
            if (attrs != null && attrs.Length > 0)
            {
                output = attrs[0].Value;
            }

            return output;
        }
    }
}