using UnityEngine;

namespace Ettmetal.Translation {
    // Configuration for Translations
    public class TranslationSettings : ScriptableObject {
        [SerializeField]
        private string defaultLocale = "";
        public string DefaultLocale { get { return defaultLocale; } }

        [SerializeField]
        [Tooltip("Relative to Assets/Resources.")]
        private string localesPath = Strings.DefaultLocalePath;
        public string LocalesPath { get { return localesPath; } }
        public string LocalesResourcePath {
            get { return localesPath.Replace("Assets/Resources/", ""); }
        }
    }
}
