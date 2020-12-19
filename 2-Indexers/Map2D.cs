using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Indexers
{
    /// <inheritdoc cref="IMap2D{TKey1,TKey2,TValue}" />
    public class Map2D<TKey1, TKey2, TValue> : IMap2D<TKey1, TKey2, TValue>
    {
        private Dictionary<Tuple<TKey1, TKey2>, TValue> map = new Dictionary<Tuple<TKey1, TKey2>, TValue>();

        public static IEqualityComparer<Map2D<TKey1, TKey2, TValue>> MapComparer { get; } = new MapEqualityComparer();

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.NumberOfElements" />
        public int NumberOfElements => map.Count;

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.this" />
        public TValue this[TKey1 key1, TKey2 key2]
        {
            get => map[new Tuple<TKey1, TKey2>(key1, key2)];
            set => map[new Tuple<TKey1, TKey2>(key1, key2)] = value;
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetRow(TKey1)" />
        public IList<Tuple<TKey2, TValue>> GetRow(TKey1 key1)
        {
            return map.Keys
                .Where(tup => tup.Item1.Equals(key1))
                .Select(t => new Tuple<TKey2, TValue>(t.Item2, map[t]))
                .ToList();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetColumn(TKey2)" />
        public IList<Tuple<TKey1, TValue>> GetColumn(TKey2 key2)
        {
            return map.Keys
                .Where(tup => tup.Item2.Equals(key2))
                .Select(t => new Tuple<TKey1, TValue>(t.Item1, map[t]))
                .ToList();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.GetElements" />
        public IList<Tuple<TKey1, TKey2, TValue>> GetElements()
        {
            return map.Keys
                .Select(tup => new Tuple<TKey1, TKey2, TValue>(tup.Item1, tup.Item2, map[tup]))
                .ToList();
        }

        /// <inheritdoc cref="IMap2D{TKey1, TKey2, TValue}.Fill(IEnumerable{TKey1}, IEnumerable{TKey2}, Func{TKey1, TKey2, TValue})" />
        public void Fill(IEnumerable<TKey1> keys1, IEnumerable<TKey2> keys2, Func<TKey1, TKey2, TValue> generator)
        {
            keys1.ToList()
                .ForEach(k1 => keys2.ToList()
                    .ForEach( k2 => this.map[Tuple.Create<TKey1, TKey2>(k1,k2)] = generator(k1, k2)));
        }

        public bool Equals(IMap2D<TKey1, TKey2, TValue> other)
        {
            return MapComparer.Equals(other);
        }

        private sealed class MapEqualityComparer : IEqualityComparer<Map2D<TKey1, TKey2, TValue>>
        {
            public bool Equals(Map2D<TKey1, TKey2, TValue> x, Map2D<TKey1, TKey2, TValue> y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return Equals(x.map, y.map);
            }

            public int GetHashCode(Map2D<TKey1, TKey2, TValue> obj)
            {
                return (obj.map != null ? obj.map.GetHashCode() : 0);
            }
        }

        public string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var keyValuePair in map)
            {
                stringBuilder.Append(String.Format("<K1 : {0}, K2 : {1}, Val : {2}", keyValuePair.Key.Item1, keyValuePair.Key.Item2, keyValuePair.Value));
            }
            return stringBuilder.ToString();
        }
    }
}
