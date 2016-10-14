namespace SharpUtility.StringManipulation
{
    public enum ByteArrayToHexMode
    {
        BitConverter,
        ByteManipulation,
        ByteManipulation2,
        Lookup,
        LookupPerByte,
        LookupAndShift,
        LookupUnsafe,
        LookupUnsafeDirect,
        StringBuilderForEachByteToString,
        StringBuilderForEachAppendFormat,
        StringBuilderAggregateByteToString,
        StringBuilderAggregateAppendFormat,
    }
}