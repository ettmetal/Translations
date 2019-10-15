using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Ettmetal.UnityHelpers;

namespace Ettmetal.Translation.Editor {
	public class Il8nWindow : EditorWindow {
		private static Il8nWindow window;
		private static TranslationSettings settings;
		[SerializeField]
		private SerializedObject serializedLocales;
		private Vector2 scrollPosition;
		private string newLocaleName;

		[MenuItem("Window/Translations")]
		private static void ShowWindow() {
			window = GetWindow<Il8nWindow>();
			window.titleContent = new GUIContent("Translations");
			window.Show();
		}

		private void OnEnable() {
			init();
		}

		private void init() { // Get settings and current locale data
			settings = settings == null ? Resources.Load<TranslationSettings>(Translation.Strings.SettingsPath) : settings;
			LocaleData[] locales = Resources.LoadAll<LocaleData>(settings.LocalesResourcePath);
			serializedLocales = locales != null && locales.Length > 0 ? new SerializedObject(locales) : null;
		}

		private void OnGUI() {
			EditorGUITools.DoHorizontal(addOrRemoveLocale);
			if(serializedLocales != null && serializedLocales.targetObject != null) {
				serializedLocales.Update();
				if(serializedLocales.FindProperty("items").arraySize > 0) {
					scrollPosition = EditorGUITools.DoScroll(drawGrid, scrollPosition);
				}
				else {
					EditorGUILayout.LabelField(Strings.NoStrings);
				}
				EditorGUITools.DoHorizontal(addOrRemoveItem);
			}
		}

		private void drawGrid() {
			EditorGUITools.DoHorizontal(() => {
				keyColumn();
				foreach(var locale in serializedLocales.targetObjects) {
					localeColumn(new SerializedObject(locale));
				}
			});
		}

		private void keyColumn() {
			int strings = serializedLocales.FindProperty("items").arraySize;
			EditorGUITools.DoVertical(() => {
				EditorGUILayout.LabelField(new GUIContent("String name", "The name used to identify this translation"));
				for(int keyIndex = 0; keyIndex < strings; keyIndex++) {
					var keyProp = serializedLocales.FindProperty("items").GetArrayElementAtIndex(keyIndex).FindPropertyRelative("key");
					EditorGUILayout.PropertyField(keyProp);
				}
			});
			serializedLocales.ApplyModifiedProperties();
		}

		private void localeColumn(SerializedObject locale) {
			int strings = serializedLocales.FindProperty("items").arraySize;
			EditorGUITools.DoVertical(() => {
				string label = locale.targetObject.name;
				if(settings.DefaultLocale == locale.targetObject.name) {
					label += " (Default)";
				}
				EditorGUILayout.LabelField(label);
				for(int keyIndex = 0; keyIndex < strings; keyIndex++) {
					var localisationProp = locale.FindProperty("items").GetArrayElementAtIndex(keyIndex);//.FindPropertyRelative("value");
					editItem(localisationProp);
				}
			});
			locale.ApplyModifiedProperties();
		}

		private void addOrRemoveLocale() {
			newLocaleName = EditorGUILayout.TextField(new GUIContent("Name"), newLocaleName);
			if(GUILayout.Button("+")) {
				LocaleData newLocale = AssetCreator.Create<LocaleData>(newLocaleName, Strings.ResourcesRoot + settings.LocalesPath);
				SerializedProperty localizedItems = serializedLocales?.FindProperty("items");
				if(localizedItems?.arraySize > 0) {
					SerializedObject newLocaleSerialized = new SerializedObject(newLocale);
					SerializedProperty newLocaleItems = newLocaleSerialized.FindProperty("items");
					do {
						newLocaleItems.arraySize++;
						int index = newLocaleItems.arraySize - 1;
						SerializedProperty currentKey = newLocaleItems.GetArrayElementAtIndex(index).FindPropertyRelative("key");
						string currentKeyValue = localizedItems.GetArrayElementAtIndex(index).FindPropertyRelative("key").stringValue;
						currentKey.stringValue = currentKeyValue;
					} while(newLocaleItems.arraySize != localizedItems.arraySize);
					newLocaleSerialized.ApplyModifiedProperties();
				}
				newLocaleName = string.Empty;
				// OnProjectChanged gets called as soon as the asset is created, so this has to be here instead.
				init();
			}
		}

		private void addOrRemoveItem() {
			if(GUILayout.Button("+")) {
				serializedLocales.FindProperty("items").arraySize++;
				serializedLocales.ApplyModifiedProperties();
			}
			if(serializedLocales.FindProperty("items").arraySize > 0 && GUILayout.Button("Remove Last Sring")) {
				serializedLocales.FindProperty("items").arraySize--;
				serializedLocales.ApplyModifiedProperties();
			}
		}

		private void editItem(SerializedProperty item) {
			SerializedProperty items = item.FindPropertyRelative("plurals");
			// Ensure default value is set up
			SerializedPropertyUtilities.EnsureSerializedArrayIsSize(items, 1);
			SerializedProperty defaultValue = items.GetArrayElementAtIndex(0);
			defaultValue.FindPropertyRelative("start").intValue = int.MinValue;
			defaultValue.FindPropertyRelative("end").intValue = int.MaxValue;
			// Draw field for default value and button for providing plurals
			EditorGUITools.DoHorizontal(() => {
				EditorGUILayout.PropertyField(defaultValue.FindPropertyRelative("value"));
				if(GUILayout.Button("Plurals", GUILayout.ExpandWidth(false))) {
					VariantsWindow.ShowFor(item.FindPropertyRelative("plurals"));
				}
			});
		}
	}
}
