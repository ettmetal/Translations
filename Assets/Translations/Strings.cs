namespace Ettmetal.Translation {
    // Used because Unity relies on strings for some resource location.
    internal static class Strings {
        public static readonly string LocalePref = "Ettmetal.Translation.Locale";
        public static readonly string SettingsPath = "Translations-Settings";
        public static readonly string FallbackToDefaultFormat = "No value found in locale {0} for {1}, falling back to default locale.";
        public static readonly string NoDefaultValueFormat = "The default locale {0} contains no default value for {1}.";
        public static readonly string NoPluralFormat = "";
        public static readonly string NoPluralException = "The queried item does not contain pluralisations.";
    }  
}
