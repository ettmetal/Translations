using UnityEngine;

namespace Ettmetal.Translation {
	// Script for hooking up e.g. UnityEvents to static Il8n ChangeLocale()
    public class LocaleSetter : MonoBehaviour {
		public void ChangeLocale(string newLocale) {
			Il8n.ChangeLocale(newLocale);
		}
    }
}
