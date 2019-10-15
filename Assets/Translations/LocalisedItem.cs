using System;
using UnityEngine;

namespace Ettmetal.Translation {
	[Serializable]
	// Represents a localised string
	public class LocalisedItem {
		[SerializeField, Tooltip("The name used to refer to this item. Must be unique.")]
		private string key = "";
		public string Key { get { return key; } }
		[SerializeField]
		private string defaultValue;
		public string Default { get { return plurals[0].Value; } }
		[SerializeField]
		private Variant[] plurals;
		public bool HasTokens {
			get {
				foreach(Variant plural in plurals) {
					if(plural.IsTokenized) return true;
				}
				return false;
			}
		}

		public string Pluralised(int count) {
			foreach(Variant plural in plurals) {
				if(plural.ApplicableTo(count)) {
					return plural.Value;
				}
			}
			throw new InvalidOperationException();
			//return null;
		}

		[Serializable]
		private class Variant {
			[SerializeField, Tooltip("The lowest count this variant is applicable to. Inclusive.")]
			private int start = 0;
			[SerializeField, Tooltip("The highest count this variant is applicable to. Inclusive.")]
			private int end = int.MaxValue;
			[SerializeField, Tooltip("The text to use as this variant. Must be complete, including tokens.")]
			private string value = "";
			public string Value {
				get { return value; }
			}
			public bool IsTokenized {
				get { return hasTokens; }
			}

			[SerializeField]
			private bool hasTokens;

			public bool ApplicableTo(int count) {
				return start <= count && count <= end;
			}
		}
	}
}
