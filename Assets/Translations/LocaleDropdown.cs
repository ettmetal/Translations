using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ettmetal.Translation {
	public class LocaleDropdown : Dropdown {

		private Dictionary<int, string> localesByIndex;
		// Start is called before the first frame update
		protected override void Start() {
			base.Start();
			localesByIndex = new Dictionary<int, string>();
			string[] localeNames = Il8n.AvailableLocales;
			for(int localeIndex = 0; localeIndex < localeNames.Length; localeIndex++) {
				localesByIndex.Add(localeIndex, localeNames[localeIndex]);
				options.Add(new OptionData(localeNames[localeIndex]));
			}
			onValueChanged.AddListener(onDropdownChanged);
		}

		protected override void OnEnable() {
			base.OnEnable();
			Il8n.OnLocaleChanged += updateOptions;
		}

		protected override void OnDisable() {
			base.OnDisable();
			Il8n.OnLocaleChanged -= updateOptions;
		}

		private void onDropdownChanged(int value) {
			Il8n.ChangeLocale(localesByIndex[value]);
		}

		private void updateOptions() {
			ClearOptions();
			foreach(string localeName in localesByIndex.Values) {
				options.Add(new OptionData(Il8n.__(localeName)));
			}
		}
	}
}
