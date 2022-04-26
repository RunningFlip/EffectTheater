using System.IO;
using System.Text;

//--------------------------------------------------------------------------------

namespace Theater.JSON {

    public static class JSONParser {

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public static JSONObject ToJSONObject(string json) => JSONParser.ToJSONContainer<JSONObject>(json) as JSONObject;
        public static JSONArray ToJSONArray(string json) => JSONParser.ToJSONContainer<JSONArray>(json) as JSONArray;

        //--------------------------------------------------------------------------------

        private static IJSONContainer ToJSONContainer<T>(string json) where T : IJSONContainer {

            if (string.IsNullOrEmpty(json)) {
                throw new System.Exception("JSON string was null or empty! Parsing failed!");
            }

            using (MemoryStream memoryReader = new MemoryStream(Encoding.UTF8.GetBytes(json))) {

                using (StreamReader reader = new StreamReader(memoryReader)) {

                    SimpleCharReader streamReader = new SimpleCharReader(reader);
                    return new JSONReader(streamReader).Parse<T>();
                }
            }
        }

        //--------------------------------------------------------------------------------

        public static string ToJsonString(JSONObject json) {

            if (json == null) {
                throw new System.Exception("JSONObject was null or empty! Parsing failed!");
            }

            return json.ToString();
        }
        //--------------------------------------------------------------------------------
    }
}