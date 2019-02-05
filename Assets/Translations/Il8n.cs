using System;
using UnityEngine;

namespace Ettmetal.Translation {
    // Provides translation services.
    // Includes both handling text localisation and preferences.
    public static class Il8n {
        private static TranslationSettings settings;
        private static LocaleData defaultLocale, activeLocale;
        public static event Action OnLocaleChanged;

        static Il8n() {
            settings = Resources.Load<TranslationSettings>(Strings.SettingsPath);
            if(!PlayerPrefs.HasKey(Strings.LocalePref)){
                setLocale(settings.DefaultLocale);
            }
            defaultLocale = loadLocale(settings.DefaultLocale);
            if(PlayerPrefs.GetString(Strings.LocalePref) == settings.DefaultLocale) {
                activeLocale = defaultLocale;
            }
            else {
                activeLocale = loadLocale(PlayerPrefs.GetString(Strings.LocalePref));
            }
        }

        // Core internationalisation interface.
        public static string __(string key) {
            string translation = activeLocale[key].Value;
            return string.IsNullOrEmpty(translation) ? defaultLocale[key].Value : translation;
        }

        public static void ChangeLocale(string newLocale) {
            if(PlayerPrefs.GetString(Strings.LocalePref) != newLocale) {
                setLocale(newLocale);
                loadLocale(newLocale);
                OnLocaleChanged();
            }
        }

        private static void setLocale(string newLocale) {
            PlayerPrefs.SetString(Strings.LocalePref, newLocale);
            PlayerPrefs.Save();
        }

        private static LocaleData loadLocale(string locale) {
            return Resources.Load<LocaleData>(string.Format(settings.LocalesPath, locale));
        }
    }
}
