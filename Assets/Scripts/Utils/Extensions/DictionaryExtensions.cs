using System;
using System.Collections.Generic;
using JetBrains.Annotations;

namespace Utils.Extensions
{
    public static class DictionaryExtensions
    {
        public static TValue GetOrElse<TKey, TValue>([NotNull] this Dictionary<TKey, TValue> dict, [NotNull] TKey key, TValue def) {
            TValue result;
            return dict.TryGetValue(key, out result) ? result : def;
        }

        public static string GetOrEmpty<T>([NotNull] this Dictionary<T, string> dict, [NotNull] T key) {
            return dict.GetOrElse(key, string.Empty);
        }

        [CanBeNull]
        public static TValue GetOrDefault<TKey, TValue>([NotNull] this Dictionary<TKey, TValue> dict, [NotNull] TKey key) {
            return dict.GetOrElse(key, default(TValue));
        }

        public static TValue GetOrCreate<TKey, TValue>([NotNull] this Dictionary<TKey, TValue> dict, TKey key, [NotNull] Func<TValue> factory) {
            if ( factory == null ) {
                throw new ArgumentNullException("factory");
            }
            TValue result;
            if ( dict.TryGetValue(key, out result) ) {
                return result;
            }
            result = factory();
            dict.Add(key, result);
            return result;
        }

        public static void Increment<TKey>([NotNull] this Dictionary<TKey, int> dict, [NotNull] TKey key, int count = 1) {
            var oldValue = dict.GetOrDefault(key);
            dict[key] = oldValue + count;
        }

        public static void AddOrUpdate<TKey, TValue>([NotNull] this Dictionary<TKey, TValue> dict, TKey key, Func<TValue> create, Action<TValue> update) {
            if ( dict.TryGetValue(key, out var value) ) {
                update(value);
            } else {
                dict.Add(key, create());
            }
        }
    }
}
