using SharpUtility.Enum;
using SharpUtility.String;

namespace SharpUtility.Core.Tests.Enum
{
    internal enum TestEnum
    {
        [StringValue("Test1")]
        Test1,
        [StringValue("Test2")]
        Test2
    }
}