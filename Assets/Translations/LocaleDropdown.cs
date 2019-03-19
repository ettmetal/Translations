using System.Collections.Generic;
using UnityEngine;

namespace Ettmetal.Translation {
    public class LocaleDropdown : MonoBehaviour {

		private Dictionary<int, string> localesByIndex;
        // Start is called before the first frame update
        void Start() {
			
        }

		private void onDropdownChanged(int value) {
			Il8n.ChangeLocale(localesByIndex[value]);
		}
    }
}
