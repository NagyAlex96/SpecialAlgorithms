using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SpecialAlgorithms.Assets
{
    //TODO: refaktorálás
    //partial class ha kell
    //paraméterek nevei kisbetűsök
    //nevek, változók, függvények illetve minden egyébnek ellenőrizni kell, hogy jól van-e írva a neve (kis és nagybetű)
    //kommentezés
    //kivételek?

    //TODO: fennmaradó algoritmusok:
    // Optimális megoldást visszaadó visszalépéses keresés


    public static partial class Algorithms
    {
        /// <summary>
        /// Determine whether a collection has at least one element with a give condition
        /// </summary>
        /// <param name="collection">Examined collection</param>
        /// <param name="condition">Conditional that returns true or false each value of T type</param>
        /// <returns><c>True</c> if at least 1 element meets the condition</returns>
        public static bool Decision<T>(this IEnumerable<T> collection, Func<T, bool> condition) where T : IComparable<T>
        {
            foreach (var item in collection)
            {
                if (condition(item))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determine whether a collection has at least one element with a give condition
        /// </summary>
        /// <param name="collection">Examined collection</param>
        /// <param name="seachedItem">Searched element</param>
        /// <returns><c>True</c> if the collection has at least one searched element</returns>
        public static bool Decision<T>(this IEnumerable<T> collection, T seachedItem) where T : IComparable<T>
        {
            foreach (var item in collection)
            {
                if (item.CompareTo(seachedItem) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Determine whether a collection has at least one element with a give condition
        /// </summary>
        /// <param name="collection">Examined collection</param>
        /// <param name="condition">Conditional that returns true or false each value of T type</param>
        /// <returns><c>True</c> + <c>Index</c> if at least one element exists in the collection with the given condition</returns>
        public static (bool, int) LinearSearch<T>(this IEnumerable<T> collection, Func<T, bool> condition) where T : IComparable<T>
        {
            int index = 0;
            foreach (var item in collection)
            {
                if (condition(item))
                {
                    return (true, index);
                }
                index++;
            }
            return (false, -1);
        }

        /// <summary>
        /// Determine whether a collection has at least one element with a give condition
        /// </summary>
        /// <param name="collection">Examined collection</param>
        /// <param name="seachedItem">Searched element</param>
        /// <returns><c>True</c> + <c>Index</c> if at least one element exists in the collection with the given condition</returns>
        public static (bool, int) LinearSearch<T>(this IEnumerable<T> collection, T seachedItem) where T : IComparable<T>
        {
            int index = 0;
            foreach (var item in collection)
            {
                if (item.CompareTo(seachedItem) == 0)
                {
                    return (true, index);
                }
                index++;
            }
            return (false, -1);
        }

        /// <summary>
        /// Determine the number of the elements in the collection with the given condition
        /// </summary>
        /// <param name="collection">Examined collection</param>
        /// <param name="condition">Conditional that returns true or false each value of T type</param>
        /// <returns><c>0</c> if there is no element with the given condition</returns>
        public static int ElementCounter<T>(this IEnumerable<T> collection, Func<T, bool> condition) where T : IComparable<T>
        {
            int counter = 0;
            foreach (var item in collection)
            {
                if (condition(item))
                {
                    counter++;
                }
            }
            return counter;
        }

        /// <summary>
        /// Determine the number of the searched item in the collection
        /// </summary>
        /// <param name="collection">Examined collection</param>
        /// <param name="seachedItem">Searched element</param>
        /// <returns><c>0</c> if there is no searched element</returns>
        public static int ElementCounter<T>(this IEnumerable<T> collection, T seachedItem) where T : IComparable<T>
        {
            int counter = 0;
            foreach (var item in collection)
            {
                if (item.CompareTo(seachedItem) == 0)
                {
                    counter++;
                }
            }
            return counter;
        }

        /// <summary>
        /// Search for the largest element of the collection
        /// </summary>
        /// <param name="collection">Examined collection</param>
        /// <returns>The largest element position <c>index</c> and <c>value</c></returns>
        public static (int, T) MaxSearch<T>(this IEnumerable<T> collection) where T : IComparable<T>
        {
            int maxIndex = -1;
            int index = -1;
            T maxValue = collection.ElementAt(0);

            foreach (var item in collection)
            {
                index++;

                if (item.CompareTo(maxValue) > 0)
                {
                    maxValue = item;
                    maxIndex = index;
                }
            }
            return (maxIndex, maxValue);
        }

        /// <summary>
        /// If the collection has at least one element with the given condition search for the largest element
        /// </summary>
        /// <param name="collection">Examined collection</param>
        /// <param name="condition">Conditional that returns true or false each value of T type</param>
        /// <returns><c>Index</c> and  <c>Value</c> of the largest element.<c>-1</c> if there is no element with the given condition</returns>
        public static (int, T) MaxSearch<T>(this IEnumerable<T> collection, Func<T, bool> condition) where T : IComparable<T>
        {
            int maxIndex = -1;
            int index = -1;
            T maxValue = collection.ElementAt(0);

            foreach (var item in collection)
            {
                index++;

                if (condition(item) && item.CompareTo(maxValue) > 0)
                {
                    maxValue = item;
                    maxIndex = index;
                }
            }
            return condition(maxValue) ? (maxIndex, maxValue) : (-1, maxValue);
        }

        /// <summary>
        /// Search for the smallest element of the collection
        /// </summary>
        /// <param name="collection">Examined collection</param>
        /// <returns>The smallest element position <c>index</c> and <c>value</c></returns>
        public static (int, T) MinSearch<T>(this IEnumerable<T> collection) where T : IComparable<T>
        {
            int minIndex = -1;
            int index = -1;
            T minValue = collection.ElementAt(0);

            foreach (var item in collection)
            {
                index++;

                if (item.CompareTo(minValue) < 0)
                {
                    minValue = item;
                    minIndex = index;
                }
            }
            return (minIndex, minValue);
        }

        /// <summary>
        /// If the collection has at least one element with the given condition, search for the smallest element
        /// </summary>
        /// <param name="collection">Examined collection</param>
        /// <param name="condition">Conditional that returns true or false each value of T type</param>
        /// <returns><c>Index</c> and  <c>Value</c> of the largest element. <c>-1</c> if there is no element with the given condition</returns>
        public static (int, T) MinSearch<T>(this IEnumerable<T> collection, Func<T, bool> condition) where T : IComparable<T>
        {
            int minIndex = -1;
            int index = -1;
            T minValue = collection.ElementAt(0);

            foreach (var item in collection)
            {
                index++;

                if (condition(item) && item.CompareTo(minValue) > 0)
                {
                    minValue = item;
                    minIndex = index;
                }
            }
            return condition(minValue) ? (minIndex, minValue) : (-1, minValue);
        }

        public static object BackTrackSearch<T>(this IEnumerable<IEnumerable<T>> collection, out bool hasResult, bool needAllResult=false) where T : IComparable<T>
        {
            IEnumerable<T> resultsCollection = CreateInstance<T>();
            hasResult = false;

            if (needAllResult)
            {
                IEnumerable<IEnumerable<T>> allResultsCollection = CreateInstance<IEnumerable<T>>();
                BackTrackSearchHelper(collection, ref resultsCollection, ref allResultsCollection, ref hasResult);
                return allResultsCollection;
            }

            BackTrackSearchHelper(collection, ref resultsCollection, ref hasResult);

            return resultsCollection;
        }


    }
}
