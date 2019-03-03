using UnityEngine;
using UnityEditor;

namespace Ettmetal.UnityHelpers {
    public static class AssetCreator {
        private static readonly string defaultPath = "Assets/";
        public static T Create<T>(string name, string path = null) where T : ScriptableObject {
            path = string.IsNullOrEmpty(path) ? defaultPath : path;
            if(!AssetDatabase.IsValidFolder(path)) {
                createPath(defaultPath, path);
            }
            return createAssetAtPath<T>(generatePath(path + name));
        }

        private static string generatePath(string path) {
            path = path + ".asset";
            return AssetDatabase.GenerateUniqueAssetPath(path);
        }

        private static T createAssetAtPath<T>(string path) where T : ScriptableObject {
            T asset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
            return asset;
        }

        private static void createPath(string parent, string path) {
            string remainingPath = path.Replace(parent, "");
            string[] splitPath = remainingPath.Split('/');
            AssetDatabase.CreateFolder(parent.TrimEnd('/'), splitPath[0]);
            string newParent = parent + splitPath[0] + "/";
            if(newParent != path) {
                createPath(newParent, path);
            }
        }
    }
}
