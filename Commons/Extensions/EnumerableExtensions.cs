﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using CG.Commons.Collections;

namespace CG.Commons.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool IsCollectionsEqual<T>(this ICollection<T> collectionA, ICollection<T> collectionB)
        {
            return collectionA.Count == collectionB.Count && !collectionA.Except(collectionB).Any() && !collectionB.Except(collectionA).Any();
        }

        /// <summary>
        /// Performs the action on each item on the collection.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach (var t in collection)
            {
                action(t);
            }
        }

        /// <summary>
        /// Converts the collection to a HashSet.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> collection)
        {
            return new HashSet<T>(collection);
        }

        /// <summary>
        /// Returns the index of the first element in a sequence that satisifies a specified condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static int IndexOf<T>(this IEnumerable<T> collection, Func<T, bool> selector)
        {
            var index = 0;
            foreach (var t in collection)
            {
                if (selector(t)) return index;
                index++;
            }
            return -1;
        }

        /// <summary>
        /// This method will return a SQL query formatted comma delimited list of value from a collection.
        /// </summary>
        /// <example>
        /// The collection { "Hello World", 17, 129.323 } will return the string: "('Hello World', 17, 129.323)".
        /// </example>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source">The list to convert to a query list string.</param>
        /// <param name="parentheses">Optional: True if opening and closing parentheses should be appended to the string.</param>
        /// /// <param name="empty">Optional: Specifies what to return if the collection is empty. The default is '' for string and 0 for numeric.</param>
        /// <returns></returns>
        public static String ToSqlFormatString<TSource>(this IEnumerable<TSource> source, bool parentheses = true, string empty = null)
        {
            var objectType = typeof(TSource);
            bool isNumeric = (objectType == typeof(int) || objectType == typeof(long) || objectType == typeof(double) || objectType == typeof(float));
            var sb = new StringBuilder();
            if (parentheses) sb.Append("(");
            if (!source.Any())
            {
                var emptyString = empty ?? (isNumeric ? "0" : "''");
                sb.Append(emptyString);
            }
            else
            {
                using (var enumerator = source.GetEnumerator())
                {
                    if (enumerator.MoveNext())
                    {
                        bool isLast;
                        do
                        {
                            var current = enumerator.Current;
                            if (isNumeric)
                            {
                                sb.Append(current);
                            }
                            else
                            {
                                sb.Append("'" + current + "'");
                            }
                            isLast = !enumerator.MoveNext();
                            if (!isLast)
                            {
                                sb.Append(", ");
                            }
                        } while (!isLast);
                    }
                }
            }
            if (parentheses) sb.Append(")");
            return sb.ToString();
        }

        /// <summary>
        /// Adds the elements of the specified collection to the end of the <see cref="ObservableCollection{T}"/>
        /// </summary>
        /// <typeparam name="TValue">The type of the collection.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="items">The elements to add to the collection.</param>
        public static void AddRange<TValue>(this ObservableCollection<TValue> collection, IEnumerable<TValue> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        public static void AddRange<TValue>(this ICollection<TValue> collection, IEnumerable<TValue> items)
        {
            foreach (var item in items)
            {
                collection.Add(item);
            }
        }

        public static Collectionary<TKey, TValue> ToCollectionary<TKey, TValue>(this IEnumerable<TValue> collection, Func<TValue, TKey> keySelector, bool makeUnique = false)
        {
            var collectionary = new Collectionary<TKey, TValue>(makeUnique);
            foreach (var item in collection)
            {
                collectionary.Add(keySelector(item), item);
            }
            return collectionary;
        }

        public static Collectionary<TKey, TValue> ToCollectionary<TKey, TValue>(this IEnumerable<TValue> collection, Func<TValue, TKey> keySelector, IEqualityComparer<TValue> comparer)
        {
            var collectionary = new Collectionary<TKey, TValue>(comparer);
            foreach (var item in collection)
            {
                collectionary.Add(keySelector(item), item);
            }
            return collectionary;
        }
    }
}
