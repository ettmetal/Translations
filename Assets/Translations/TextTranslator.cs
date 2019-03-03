using UnityEngine;
using UnityEngine.UI;

namespace Ettmetal.Translation {
    // Basic text Component translation
    [RequireComponent(typeof(Text))]
    public class TextTranslator : MonoBehaviour {
        [SerializeField]
        private string key = null;
        private Text text;

        void Start() {
            text = GetComponent<Text>();
            updateTranslation();
        }

        private void OnEnable() {
            Il8n.OnLocaleChanged += updateTranslation;
            updateTranslation();
        }

        private void OnDisable() {
            Il8n.OnLocaleChanged -= updateTranslation;
        }

        private void updateTranslation() {
            if(!string.IsNullOrEmpty(key)){
                text.text = Il8n.__(key);
            }
        }
    }
}
