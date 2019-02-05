using UnityEngine;

namespace Ettmetal.Translation {
    // Serves as representation of available localised resources accessible
    // with a string key.
    public class LocaleData : ScriptableObject {
        [SerializeField]
        LocalisedItem[] items;
        
        public LocalisedItem this[string key] {
            get {
                foreach (LocalisedItem item in items) {
                    if(item.Key == key) return item;
                }
                return null;
            }
        }
    }
}
