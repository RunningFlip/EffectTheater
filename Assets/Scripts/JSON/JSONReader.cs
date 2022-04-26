using System;

//--------------------------------------------------------------------------------

namespace Theater.JSON {

    public class JSONReader {

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        private ICharReader charReader;

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        public JSONReader(ICharReader charReader) {
            this.charReader = charReader;
        }

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public IJSONContainer Parse<T>() where T : IJSONContainer {

            if (this.charReader == null) {
                throw new ArgumentNullException("charReader");
            }

            Type type = typeof(T);

            try {

                if (type == typeof(JSONObject)) {
                    return this.ReadObject();
                }
                else if (type == typeof(JSONArray)) {
                    return this.ReadArray();
                }
            }
            catch (Exception e) {
                throw new Exception($"Could not parse the to '{type}'! JSON is not valid!", e);
            }

            return null;
        }

        //--------------------------------------------------------------------------------

        private JSONObject ReadObject() {

            JSONObject jsonObject = new JSONObject();

            this.charReader.SkipWhitespaces();
            this.PromiseChar(this.charReader.Peek(), JSONValue.OPEN_BRACE);
            this.charReader.Read();

            while (this.charReader.Peek() != JSONValue.CLOSED_BRACE) {

                string key = this.ReadKey();

                this.charReader.SkipWhitespaces();
                this.PromiseChar(this.charReader.Peek(), JSONValue.COLON);
                this.charReader.Read();
                this.charReader.SkipWhitespaces();

                JSONValue value = this.ReadValue();
                jsonObject.AddValue(key, value);

                this.charReader.SkipWhitespaces();

                if (this.charReader.Peek().Equals(JSONValue.COMMA)) {
                    this.charReader.Read();
                }
            }

            this.charReader.SkipWhitespaces();
            this.PromiseChar(this.charReader.Peek(), JSONValue.CLOSED_BRACE);
            this.charReader.Read();

            return jsonObject;
        }

        //--------------------------------------------------------------------------------

        private JSONArray ReadArray() {

            JSONArray jsonArray = new JSONArray();

            this.charReader.SkipWhitespaces();
            this.PromiseChar(this.charReader.Peek(), JSONValue.OPEN_BRACKET);
            this.charReader.Read();

            while (this.charReader.Peek() != JSONValue.CLOSED_BRACKET) {

                this.charReader.SkipWhitespaces();
                jsonArray.Add(this.ReadValue());
                this.charReader.SkipWhitespaces();

                if (this.charReader.Peek().Equals(JSONValue.COMMA)) {
                    this.charReader.Read();
                }
            }

            this.charReader.SkipWhitespaces();
            this.PromiseChar(this.charReader.Peek(), JSONValue.CLOSED_BRACKET);
            this.charReader.Read();

            return jsonArray;
        }

        //--------------------------------------------------------------------------------

        private string ReadKey() {

            this.charReader.SkipWhitespaces();
            return this.ReadString();
        }

        //--------------------------------------------------------------------------------

        private JSONValue ReadValue() {

            JSONValue value;

            this.charReader.SkipWhitespaces();

            switch (this.charReader.Peek()) {

                case JSONValue.OPEN_BRACE:
                    value = this.ReadObject();
                    break;

                case JSONValue.OPEN_BRACKET:
                    value = this.ReadArray();
                    break;

                case JSONValue.QUOTION_MARK:
                    value = new JSONString(this.ReadString());
                    break;

                case 't':
                    value = this.ReadBoolean(true);
                    break;

                case 'f':
                    value = this.ReadBoolean(false);
                    break;

                case 'n':
                    value = this.ReadNull();
                    break;

                default:

                    string numberString = this.ReadNumberString();

                    if (this.NumberStringAsInt(numberString, out int intNumber)) {
                        value = new JSONInt(intNumber);
                    }
                    else if (this.NumberStringAsLong(numberString, out long longNumber)) {
                        value = new JSONFloat(longNumber);
                    }
                    else if (this.NumberStringAsFloat(numberString, out float floatNumber)) {
                        value = new JSONFloat(floatNumber);
                    }
                    else if (this.NumberStringAsDouble(numberString, out double doubleNumber)) {
                        value = new JSONDouble(doubleNumber);
                    } 
                    else {
                        throw new Exception("Could not parse current chunk into valid JSONValue! Parsing failed!");
                    }

                    break;
            }

            return value;
        }

        //--------------------------------------------------------------------------------

        private JSONNull ReadNull() {

            this.charReader.SkipWhitespaces();
            this.PromiseChar(this.charReader.Peek(), 'n');

            this.ReadMultipleCharacters(4);

            return new JSONNull();
        }

        //--------------------------------------------------------------------------------


        private JSONBool ReadBoolean(bool boolean) {

            this.charReader.SkipWhitespaces();
            this.PromiseChar(this.charReader.Peek(), boolean ? 't' : 'f');

            this.ReadMultipleCharacters(boolean ? 4 : 5);

            return new JSONBool(boolean);
        }


        //--------------------------------------------------------------------------------

        private void ReadMultipleCharacters(int times) {

             for(int i = 0; i < times; i++) {
                this.charReader.Read();
            }
        }

        //--------------------------------------------------------------------------------

        private string ReadString() {

            string str = "";

            this.charReader.SkipWhitespaces();
            this.PromiseChar(this.charReader.Peek(), JSONValue.QUOTION_MARK);
            this.charReader.Read();

            while (this.charReader.Peek() != JSONValue.QUOTION_MARK) {
                str += this.charReader.Read();
            }

            this.PromiseChar(this.charReader.Peek(), JSONValue.QUOTION_MARK);
            this.charReader.Read();

            return str;
        }

        //--------------------------------------------------------------------------------

        private string ReadNumberString() {

            this.charReader.SkipWhitespaces();

            string str = "";
            char current = this.charReader.Peek();

            if (current == '-' || char.IsDigit(current)) {

                do {

                    str += this.charReader.Read();
                    current = this.charReader.Peek();
                }
                while (current == '-' || char.IsDigit(current));
            }

            if (str.Length > 0) {

                char lastChar = str[str.Length - 1];

                if (lastChar != '.' && lastChar != 'f' && !char.IsDigit(lastChar)) {
                    throw new Exception($"Last character of read number was not a digit but '{lastChar}'! Parsing failed!");
                }
            }
            else {
                throw new Exception("Number was tried to parse, but no characters were read!");
            }
            
            return str;
        }

        //--------------------------------------------------------------------------------

        private bool NumberStringAsInt(string value, out int num) => int.TryParse(value, out num);
        private bool NumberStringAsLong(string value, out long num) => long.TryParse(value, out num);
        private bool NumberStringAsDouble(string value, out double num) => double.TryParse(value, out num);
        private bool NumberStringAsFloat(string value, out float num) => float.TryParse(value, out num);

        //--------------------------------------------------------------------------------

        private bool PromiseChar(char charToCheck, char symbol) {

            if (charToCheck.Equals(symbol)) {
                return true;
            }

            throw new Exception($"'{symbol}' char was expected but '{charToCheck}' was found! JSON is not valid!");
        }

        //--------------------------------------------------------------------------------
    }
}
