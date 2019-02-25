using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Ettmetal.Translation.Editor {
	[CustomEditor(typeof(TextTranslator))]
	public class TextTranslatorEditor : UnityEditor.Editor {

        private static TranslationSettings settings;

		private bool fancyEdit = true;

		public override void OnInspectorGUI() {
            settings = settings == null ? Resources.Load<TranslationSettings>(Translation.Strings.SettingsPath) : settings;
			SerializedProperty translationKeyProp = serializedObject.FindProperty("key");
			fancyEdit = GUILayout.Toggle(fancyEdit, new GUIContent("Select from dropdown", "When enabled, translations can be selected from those available."));
			if(fancyEdit) {
				string[] keys = getPossibleKeys();
				if(keys != null && keys.Length > 0) {
					string currentValue = translationKeyProp.stringValue;
					int currentIndex = Array.IndexOf(keys, currentValue);
					if(currentIndex > -1 || currentValue == "") {
						int selectedKey = EditorGUILayout.Popup(currentIndex, keys);
						string newValue = keys[selectedKey];
						translationKeyProp.stringValue = newValue;
					}
					else {
						EditorGUILayout.LabelField(new GUIContent("This translator references a key which is not present in languae files."));
						EditorGUILayout.LabelField(new GUIContent("Please edit it without using the dropdown."));
					}
				}
				else {
					EditorGUILayout.LabelField(new GUIContent("There are no translation keys available"));
				}
			}
			else {
				EditorGUILayout.PropertyField(translationKeyProp);
			}
			serializedObject.ApplyModifiedProperties();
		}

		private string[] getPossibleKeys() {
			var locales = Resources.LoadAll<LocaleData>(settings.LocalesPath);
			if(locales == null || locales.Length < 1)
				return null;
			var firstLocale = new SerializedObject(locales[0]);
			var itemsProp = firstLocale.FindProperty("items");
			if(itemsProp.arraySize < 1)
				return null;
			List<string> keys = new List<string>();
			for(int itemIndex = 0; itemIndex < itemsProp.arraySize; itemIndex++) {
				keys.Add(itemsProp.GetArrayElementAtIndex(itemIndex).FindPropertyRelative("key").stringValue);
			}
			return keys.ToArray();
		}
	}
}
