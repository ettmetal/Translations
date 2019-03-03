using UnityEngine;
using UnityEngine.UI;

namespace Ettmetal.Translation {
    [AddComponentMenu("UI/Localized Text")]
    public class LocalizedText : Text {
        [SerializeField]
        private string localizationKey;

        protected override void Awake() {
            base.Awake();
            updateLocalizedText();
        }

        protected override void OnEnable() {
            base.OnEnable();
            Il8n.OnLocaleChanged += updateLocalizedText;
        }

        protected override void OnDisable() {
            base.OnDisable();
            Il8n.OnLocaleChanged -= updateLocalizedText;
        }

        private void updateLocalizedText() {
            text = Il8n.__(localizationKey);
        }
    }
}
