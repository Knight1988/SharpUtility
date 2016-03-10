namespace SharpUtility.Math
{
    public static class MathExtensions
    {
        #region IsEven

        public static bool IsEven(this byte value)
        {
            return !value.IsOdd();
        }


        public static bool IsEven(this sbyte value)
        {
            return !value.IsOdd();
        }

        public static bool IsEven(this short value)
        {
            return !value.IsOdd();
        }


        public static bool IsEven(this ushort value)
        {
            return !value.IsOdd();
        }

        public static bool IsEven(this int value)
        {
            return !value.IsOdd();
        }

        public static bool IsEven(this uint value)
        {
            return !value.IsOdd();
        }

        public static bool IsEven(this long value)
        {
            return !value.IsOdd();
        }

        public static bool IsEven(this ulong value)
        {
            return !value.IsOdd();
        }

        public static bool IsEven(this float value)
        {
            return !value.IsOdd();
        }

        public static bool IsEven(this double value)
        {
            return !value.IsOdd();
        }

        public static bool IsEven(this decimal value)
        {
            return !value.IsOdd();
        }

        #endregion

        #region IsOdd

        public static bool IsOdd(this byte value)
        {
            return value%2 != 0;
        }


        public static bool IsOdd(this sbyte value)
        {
            return value%2 != 0;
        }

        public static bool IsOdd(this short value)
        {
            return value%2 != 0;
        }


        public static bool IsOdd(this ushort value)
        {
            return value%2 != 0;
        }

        public static bool IsOdd(this int value)
        {
            return value%2 != 0;
        }

        public static bool IsOdd(this uint value)
        {
            return value%2 != 0;
        }

        public static bool IsOdd(this long value)
        {
            return value%2 != 0;
        }

        public static bool IsOdd(this ulong value)
        {
            return value%2 != 0;
        }

        public static bool IsOdd(this float value)
        {
            return System.Math.Abs(value%2) > double.Epsilon;
        }

        public static bool IsOdd(this double value)
        {
            return System.Math.Abs(value%2) > double.Epsilon;
        }

        public static bool IsOdd(this decimal value)
        {
            return value%2 != 0;
        }

        #endregion
    }
}