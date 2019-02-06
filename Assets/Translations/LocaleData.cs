using UnityEngine;

namespace Ettmetal.Translation {
    // Serves as representation of available localised resources accessible
    // with a string key.
    public class LocaleData : ScriptableObject {
        [SerializeField]
        private string localeName;
        public string Name { get{return localeName;} }
        [SerializeField]
        private string localeCode;
        [SerializeField]
        private LocalisedItem[] items;
        
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
