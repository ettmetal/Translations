using System;
using UnityEngine;

namespace Ettmetal.Translation {
    [Serializable]
    // A key-value pair representing a localised string
    public class LocalisedItem {
        [SerializeField]
        private string key;
        public string Key{ get{return key;} }
        [SerializeField]
        private string value;
        public string Value{ get{return value;} }
    }
}
