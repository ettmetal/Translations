using System.Collections.Generic;

namespace Ettmetal.Translation {
	internal static class DictionaryExtensions {
		public static void Add<TKey, TValue>(this Dictionary<TKey, TValue> self, IEnumerable<TKey> keys, TValue value) {
			foreach(TKey key in keys) { self.Add(key, value); }
		}
	}
}
