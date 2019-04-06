using UnityEngine.UI;
using UnityEditor;
using UnityEditor.UI;

namespace Ettmetal.Translation.Editor {
	[CustomEditor(typeof(LocaleDropdown), true)]
	[CanEditMultipleObjects]
	public class LocaleDropdownEditor : SelectableEditor {
		private SerializedProperty template;
		private SerializedProperty captionText;
		private SerializedProperty captionImage;
		private SerializedProperty itemText;
		private SerializedProperty itemImage;
		private SerializedProperty value;

		protected override void OnEnable() {
			base.OnEnable();
			template = serializedObject.FindProperty("m_Template");
			captionText = serializedObject.FindProperty("m_CaptionText");
			captionImage = serializedObject.FindProperty("m_CaptionImage");
			itemText = serializedObject.FindProperty("m_ItemText");
			itemImage = serializedObject.FindProperty("m_ItemImage");
			value = serializedObject.FindProperty("m_Value");
		}

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();
			EditorGUILayout.Space();
			serializedObject.Update();
			EditorGUILayout.PropertyField(template);
			EditorGUILayout.PropertyField(captionText);
			EditorGUILayout.PropertyField(captionImage);
			EditorGUILayout.PropertyField(itemText);
			EditorGUILayout.PropertyField(itemImage);
			EditorGUILayout.PropertyField(value);
			serializedObject.ApplyModifiedProperties();
		}
	}
}
