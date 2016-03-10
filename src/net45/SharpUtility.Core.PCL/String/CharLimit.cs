using System;

namespace SharpUtility.String
{
    [Flags]
    public enum CharLimit
    {
        [StringValue("ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
        UpperCase = 1,
        [StringValue("abcdefghijklmnopqrstuvwxyz")]
        LowerCase = 2,
        [StringValue("0123456789")]
        Number = 4,
        [StringValue("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789")]
        Default = 8
    }
}