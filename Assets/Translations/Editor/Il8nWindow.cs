using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using Ettmetal.UnityHelpers;


namespace Ettmetal.Translation.Editor {    
    public class Il8nWindow : EditorWindow {
        private static Il8nWindow window;
        private static TranslationSettings settings;
        private SerializedObject serializedLocales;
        private Vector2 scrollPosition;
        private int keyCount;
        private ReorderableList list;
        private string newLocaleName;
    
        [MenuItem("Window/Translations")]
        private static void ShowWindow() {
            settings = settings == null ? Resources.Load<TranslationSettings>(Strings.SettingsPath) : settings;
            window = GetWindow<Il8nWindow>();
            window.titleContent = new GUIContent("Il8n");
            window.init();
            window.Show();
        }

        private void init() {
            LocaleData[] locales = Resources.LoadAll<LocaleData>(settings.LocalesPath);
            serializedLocales = locales != null && locales.Length > 0 ? new SerializedObject(locales) : null;
            keyCount = serializedLocales != null ? serializedLocales.FindProperty("items").arraySize : 0;
        }

        private void OnGUI() {
            EditorGUITools.DoHorizontal(addOrRemoveLocale);
            EditorGUITools.DoVertical(() => {
                if(serializedLocales != null && serializedLocales.targetObject != null) {
                    if(keyCount > 0){
                        scrollPosition = EditorGUITools.DoScroll(drawGrid, scrollPosition);
                    }
                    else{
                    EditorGUILayout.LabelField("No strings to edit");
                }
                }
                EditorGUITools.DoHorizontal(addOrRemoveItem);
            });
        }

        private void drawGrid(){
            EditorGUITools.DoHorizontal(() => {
                keyColumn();
                foreach(var locale in serializedLocales.targetObjects) {
                    localeColumn(new SerializedObject(locale));
                }
            });
        }

        private void keyColumn() {
            EditorGUITools.DoVertical(() => {
                EditorGUILayout.LabelField(new GUIContent("String name", "The name used to identify this translation"));
                for(int keyIndex = 0; keyIndex < keyCount; keyIndex++){
                    var keyProp = serializedLocales.FindProperty("items").GetArrayElementAtIndex(keyIndex).FindPropertyRelative("key");
                    EditorGUILayout.PropertyField(keyProp);
                }
            });
        }

        private void localeColumn(SerializedObject locale) {
            EditorGUITools.DoVertical(() => {
                EditorGUILayout.LabelField(locale.targetObject.name);
                for(int keyIndex = 0; keyIndex < keyCount; keyIndex++){
                    var localisationProp = locale.FindProperty("items").GetArrayElementAtIndex(keyIndex).FindPropertyRelative("value");
                    EditorGUILayout.PropertyField(localisationProp);
                }
            });
            locale.ApplyModifiedProperties();
        }

        private void addOrRemoveLocale(){
            newLocaleName = EditorGUILayout.TextField(new GUIContent("Name"), newLocaleName);
            if(GUILayout.Button("+")){
                AssetCreator.Create<LocaleData>(newLocaleName, "Assets/Resources/Locales/");
                newLocaleName = string.Empty;
            }
            // TODO: Remove Button
            // if(GUILayout.Button("-")){
                
            // }
        }

        private void addOrRemoveItem(){
            if(GUILayout.Button("+")){
                serializedLocales.FindProperty("items").arraySize++;
                serializedLocales.ApplyModifiedProperties();
            }
            if(serializedLocales.FindProperty("items").arraySize > 0 && GUILayout.Button("Remove Last Sring")){
                serializedLocales.FindProperty("items").arraySize--;
                serializedLocales.ApplyModifiedProperties();
            }
        }
    }
}