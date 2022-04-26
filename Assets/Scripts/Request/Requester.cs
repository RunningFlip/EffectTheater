using System;
using System.Collections;
using System.IO;
using System.Text;
using Theater.Hue;
using UnityEngine;
using UnityEngine.Networking;

//--------------------------------------------------------------------------------

namespace Assets.Scripts.Request {

    public class Requester : MonoBehaviour {

        //--------------------------------------------------------------------------------
        // Inner enum
        //--------------------------------------------------------------------------------

        private enum RequestType {

            PUT = 0,
            GET = 1,
            POST = 2,
        }

        //--------------------------------------------------------------------------------
        // Constants
        //--------------------------------------------------------------------------------

        private string CERTIFICATE_PATH => Path.Combine(Application.dataPath, "DEBUGGING\\HueCertificate.cer");

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        private static Requester instance;
        private static Requester Instance {

            get {

                if (Requester.instance == null) {

                    GameObject g = new GameObject();
                    g.name = "Requester";

                    Requester component = g.AddComponent<Requester>();
                    Requester.instance = component;
                }

                return Requester.instance;
            }
        }

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public static void Get(string url, Action<string> onDataReceived) =>                 Instance.StartCoroutine(Instance.Send(RequestType.GET, url, onDataReceived));
        public static void Post(string url, string json, Action<string> onDataReceived) =>   Instance.StartCoroutine(Instance.Send(RequestType.POST, url, onDataReceived, json));
        public static void Put(string url, string json) =>                                   Instance.StartCoroutine(Instance.Send(RequestType.PUT, url, null, json));

        //--------------------------------------------------------------------------------

        private IEnumerator Send(RequestType requestType, string url, Action<string> getData, string json = "") {

            UnityWebRequest request = new UnityWebRequest(url, requestType.ToString());

            if (requestType != RequestType.GET) {
                request.uploadHandler = new UploadHandlerRaw(Encoding.UTF8.GetBytes(json));
            }

            request.downloadHandler = new DownloadHandlerBuffer();
            request.certificateHandler = new HueCertificateHandler(CERTIFICATE_PATH);
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success) {

                Debug.LogError($"Error: {request.error}");
                getData?.Invoke(request.error);
            }
            else {
                getData?.Invoke(request.downloadHandler.text);
            }
        }

        //--------------------------------------------------------------------------------
    }
}
