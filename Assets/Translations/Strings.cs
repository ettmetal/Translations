namespace Ettmetal.Translation {
	// Used because Unity relies on strings for some resource location.
	public static class Strings {
		public static readonly string LocalePref = "Ettmetal.Translation.Locale";
		public static readonly string SettingsPath = "Translations Settings";
		public static readonly string DefaultLocalePath = "Translations/Locales/";
		public static readonly string FallbackToDefaultFormat = "No value found in locale {0} for {1}, falling back to default locale.";
		public static readonly string NoDefaultValueFormat = "The default locale {0} contains no default value for {1}.";
		public static readonly string NoPluralFormat = "";
		public static readonly string LocaleNotFoundFormat = "The requested locale ({0}) could not be found in the Locales folder.";
		public static readonly string TokenPattern = "(?<token><%(?<token_name>[.+])%>)";
		public static readonly string NoTokenResolverErrorFormat = "The token \"{0}\" could not be resolved as no token resolver was registered.";
		public static readonly string NoValueForTokenFormat = "The token \"{0}\" has no value set.";
	}
}
