using System.Collections.Generic;

//--------------------------------------------------------------------------------

namespace Theater.JSON {

    public class JSONArray : JSONValue {

        //--------------------------------------------------------------------------------
        // Properties
        //--------------------------------------------------------------------------------

        public int Count => this.values.Count;

        public JSONValue this[int index] {
            get => this.values[index];
            set => this.values[index] = value;
        }

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        private readonly List<JSONValue> values = new List<JSONValue>();

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public override bool Equals(JSONValue value) {
            return value is JSONArray;
        }

        //--------------------------------------------------------------------------------

        public override string ToString() {

            string str = "" + JSONValue.OPEN_BRACKET;

            for (int i = 0; i < values.Count; i++) {

                str += values[i].ToString();

                if (i < values.Count - 1) {
                    str += JSONValue.COMMA;
                }
            }

            str += JSONValue.CLOSED_BRACKET;

            return str; 
        }

        //--------------------------------------------------------------------------------

        public void Add(JSONValue value) => this.values.Add(value);
        public void Remove(JSONValue value) => this.Remove(value);
        public bool Contains(JSONValue value) => this.Contains(value);

        //--------------------------------------------------------------------------------
    }
}