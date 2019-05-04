using System;
using UnityEngine;

namespace Ettmetal.Translation {
	// Provides translation services.
	// Includes both handling text localisation and preferences.
	public static class Il8n {
		private static TranslationSettings settings;
		private static LocaleData defaultLocale, activeLocale;
		private static TokenProcesser tokens;
		public static event Action OnLocaleChanged;
		private static string[] availableLocales;
		public static TokenProcesser Tokens { get { return tokens; } }
		public static string[] AvailableLocales {
			get { return availableLocales; }
		}
		static Il8n() {
			settings = Resources.Load<TranslationSettings>(Strings.SettingsPath);
			LocaleData[] locales = Resources.LoadAll<LocaleData>(settings.LocalesResourcePath);
			availableLocales = new string[locales.Length];
			for(int localeIndex = 0; localeIndex < locales.Length; localeIndex++) {
				availableLocales[localeIndex] = locales[localeIndex].Name;
			}
			if(!PlayerPrefs.HasKey(Strings.LocalePref)) {
				setLocale(settings.DefaultLocale);
			}
			defaultLocale = loadLocale(settings.DefaultLocale);
			if(PlayerPrefs.GetString(Strings.LocalePref) == settings.DefaultLocale) {
				activeLocale = defaultLocale;
			}
			else {
				activeLocale = loadLocale(PlayerPrefs.GetString(Strings.LocalePref));
			}
			Resources.UnloadUnusedAssets();
			tokens = new TokenProcesser();
		}

		// Core internationalisation interface.
		public static string __(string key) {
			return __(key, 0);
		}

		public static string __(string key, int count) {
			string translation = activeLocale[key].Pluralised(count);
			if(string.IsNullOrEmpty(translation)) {
				Debug.LogFormat(Strings.FallbackToDefaultFormat, activeLocale.Name, key);
				translation = defaultLocale[key].Pluralised(count);
			}
			if(string.IsNullOrEmpty(translation)) {
				Debug.LogWarningFormat(Strings.NoDefaultValueFormat, defaultLocale.Name, key);
			}
			return Tokens.ReplaceTokens(translation);
		}

		public static void ChangeLocale(string newLocale) {
			if(PlayerPrefs.GetString(Strings.LocalePref) != newLocale) {
				setLocale(newLocale);
				activeLocale = loadLocale(newLocale);
				OnLocaleChanged();
			}
		}

		private static void setLocale(string newLocale) {
			PlayerPrefs.SetString(Strings.LocalePref, newLocale);
			PlayerPrefs.Save();
		}

		private static LocaleData loadLocale(string locale) {
			LocaleData loadedLocale = Resources.Load<LocaleData>(settings.LocalesResourcePath + locale);
			if(loadedLocale == null) {
				throw new ArgumentOutOfRangeException(string.Format(Strings.LocaleNotFoundFormat, locale));
			}
			return loadedLocale;
		}
	}
}
