using UnityEditor;
using Ettmetal.Translation;

namespace Etmetal.Translation.Editor {
    [CustomEditor(typeof(LocaleData))]
    // A dummy editor to prevent serialised fields appearing in the inspector
    public class LocaleDataEditor : UnityEditor.Editor {
        public override void OnInspectorGUI() {
            int arraySize = serializedObject.FindProperty("items").arraySize;
            EditorGUILayout.LabelField(serializedObject.FindProperty("localeName").stringValue);
			string details = string.Format("Contains {0} localisation strings", arraySize);
			EditorGUILayout.LabelField(details);
        }
    }
}
