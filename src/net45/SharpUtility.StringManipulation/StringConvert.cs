using System;

namespace SharpUtility.StringManipulation
{
    public static class StringConvert
    {
        /// <summary>
        /// Convert byte array to hex
        /// Default: LookupPerByte (fastest and safe in most case)
        /// mode LookupUnsafeDirect is the fastest but unsafe
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteArrayToHex(byte[] bytes)
        {
            return ByteArrayToHex(bytes, ByteArrayToHexMode.LookupPerByte);
        }

        public static string ByteArrayToHex(byte[] bytes, ByteArrayToHexMode mode)
        {
            switch (mode)
            {
                case ByteArrayToHexMode.BitConverter:
                    return ByteArrayToHexConverter.BitConverter(bytes);
                case ByteArrayToHexMode.ByteManipulation:
                    return ByteArrayToHexConverter.ByteManipulation(bytes);
                case ByteArrayToHexMode.ByteManipulation2:
                    return ByteArrayToHexConverter.ByteManipulation2(bytes);

                case ByteArrayToHexMode.Lookup:
                    return ByteArrayToHexConverter.Lookup(bytes);
                case ByteArrayToHexMode.LookupPerByte:
                    return ByteArrayToHexConverter.LookupPerByte(bytes);
                case ByteArrayToHexMode.LookupAndShift:
                    return ByteArrayToHexConverter.LookupAndShift(bytes);
                case ByteArrayToHexMode.LookupUnsafe:
                    return ByteArrayToHexConverter.LookupUnsafe(bytes);
                case ByteArrayToHexMode.LookupUnsafeDirect:
                    return ByteArrayToHexConverter.LookupUnsafeDirect(bytes);

                case ByteArrayToHexMode.StringBuilderForEachByteToString:
                    return ByteArrayToHexConverter.StringBuilderForEachByteToString(bytes);
                case ByteArrayToHexMode.StringBuilderForEachAppendFormat:
                    return ByteArrayToHexConverter.StringBuilderForEachAppendFormat(bytes);
                case ByteArrayToHexMode.StringBuilderAggregateByteToString:
                    return ByteArrayToHexConverter.StringBuilderAggregateByteToString(bytes);
                case ByteArrayToHexMode.StringBuilderAggregateAppendFormat:
                    return ByteArrayToHexConverter.StringBuilderAggregateAppendFormat(bytes);
                default:
                    return ByteArrayToHexConverter.LookupPerByte(bytes);
            }
        }

        public static byte[] StringToBytes(string str)
        {
            var bytes = new byte[str.Length*sizeof(char)];
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        public static string BytesToString(byte[] bytes)
        {
            var chars = new char[bytes.Length/sizeof(char)];
            Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
        }
    }
}