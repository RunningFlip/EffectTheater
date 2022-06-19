using System;
using UnityEngine;
using UnityEngine.UI;
using Theater.JSON;
using Assets.Scripts.Request;
using TMPro;

//--------------------------------------------------------------------------------

namespace Theater.Hue {

    public class Hue : MonoBehaviour {

        //--------------------------------------------------------------------------------
        // Inner enum
        //--------------------------------------------------------------------------------

        private enum RequestErrorCode {

            PRESS_BRIDGE_BUTTON = 101,
        }

        //--------------------------------------------------------------------------------
        // DEBUGGING
        //--------------------------------------------------------------------------------

        private const string APP_NAME = "effectTheater";
        private const string USER_NAME = "home_philipp";

        private const string HUE_DISCOVERY_URL = "https://discovery.meethue.com/";

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        [SerializeField] private HueBridgeData hueBridgeData;

        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI idTest;
        [SerializeField] private TextMeshProUGUI ipTest;
        [SerializeField] private TextMeshProUGUI portTest;
        [SerializeField] private TextMeshProUGUI userTest;

        [Header("Buttons")]
        [SerializeField] private Button findHueBridgeButton;
        [SerializeField] private Button createUserButton;

        [Header("Hue Panel")]
        [SerializeField] private Button huePanelOpenButton;
        [SerializeField] private Button huePanelCloseButton;
        [Space]
        [SerializeField] private CanvasGroup canvasGroup;

        //--------------------------------------------------------------------------------
        // URL properties
        //--------------------------------------------------------------------------------

        private string ApiURL => $"https://{this.hueBridgeData.ip}/api";
        private string DataURL => $"{this.ApiURL}/{this.hueBridgeData.userName}";
        private string LightsURL => $"{this.DataURL}/lights";

        private string GetLightURL(int lightIndex) => $"{this.LightsURL}/{lightIndex}";
        private string GetLightStateURL(int lightIndex) => $"{this.GetLightURL(lightIndex)}/state";

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        private void Awake() {

            this.huePanelOpenButton.onClick.AddListener(() => this.canvasGroup.Show(true));
            this.huePanelCloseButton.onClick.AddListener(() => this.canvasGroup.Show(false));

            this.UpdateTexts();

            this.findHueBridgeButton.onClick.AddListener(this.GetHueBridgeData);
            this.createUserButton.onClick.AddListener(() => { this.InitUser(APP_NAME, USER_NAME); });
        }

        //--------------------------------------------------------------------------------

        private void GetHueBridgeData() {

            Requester.Get(HUE_DISCOVERY_URL, (json) => {
                this.ParseHueBridgeData(json);
            });
        }

        //--------------------------------------------------------------------------------

        private void InitUser(string appName, string userName) {

            this.hueBridgeData.deviceType = $"{appName}#{userName}";

            JSONObject jsonObject = new JSONObject();
            jsonObject.AddValue("devicetype", new JSONString(this.hueBridgeData.deviceType));

            Requester.Post(this.ApiURL, jsonObject.ToString(), this.ParseUserName);
        }

        //--------------------------------------------------------------------------------

        private void ParseUserName(string json) {

            if (json.ToLower().Contains("error")) {

                this.ProcessErrorMessage(json);
                return;
            }

            Debug.Log("RECEIVED: " + json);

            try {

                JSONObject jsonObject = JSONParser.ToJSONArray(json)[0].AsObject();
                this.hueBridgeData.userName = jsonObject["success"].AsObject()["username"].AsString().Value;
            }
            catch (Exception e) {
                throw new Exception("Could not parse the 'User' JSON!", e);
            }
        }

        //--------------------------------------------------------------------------------

        private void ParseHueBridgeData(string json) {

            if (json.ToLower().Contains("error")) {

                this.ProcessErrorMessage(json);
                return;
            }

            Debug.Log("RECEIVED: " + json);

            try {

                JSONObject jsonObject = JSONParser.ToJSONArray(json)[0].AsObject();

                this.hueBridgeData.id =   jsonObject["id"].AsString().Value;                  //TODO "001788fffeae3e9d"
                this.hueBridgeData.ip =   jsonObject["internalipaddress"].AsString().Value;   //TODO "192.168.2.50"
                this.hueBridgeData.port = jsonObject["port"].AsInt().Value;                   //TODO "443"

                this.UpdateTexts();
            }
            catch (Exception e) {
                throw new Exception("Could not parse the 'HueBridge' JSON!", e);
            }
        }

        //--------------------------------------------------------------------------------

        private void ProcessErrorMessage(string json) {

            try {

                JSONObject jsonObject = JSONParser.ToJSONArray(json)[0].AsObject();
                int errorCode = jsonObject["error"].AsObject()["type"].AsInt().Value;

                RequestErrorCode code = (RequestErrorCode) errorCode;

                Debug.LogError($"ERROR '{code.ToString()}' while receiving data!\n{json}");
            }
            catch (Exception e) {
                Debug.LogError($"ERROR while receiving data! No JSON returned!\n{json}\n{e.Message}\n\n{e.StackTrace}");
            }
        }

        //--------------------------------------------------------------------------------

        private void UpdateTexts() {

            this.idTest.text = this.hueBridgeData.id.ToString();
            this.ipTest.text = this.hueBridgeData.ip.ToString();
            this.portTest.text = this.hueBridgeData.port.ToString();
            this.userTest.text = this.hueBridgeData.userName.ToString();
        }

        //--------------------------------------------------------------------------------
    }
}