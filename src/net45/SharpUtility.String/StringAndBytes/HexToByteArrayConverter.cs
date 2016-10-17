using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SharpUtility.StringManipulation
{
    internal static class HexToByteArrayConverter
    {
        public static byte[] Enumerable(string hex)
        {
            return System.Linq.Enumerable.Range(0, hex.Length)
                .Where(x => x%2 == 0)
                .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                .ToArray();
        }

        public static byte[] ConvertToByte(string hex)
        {
            var numberChars = hex.Length;
            var bytes = new byte[numberChars/2];
            for (var i = 0; i < numberChars; i += 2)
                bytes[i/2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        public static byte[] Lookup(string hex)
        {
            var bytes = new byte[hex.Length/2];
            int[] hexValue =
            {
                0x00, 0x01, 0x02, 0x03, 0x04, 0x05,
                0x06, 0x07, 0x08, 0x09, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F
            };

            for (int x = 0, i = 0; i < hex.Length; i += 2, x += 1)
                bytes[x] = (byte) ((hexValue[char.ToUpper(hex[i + 0]) - '0'] << 4) |
                                   hexValue[char.ToUpper(hex[i + 1]) - '0']);

            return bytes;
        }

        public static byte[] ByteCasting(string hex)
        {
            var arr = new byte[hex.Length >> 1];

            for (var i = 0; i < hex.Length >> 1; ++i)
                arr[i] = (byte) ((GetHexVal(hex[i << 1]) << 4) + GetHexVal(hex[(i << 1) + 1]));

            return arr;
        }

        public static byte[] ByteParse(string hexString)
        {
            var hexAsBytes = new byte[hexString.Length/2];
            for (var index = 0; index < hexAsBytes.Length; index++)
            {
                var byteValue = hexString.Substring(index*2, 2);
                hexAsBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return hexAsBytes;
        }

        public static byte[] DictionaryAndList(string str)
        {
            var hexindex = new Dictionary<string, byte>();
            for (var i = 0; i <= 255; i++)
                hexindex.Add(i.ToString("X2"), (byte) i);

            var hexres = new List<byte>();
            for (var i = 0; i < str.Length; i += 2)
                hexres.Add(hexindex[str.Substring(i, 2)]);

            return hexres.ToArray();
        }

        public static byte[] Complex(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;

            var stringLength = value.Length;
            var characterIndex = value.StartsWith("0x", StringComparison.Ordinal) ? 2 : 0;
            // Does the string define leading HEX indicator '0x'. Adjust starting index accordingly.               
            var numberOfCharacters = stringLength - characterIndex;

            var addLeadingZero = false;
            if (0 != numberOfCharacters%2)
            {
                addLeadingZero = true;

                numberOfCharacters += 1; // Leading '0' has been striped from the string presentation.
            }

            var bytes = new byte[numberOfCharacters/2];

            var writeIndex = 0;
            if (addLeadingZero)
            {
                bytes[writeIndex++] = FromCharacterToByte(value[characterIndex], characterIndex);
                characterIndex += 1;
            }

            for (var readIndex = characterIndex; readIndex < value.Length; readIndex += 2)
            {
                var upper = FromCharacterToByte(value[readIndex], readIndex, 4);
                var lower = FromCharacterToByte(value[readIndex + 1], readIndex + 1);

                bytes[writeIndex++] = (byte) (upper | lower);
            }

            return bytes;
        }

        private static byte FromCharacterToByte(char character, int index, int shift = 0)
        {
            var value = (byte) character;
            if (((0x40 < value) && (0x47 > value)) || ((0x60 < value) && (0x67 > value)))
            {
                if (0x40 != (0x40 & value)) return value;

                if (0x20 == (0x20 & value))
                    value = (byte) ((value + 0xA - 0x61) << shift);
                else
                    value = (byte) ((value + 0xA - 0x41) << shift);
            }
            else if ((0x29 < value) && (0x40 > value))
                value = (byte) ((value - 0x30) << shift);
            else
                throw new InvalidOperationException(
                    $"Character '{character}' at index '{index}' is not valid alphanumeric character.");

            return value;
        }

        private static int GetHexVal(char hex)
        {
            var val = (int) hex;
            //For uppercase A-F letters:
            return val - (val < 58 ? 48 : 55);
            //For lowercase a-f letters:
            //return val - (val < 58 ? 48 : 87);
            //Or the two combined, but a bit slower:
            //return val - (val < 58 ? 48 : (val < 97 ? 55 : 87));
        }
    }
}