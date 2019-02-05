using UnityEngine;

namespace Ettmetal.Translation {
    // Configuration for Translations
    public class TranslationSettings : ScriptableObject {
        [SerializeField]
        private string defaultLocale;
        public string DefaultLocale {get {return defaultLocale;} }

        [SerializeField]
        private string localesPath;
        public string LocalesPath {get {return localesPath;} }
    }  
}
