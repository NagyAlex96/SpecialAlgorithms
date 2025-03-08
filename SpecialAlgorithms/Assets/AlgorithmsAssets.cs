namespace SpecialAlgorithms.Assets
{
    public partial class Algorithms
    {
        private static IEnumerable<T> CreateEnumerableInstance<T>() => Enumerable.Empty<T>();

        private static void BackTrackSearchHelper<T>(IEnumerable<IEnumerable<T>> inputCollection, ref IEnumerable<T> outPutValues, ref bool hasResult, int level = 0) where T : IComparable<T>
        {
            int i = -1;

            while (!hasResult && i < inputCollection.ElementAt(level).Count() - 1)
            {
                i++;
                if (i > inputCollection.ElementAt(level).Count() - 1)
                    return;

                T item = inputCollection.ElementAt(level).ElementAt(i);
                if (FkBTS(level, item, outPutValues))
                {
                    outPutValues = InsertElementAt(outPutValues, item, level);

                    if (level == inputCollection.Count() - 1)
                    {
                        hasResult = true;
                    }
                    else
                    {
                        BackTrackSearchHelper<T>(inputCollection, ref outPutValues, ref hasResult, level + 1);
                    }
                }
            }
        }

        private static void BackTrackSearchHelper<T>(IEnumerable<IEnumerable<T>> inputCollection, ref IEnumerable<T> outPutValues, ref IEnumerable<IEnumerable<T>> allResults, ref bool hasResult, int level = 0) where T : IComparable<T>
        {
            int i = -1;

            while (i < inputCollection.ElementAt(level).Count() - 1)
            {
                i++;
                if (i > inputCollection.ElementAt(level).Count() - 1)
                    return;

                T item = inputCollection.ElementAt(level).ElementAt(i);
                if (FkBTS(level, item, outPutValues))
                {
                    outPutValues = InsertElementAt(outPutValues, item, level);

                    if (level == inputCollection.Count() - 1)
                    {
                        hasResult = true;
                        allResults = allResults.Append(outPutValues);
                    }
                    else
                    {
                        BackTrackSearchHelper<T>(inputCollection, ref outPutValues, ref allResults, ref hasResult, level + 1);
                    }
                }
            }
        }

        private static bool FkBTS<T>(int level, T item, IEnumerable<T> partResult) where T : IComparable<T>
        {
            bool ok = true;

            for (int i = 0; i < level; i++)
            {
                if (item.CompareTo(partResult.ElementAt(i)) == 0)
                {
                    ok = false;
                }
            }
            return ok;
        }

        private static IEnumerable<T> InsertElementAt<T>(IEnumerable<T> inputCollection, T element, int index = -1)
        {
            if (index <= 0 && inputCollection.Count() == 0 || index == inputCollection.Count())
            {
                return inputCollection.Append(element);
            }

            int counter = 0;
            IEnumerable<T> returnedCollection = CreateEnumerableInstance<T>();

            foreach (var item in inputCollection)
            {
                if (counter == index)
                {
                    returnedCollection = returnedCollection.Append(element);
                }
                else
                {
                    returnedCollection = returnedCollection.Append(item);
                }
                counter++;
            }
            return returnedCollection;
        }
    }
}
