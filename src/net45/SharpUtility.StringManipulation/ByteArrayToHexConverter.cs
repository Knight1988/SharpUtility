﻿using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace SharpUtility.StringManipulation
{
    internal class ByteArrayToHexConverter
    {
        private const string HexAlphabet = "0123456789ABCDEF";

        private static readonly uint[] Lookup32 = Enumerable.Range(0, 256).Select(i =>
        {
            var s = i.ToString("X2");
            return (uint) s[0] + ((uint) s[1] << 16);
        }).ToArray();

        // { "00", "01", ..., "0E", "0F", "10", "11", ..., "FE", "FF" }
        private static readonly string[] HexStringTable =
        {
            "00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "0A", "0B", "0C", "0D", "0E", "0F",
            "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "1A", "1B", "1C", "1D", "1E", "1F",
            "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "2A", "2B", "2C", "2D", "2E", "2F",
            "30", "31", "32", "33", "34", "35", "36", "37", "38", "39", "3A", "3B", "3C", "3D", "3E", "3F",
            "40", "41", "42", "43", "44", "45", "46", "47", "48", "49", "4A", "4B", "4C", "4D", "4E", "4F",
            "50", "51", "52", "53", "54", "55", "56", "57", "58", "59", "5A", "5B", "5C", "5D", "5E", "5F",
            "60", "61", "62", "63", "64", "65", "66", "67", "68", "69", "6A", "6B", "6C", "6D", "6E", "6F",
            "70", "71", "72", "73", "74", "75", "76", "77", "78", "79", "7A", "7B", "7C", "7D", "7E", "7F",
            "80", "81", "82", "83", "84", "85", "86", "87", "88", "89", "8A", "8B", "8C", "8D", "8E", "8F",
            "90", "91", "92", "93", "94", "95", "96", "97", "98", "99", "9A", "9B", "9C", "9D", "9E", "9F",
            "A0", "A1", "A2", "A3", "A4", "A5", "A6", "A7", "A8", "A9", "AA", "AB", "AC", "AD", "AE", "AF",
            "B0", "B1", "B2", "B3", "B4", "B5", "B6", "B7", "B8", "B9", "BA", "BB", "BC", "BD", "BE", "BF",
            "C0", "C1", "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "CA", "CB", "CC", "CD", "CE", "CF",
            "D0", "D1", "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "DA", "DB", "DC", "DD", "DE", "DF",
            "E0", "E1", "E2", "E3", "E4", "E5", "E6", "E7", "E8", "E9", "EA", "EB", "EC", "ED", "EE", "EF",
            "F0", "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "FA", "FB", "FC", "FD", "FE", "FF"
        };

        private static readonly unsafe uint* Lookup32UnsafeP =
            (uint*) GCHandle.Alloc(Lookup32, GCHandleType.Pinned).AddrOfPinnedObject();

        public static unsafe string LookupUnsafe(byte[] bytes)
        {
            var lookupP = Lookup32UnsafeP;
            var result = new char[bytes.Length*2];
            fixed (byte* bytesP = bytes)
            {
                fixed (char* resultP = result)
                {
                    var resultP2 = (uint*) resultP;
                    for (var i = 0; i < bytes.Length; i++)
                        resultP2[i] = lookupP[bytesP[i]];
                }
            }
            return new string(result);
        }

        public static string BitConverter(byte[] bytes)
        {
            var hex = System.BitConverter.ToString(bytes);
            return hex.Replace("-", "");
        }

        public static string StringBuilderAggregateByteToString(byte[] bytes)
        {
            return bytes.Aggregate(new StringBuilder(bytes.Length*2), (sb, b) => sb.Append(b.ToString("X2"))).ToString();
        }

        public static string StringBuilderForEachByteToString(byte[] bytes)
        {
            var hex = new StringBuilder(bytes.Length*2);
            foreach (var b in bytes)
                hex.Append(b.ToString("X2"));
            return hex.ToString();
        }

        public static string StringBuilderAggregateAppendFormat(byte[] bytes)
        {
            return
                bytes.Aggregate(new StringBuilder(bytes.Length*2), (sb, b) => sb.AppendFormat("{0:X2}", b)).ToString();
        }

        public static string StringBuilderForEachAppendFormat(byte[] bytes)
        {
            var hex = new StringBuilder(bytes.Length*2);
            foreach (var b in bytes)
                hex.AppendFormat("{0:X2}", b);
            return hex.ToString();
        }

        public static string ByteManipulation(byte[] bytes)
        {
            var c = new char[bytes.Length*2];
            for (var i = 0; i < bytes.Length; i++)
            {
                var b = (byte) (bytes[i] >> 4);
                c[i*2] = (char) (b > 9 ? b + 0x37 : b + 0x30);
                b = (byte) (bytes[i] & 0xF);
                c[i*2 + 1] = (char) (b > 9 ? b + 0x37 : b + 0x30);
            }
            return new string(c);
        }

        /// <summary>
        ///     Derived from http://stackoverflow.com/a/14333437/48700
        /// </summary>
        public static string ByteManipulation2(byte[] bytes)
        {
            var c = new char[bytes.Length*2];
            for (var i = 0; i < bytes.Length; i++)
            {
                var b = bytes[i] >> 4;
                c[i*2] = (char) (55 + b + (((b - 10) >> 31) & -7));
                b = bytes[i] & 0xF;
                c[i*2 + 1] = (char) (55 + b + (((b - 10) >> 31) & -7));
            }
            return new string(c);
        }

        public static string LookupAndShift(byte[] bytes)
        {
            var result = new StringBuilder(bytes.Length*2);
            foreach (var b in bytes)
            {
                result.Append(HexAlphabet[b >> 4]);
                result.Append(HexAlphabet[b & 0xF]);
            }
            return result.ToString();
        }

        /// <summary>
        ///     Derived from http://stackoverflow.com/a/24343727/48700
        /// </summary>
        public static string LookupPerByte(byte[] bytes)
        {
            var result = new char[bytes.Length*2];
            for (var i = 0; i < bytes.Length; i++)
            {
                var val = Lookup32[bytes[i]];
                result[2*i] = (char) val;
                result[2*i + 1] = (char) (val >> 16);
            }
            return new string(result);
        }

        public static string Lookup(byte[] bytes)
        {
            var result = new StringBuilder(bytes.Length*2);
            foreach (var b in bytes)
                result.Append(HexStringTable[b]);
            return result.ToString();
        }

        public static unsafe string LookupUnsafeDirect(byte[] bytes)
        {
            var lookupP = Lookup32UnsafeP;
            var result = new string((char) 0, bytes.Length*2);
            fixed (byte* bytesP = bytes)
            {
                fixed (char* resultP = result)
                {
                    var resultP2 = (uint*) resultP;
                    for (var i = 0; i < bytes.Length; i++)
                        resultP2[i] = lookupP[bytesP[i]];
                }
            }

            return result;
        }
    }
}