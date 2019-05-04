using System;
using UnityEngine;

namespace Ettmetal.Translation {
	[Serializable]
	// Represents a localised string
	public class LocalisedItem {
		[SerializeField]
		private string key = "";
		public string Key { get { return key; } }
		[SerializeField]
		private PluralForm[] plurals = null;
		public bool HasTokens {
			get {
				foreach(PluralForm plural in plurals) {
					if(plural.IsTokenized) return true;
				}
				return false;
			}
		}

		public string Pluralised(int count) {
			foreach(PluralForm plural in plurals) {
				if(plural.ApplicableTo(count)) {
					return plural.Value;
				}
			}
			throw new InvalidOperationException();
			//return null;
		}

		[Serializable]
		private class PluralForm {
			[SerializeField]
			private int start = 0;
			[SerializeField]
			private int end = int.MaxValue;
			[SerializeField]
			private string value = "";
			public string Value {
				get { return value; }
			}
			public bool IsTokenized = false;

			public bool ApplicableTo(int count) {
				return start <= count && count <= end;
			}
		}
	}
}
