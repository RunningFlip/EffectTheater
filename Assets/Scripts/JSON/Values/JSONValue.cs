using System;
using System.Collections.Generic;

//--------------------------------------------------------------------------------

namespace Theater.JSON {

    public abstract class JSONValue {

        //--------------------------------------------------------------------------------
        // Constants
        //--------------------------------------------------------------------------------

        public const char OPEN_BRACE = '{';
        public const char CLOSED_BRACE = '}';
        public const char OPEN_BRACKET = '[';
        public const char CLOSED_BRACKET = ']';

        public const char COMMA = ',';
        public const char COLON = ':';
        public const char QUOTION_MARK = '"';

        public const string NULL = "null";
        public const string FALSE = "false";
        public const string TRUE = "true";

        //--------------------------------------------------------------------------------
        // Abstract methods
        //--------------------------------------------------------------------------------

        public abstract bool Equals(JSONValue value);
        public abstract override string ToString();

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public bool IsNull() => this is JSONNull;
        public bool IsBool() => this is JSONBool;
        public bool IsInt() => this is JSONInt;
        public bool IsDouble() => this is JSONDouble;
        public bool IsFloat() => this is JSONFloat;
        public bool IsNumber() => this.IsInt() || this.IsDouble() || this.IsFloat();
        public bool IsString() => this is JSONString;
        public bool IsArray() => this is JSONArray;
        public bool IsObject() => this is JSONObject;

        //--------------------------------------------------------------------------------

        public JSONNull AsNull() => this as JSONNull;
        public JSONBool AsBool() => this as JSONBool;
        public JSONInt AsInt() => this as JSONInt;
        public JSONDouble AsDouble() => this as JSONDouble;
        public JSONFloat AsFloat() => this as JSONFloat;
        public JSONString AsString() => this as JSONString;
        public JSONArray AsArray() => this as JSONArray;
        public JSONObject AsObject() => this as JSONObject;

        //--------------------------------------------------------------------------------
    }
}