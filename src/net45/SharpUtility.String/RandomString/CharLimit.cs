using System;
using SharpUtility.Enum;

namespace SharpUtility.StringManipulation
{
    [Flags]
    public enum CharLimit
    {
        /// <summary>
        /// Uppercase chars
        /// </summary>
        [StringValue("ABCDEFGHIJKLMNOPQRSTUVWXYZ")]
        UpperCase = 1,
        /// <summary>
        /// Lowercase chars
        /// </summary>
        [StringValue("abcdefghijklmnopqrstuvwxyz")]
        LowerCase = 2,
        /// <summary>
        /// Digitals
        /// </summary>
        [StringValue("0123456789")]
        Number = 4,
        /// <summary>
        /// Contain all
        /// </summary>
        [StringValue("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789")]
        Default = 8
    }
}