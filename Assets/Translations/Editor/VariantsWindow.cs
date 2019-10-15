using System.Text.RegularExpressions;
using UnityEngine;
using UnityEditor;

namespace Ettmetal.Translation.Editor {
	public class VariantsWindow : EditorWindow {
		private SerializedProperty plurals;
		private static Regex regexr;
		private static VariantsWindow window;
		public static void ShowFor(SerializedProperty plurals) {
			window = window ?? CreateInstance<VariantsWindow>(); // Use existing window if available
			window.titleContent = new GUIContent("Plural editor");
			window.plurals = plurals;
			regexr = regexr ?? new Regex(Ettmetal.Translation.Strings.TokenPattern);
			window.ShowUtility();
		}

		private void OnGUI(){ // Will eventually be replaced with reorderable
			int elements = EditorGUILayout.IntField("Elements", plurals.arraySize);
			SerializedPropertyUtilities.EnsureSerializedArrayIsSize(plurals, elements);
			for(int i = 0; i < elements; i++) {
				SerializedProperty item = plurals.GetArrayElementAtIndex(i);
				item.isExpanded = EditorGUILayout.Foldout(item.isExpanded, string.Format("Element {0}", i));
				if(item.isExpanded) {
					EditorGUILayout.PropertyField(item.FindPropertyRelative("start"), new GUIContent("Start"));
					// End uses blank = infinity
					EditorGUILayout.PropertyField(item.FindPropertyRelative("end"), new GUIContent("End"));
					SerializedProperty val = item.FindPropertyRelative("value");
					SerializedProperty tok = item.FindPropertyRelative("hasTokens");
					string newValue = EditorGUILayout.TextField(val.stringValue, val.stringValue);
					val.stringValue = newValue;
					tok.boolValue = regexr.Match(newValue).Success;
				}
			}
			plurals.serializedObject.ApplyModifiedProperties();
		}
		
		private void OnDestroy() {
			window = null; // Required because this would otherwise leave a reference to a nonexistent native object
		}
	}
}
