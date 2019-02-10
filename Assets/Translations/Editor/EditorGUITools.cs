using System;
using UnityEngine;
using UnityEditor;

namespace Ettmetal.Translation.Editor {
    public static class EditorGUITools {
        
        public static void DoHorizontal(Action horizontalGUI) {
            EditorGUILayout.BeginHorizontal();
            horizontalGUI();
            EditorGUILayout.EndHorizontal();
        }

        public static void DoVertical(Action verticalGUI) {
            EditorGUILayout.BeginVertical();
            verticalGUI();
            EditorGUILayout.EndVertical();
        }

        public static Vector2 DoScroll(Action scrolledGUI, Vector2	scrollPosition) {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            scrolledGUI();
            EditorGUILayout.EndScrollView();
            return scrollPosition;
        }
    }
}
