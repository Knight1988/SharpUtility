﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpUtility.Linq
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Adds the specified element at the end of the IEnummerable.
        /// </summary>
        /// <typeparam name="T">The type of elements the IEnumerable contans.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="item">The item to be concatenated.</param>
        /// <returns>An IEnumerable, enumerating first the items in the existing enumerable</returns>
        public static IEnumerable<T> ConcatItem<T>(this IEnumerable<T> target, T item)
        {
            foreach (var t in target) yield return t;
            yield return item;
        }

        /// <summary>
        /// Inserts the specified element at the start of the IEnumerable.
        /// </summary>
        /// <typeparam name="T">The type of elements the IEnumerable contans.</typeparam>
        /// <param name="target">The IEnummerable.</param>
        /// <param name="item">The item to be concatenated.</param>
        /// <returns>An IEnumerable, enumerating first the target elements, and then the new element.</returns>
        public static IEnumerable<T> ConcatTo<T>(this IEnumerable<T> target, T item)
        {
            yield return item;
            foreach (var t in target) yield return t;
        }
    }
}
