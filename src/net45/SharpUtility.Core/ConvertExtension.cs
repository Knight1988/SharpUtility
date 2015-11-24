using System;

namespace SharpUtility.Core
{
    public static class ConvertExtensions
    {
        /// <summary>
        /// Try parse and return default if fail
        /// </summary>
        /// <typeparam name="T">source type</typeparam>
        /// <typeparam name="V">target type</typeparam>
        /// <param name="source">source input</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>converted input or default value</returns>
        public static V TryParse<T, V>(this T source, V defaultValue) where V : class
        {
            if (source == null) return defaultValue;

            var temp = source as V;
            return temp ?? defaultValue;
        }

        /// <summary>
        /// Try parse and return default if fail
        /// </summary>
        /// <typeparam name="T">source type</typeparam>
        /// <param name="source">source input</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>converted input or default value</returns>
        public static DateTime TryParse<T>(this T source, DateTime defaultValue)
        {
            if (source == null) return defaultValue;

            try
            {
                DateTime temp;
                if (source is string)
                    temp = DateTime.Parse(source as string);
                else
                    temp = DateTime.Parse(source.ToString());

                return temp;
            }
            catch
            {
                // ignored
            }
            return defaultValue;
        }

        /// <summary>
        /// Try parse and return default if fail
        /// </summary>
        /// <typeparam name="T">source type</typeparam>
        /// <param name="source">source input</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>converted input or default value</returns>
        public static byte TryParse<T>(this T source, byte defaultValue)
        {
            if (source == null) return defaultValue;

            try
            {
                byte temp;
                if (source is string)
                    temp = byte.Parse(source as string);
                else
                    temp = byte.Parse(source.ToString());

                return temp;
            }
            catch
            {
                // ignored
            }
            return defaultValue;
        }

        /// <summary>
        /// Try parse and return default if fail
        /// </summary>
        /// <typeparam name="T">source type</typeparam>
        /// <param name="source">source input</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>converted input or default value</returns>
        public static sbyte TryParse<T>(this T source, sbyte defaultValue)
        {
            if (source == null) return defaultValue;

            try
            {
                sbyte temp;
                if (source is string)
                    temp = sbyte.Parse(source as string);
                else
                    temp = sbyte.Parse(source.ToString());

                return temp;
            }
            catch
            {
                // ignored
            }
            return defaultValue;
        }


        /// <summary>
        /// Try parse and return default if fail
        /// </summary>
        /// <typeparam name="T">source type</typeparam>
        /// <param name="source">source input</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>converted input or default value</returns>
        public static short TryParse<T>(this T source, short defaultValue)
        {
            if (source == null) return defaultValue;

            try
            {
                short temp;
                if (source is string)
                    temp = short.Parse(source as string);
                else
                    temp = short.Parse(source.ToString());

                return temp;
            }
            catch
            {
                // ignored
            }
            return defaultValue;
        }


        /// <summary>
        /// Try parse and return default if fail
        /// </summary>
        /// <typeparam name="T">source type</typeparam>
        /// <param name="source">source input</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>converted input or default value</returns>
        public static ushort TryParse<T>(this T source, ushort defaultValue)
        {
            if (source == null) return defaultValue;

            try
            {
                ushort temp;
                if (source is string)
                    temp = ushort.Parse(source as string);
                else
                    temp = ushort.Parse(source.ToString());

                return temp;
            }
            catch
            {
                // ignored
            }
            return defaultValue;
        }


        /// <summary>
        /// Try parse and return default if fail
        /// </summary>
        /// <typeparam name="T">source type</typeparam>
        /// <param name="source">source input</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>converted input or default value</returns>
        public static int TryParse<T>(this T source, int defaultValue)
        {
            if (source == null) return defaultValue;

            try
            {
                int temp;
                if (source is string)
                    temp = int.Parse(source as string);
                else
                    temp = int.Parse(source.ToString());

                return temp;
            }
            catch
            {
                // ignored
            }
            return defaultValue;
        }


        /// <summary>
        /// Try parse and return default if fail
        /// </summary>
        /// <typeparam name="T">source type</typeparam>
        /// <param name="source">source input</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>converted input or default value</returns>
        public static uint TryParse<T>(this T source, uint defaultValue)
        {
            if (source == null) return defaultValue;

            try
            {
                uint temp;
                if (source is string)
                    temp = uint.Parse(source as string);
                else
                    temp = uint.Parse(source.ToString());

                return temp;
            }
            catch
            {
                // ignored
            }
            return defaultValue;
        }


        /// <summary>
        /// Try parse and return default if fail
        /// </summary>
        /// <typeparam name="T">source type</typeparam>
        /// <param name="source">source input</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>converted input or default value</returns>
        public static long TryParse<T>(this T source, long defaultValue)
        {
            if (source == null) return defaultValue;

            try
            {
                long temp;
                if (source is string)
                    temp = long.Parse(source as string);
                else
                    temp = long.Parse(source.ToString());

                return temp;
            }
            catch
            {
                // ignored
            }
            return defaultValue;
        }


        /// <summary>
        /// Try parse and return default if fail
        /// </summary>
        /// <typeparam name="T">source type</typeparam>
        /// <param name="source">source input</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>converted input or default value</returns>
        public static ulong TryParse<T>(this T source, ulong defaultValue)
        {
            if (source == null) return defaultValue;

            try
            {
                ulong temp;
                if (source is string)
                    temp = ulong.Parse(source as string);
                else
                    temp = ulong.Parse(source.ToString());

                return temp;
            }
            catch
            {
                // ignored
            }
            return defaultValue;
        }


        /// <summary>
        /// Try parse and return default if fail
        /// </summary>
        /// <typeparam name="T">source type</typeparam>
        /// <param name="source">source input</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>converted input or default value</returns>
        public static float TryParse<T>(this T source, float defaultValue)
        {
            if (source == null) return defaultValue;

            try
            {
                float temp;
                if (source is string)
                    temp = float.Parse(source as string);
                else
                    temp = float.Parse(source.ToString());

                return temp;
            }
            catch
            {
                // ignored
            }
            return defaultValue;
        }


        /// <summary>
        /// Try parse and return default if fail
        /// </summary>
        /// <typeparam name="T">source type</typeparam>
        /// <param name="source">source input</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>converted input or default value</returns>
        public static double TryParse<T>(this T source, double defaultValue)
        {
            if (source == null) return defaultValue;

            try
            {
                double temp;
                if (source is string)
                    temp = double.Parse(source as string);
                else
                    temp = double.Parse(source.ToString());

                return temp;
            }
            catch
            {
                // ignored
            }
            return defaultValue;
        }


        /// <summary>
        /// Try parse and return default if fail
        /// </summary>
        /// <typeparam name="T">source type</typeparam>
        /// <param name="source">source input</param>
        /// <param name="defaultValue">default value</param>
        /// <returns>converted input or default value</returns>
        public static decimal TryParse<T>(this T source, decimal defaultValue)
        {
            if (source == null) return defaultValue;

            try
            {
                decimal temp;
                if (source is string)
                    temp = decimal.Parse(source as string);
                else
                    temp = decimal.Parse(source.ToString());

                return temp;
            }
            catch
            {
                // ignored
            }
            return defaultValue;
        }
    }
}
