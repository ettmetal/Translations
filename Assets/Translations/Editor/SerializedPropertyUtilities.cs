using UnityEditor;
using UnityEngine;

namespace Ettmetal.Translation.Editor {
	public static class SerializedPropertyUtilities {
		public static void EnsureSerializedArrayIsSize(SerializedProperty array, int size) {
			int currentSize = array.arraySize;
			int sizeDifference = size - currentSize;
			if(sizeDifference == 0) return;
			else if(sizeDifference < 0) {
				for(int i = 0; i < Mathf.Abs(sizeDifference); i++) {
					array.arraySize--;
				}
			}
			else {
				for(int i = 0; i < sizeDifference; i++) {
					array.arraySize++;
				}
			}
		}
	}
}
