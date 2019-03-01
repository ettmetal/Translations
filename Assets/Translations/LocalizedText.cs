using UnityEngine;
using UnityEngine.UI;

namespace Ettmetal.Translation {
    [AddComponentMenu("UI/Localized Text")]
    public class LocalizedText : Text {
        [SerializeField]
        private string localizationKey;

        protected void Awake() {
            base.Awake();
            updateLocalizedText();
        }

        protected void OnEnable() {
            base.OnEnable();
            Il8n.OnLocaleChanged += updateLocalizedText;
        }

        protected void OnDisable() {
            base.OnDisable();
            Il8n.OnLocaleChanged -= updateLocalizedText;
        }

        private void updateLocalizedText() {
            text = Il8n.__(localizationKey);
        }
    }
}
