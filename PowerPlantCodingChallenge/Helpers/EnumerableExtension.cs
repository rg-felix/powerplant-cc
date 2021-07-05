using System.Collections.Generic;
using System.Linq;

namespace PowerPlantCodingChallenge.Helpers
{
    public static class EnumerableExtension
    {
        public static IEnumerable<IEnumerable<T>> DifferentCombinations<T>(this IEnumerable<T> elements)
        {
            return Enumerable.Range(0, elements.Count())
                .SelectMany(i => elements.DifferentCombinations(i));

        }
        public static IEnumerable<IEnumerable<T>> DifferentCombinations<T>(this IEnumerable<T> elements, int k)
        {
            return k == 0 ? new[] { new T[0] } :
              elements.SelectMany((e, i) =>
                elements.Skip(i + 1).DifferentCombinations(k - 1).Select(c => (new[] { e }).Concat(c)));
        }

        public static void Add<TKey, TValue>(this IDictionary<TKey, HashSet<TValue>> collection, TKey key, TValue value)
        {
            if (collection.ContainsKey(key))
            {
                if (collection[key] == null)
                    collection[key] = new HashSet<TValue>();

                collection[key].Add(value);
            }
            else
            {
                collection[key] = new HashSet<TValue>();

                collection[key].Add(value);
            }
        }

    }
}
