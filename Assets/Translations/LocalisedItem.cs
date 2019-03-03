using System;
using UnityEngine;

namespace Ettmetal.Translation {
    [Serializable]
    // Represents a localised string
    public class LocalisedItem {
        [SerializeField]
        private string key = "";
        public string Key{ get{return key;} }
        [SerializeField]
        private string value = "";
        public string Value{ get{return value;} }
        [SerializeField]
        private PluralForm[] plurals = null;

        public string Pluralised(int count) {
            if(plurals.Length < 1) {
                if(count == 0){
                    return value;
                }
                else{
                    throw new InvalidOperationException(Strings.NoPluralException);
                }
            }
            foreach(PluralForm plural in plurals) {
                if(plural.ApplicableTo(count)) return plural.Value;
            }
            return null;
        }

        [Serializable]
        private class PluralForm {
            [SerializeField]
            private int start = 0;
            [SerializeField]
            private int end = int.MaxValue;
            [SerializeField]
            private string value = "";
            public string Value{ get{return value;} }

            public bool ApplicableTo(int count) {
                return start <= count && count <= end;
            }
        }
    }
}
