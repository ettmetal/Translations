using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Ettmetal.Translation.Editor {
    [CustomEditor(typeof(LocalizedText))]
    [CanEditMultipleObjects]
    public class LocalizedTextEditor : UnityEditor.UI.TextEditor {
        private static TranslationSettings settings;

        private SerializedProperty localizedKeyProp;

        private bool useFancyEdit = true;

        protected override void OnEnable() {
            base.OnEnable();
            localizedKeyProp = serializedObject.FindProperty("localizationKey");
        }

        public override void OnInspectorGUI() {
            settings = settings ?? Resources.Load<TranslationSettings>(Translation.Strings.SettingsPath);
            serializedObject.Update();
            if(useFancyEdit) { fancyEdit(); }
            else { EditorGUILayout.PropertyField(localizedKeyProp); }
            serializedObject.ApplyModifiedProperties();
            base.OnInspectorGUI();
        }

        private void fancyEdit() {
            string[] keys = getPossibleKeys();
            if(keys?.Length > 0) {
                string currentValue = localizedKeyProp.stringValue;
                int currentIndex = Array.IndexOf(keys, currentValue);
                if(currentIndex > -1 || currentValue == "") {
                    int selectedKey = EditorGUILayout.Popup(currentIndex, keys);
                    localizedKeyProp.stringValue = keys[selectedKey];
                }
                else {
                    EditorGUILayout.LabelField(Strings.SelectedKeyNotFound);
                }
            }
            else {
                EditorGUILayout.LabelField(Strings.NoKeys);
            }
        }

        // Gets possible localisation strings from locale files
        private string[] getPossibleKeys() {
            LocaleData[] locales = Resources.LoadAll<LocaleData>(settings.LocalesPath);
            string[] keys = null;
            if(locales?.Length > 0) {
                SerializedObject firstLocale = new SerializedObject(locales[0]);
                SerializedProperty itemsProp = firstLocale.FindProperty("items");
                if(itemsProp.arraySize > 1) {
                    keys = new string[itemsProp.arraySize];
                    for(int itemIndex = 0; itemIndex < keys.Length; itemIndex++) {
                        keys[itemIndex] = itemsProp.GetArrayElementAtIndex(itemIndex).FindPropertyRelative("key").stringValue;
                    }
                }
            }
            return keys;
        }
    }
}
