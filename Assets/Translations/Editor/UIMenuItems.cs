using UnityEngine;
using UnityEditor;

namespace Ettmetal.Translation.Editor {
	[InitializeOnLoad]
	internal static class UIMenuItems {
		private static GameObject localizedTextPrefab;
		private static GameObject localeDropdownPrefab;
		static UIMenuItems() {
			localizedTextPrefab = Resources.Load<GameObject>(Editor.Strings.TextPrefabPath);
			localeDropdownPrefab = Resources.Load<GameObject>(Editor.Strings.DropdownPrefabPath);
		}

		[MenuItem("GameObject/UI/Localization/Localized Text")]
		public static void AddLocalizedText() {
			instantiateUIObject(localizedTextPrefab);
		}

		[MenuItem("GameObject/UI/Localization/Locale Dropdown")]
		public static void AddLocaleDropdown() {
			instantiateUIObject(localeDropdownPrefab);
		}

		private static void instantiateUIObject(GameObject toInstantiate) {
			GameObject instantiated = GameObject.Instantiate(toInstantiate);
			GameObject canvas = getOrCreateCanvas();
			instantiated.transform.SetParent(canvas.transform, false);
			instantiated.name = toInstantiate.name;
			Selection.SetActiveObjectWithContext(instantiated, null);
		}

		private static GameObject getOrCreateCanvas() {
			GameObject canvas = getCanvasInSelection() ?? GameObject.FindObjectOfType<Canvas>()?.gameObject;
			if(canvas == null) {
				EditorApplication.ExecuteMenuItem("GameObject/UI/Canvas");
				canvas = GameObject.FindObjectOfType<Canvas>().gameObject;
			}
			return canvas;
		}

		private static GameObject getCanvasInSelection() {
			GameObject selected = Selection.activeGameObject;
			return selected?.GetComponentInChildren<Canvas>()?.gameObject;
		}
	}
}
