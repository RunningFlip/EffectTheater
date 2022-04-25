using System.IO;
using System.Text;

//--------------------------------------------------------------------------------

namespace Theater.JSON {

    public static class JSONParser {

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public static JSONObject ToJsonObject(string json) {

            if (string.IsNullOrEmpty(json)) {
                throw new System.Exception("JSON string was null or empty! Parsing failed!");
            }

            using (MemoryStream memoryReader = new MemoryStream(Encoding.UTF8.GetBytes(json))) {

                using (StreamReader reader = new StreamReader(memoryReader)) {

                    SimpleCharReader streamReader = new SimpleCharReader(reader);
                    return new JSONReader(streamReader).Parse();
                }
            }
        }

        //--------------------------------------------------------------------------------

        public static string ToJsonString(JSONObject json) {

            if (json == null) {
                throw new System.Exception("JSON Object was null or empty! Parsing failed!");
            }

            return json.ToString();
        }
        //--------------------------------------------------------------------------------
    }
}