using System.Collections.Generic;
using System.Linq;

//--------------------------------------------------------------------------------

namespace Theater.JSON {

    public class JSONObject : JSONValue{

        //--------------------------------------------------------------------------------
        // Properties
        //--------------------------------------------------------------------------------

        public int Count => this.mapping.Count;
        public List<string> Keys => this.mapping.Keys.ToList();
        public List<JSONValue> Values => this.mapping.Values.ToList();

        public JSONValue this[string key] {
            get => this.mapping[key];
            set => this.mapping[key] = value;
        }

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        private readonly Dictionary<string, JSONValue> mapping = new Dictionary<string, JSONValue>();

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        public JSONObject() { }

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public override bool Equals(JSONValue value) {

            if (value is JSONObject other) {
                return this.mapping == other.mapping;
            }

            return false;
        }

        //--------------------------------------------------------------------------------

        public override string ToString() {

            string str = "" + JSONValue.OPEN_BRACE;
            List<string> keys = this.Keys;

            for (int i = 0; i < keys.Count; i++) {

                string key = keys[i];
                str += $"\"{key}\"{JSONValue.COLON}{this[key]}";

                if (i < keys.Count - 1) {
                    str += JSONValue.COMMA;
                }
            }

            str += JSONValue.CLOSED_BRACE;

            return str;
        }

        //--------------------------------------------------------------------------------

        public JSONValue GetValue(string key) {
            
            if (this.mapping.TryGetValue(key, out JSONValue value)) {
                return value;
            }

            return null;
        }

        //--------------------------------------------------------------------------------

        public void AddValue(string key, JSONValue value) {
            
            if (!this.mapping.ContainsKey(key)) {
                this.mapping.Add(key, value);
            }
        }

        //--------------------------------------------------------------------------------

        public void RemoveValue(string key) {

            if (this.mapping.ContainsKey(key)) {
                this.mapping.Remove(key);
            }
        }

        //--------------------------------------------------------------------------------

        public bool ContainsKey(string key) => this.mapping.ContainsKey(key);
        public bool TryGetValue(string key, out JSONValue value) => this.mapping.TryGetValue(key, out value);

        //--------------------------------------------------------------------------------
    }
}