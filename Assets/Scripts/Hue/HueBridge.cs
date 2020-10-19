using UnityEngine;
using System.Collections.Generic;
using System.Net;
using MiniJSON;
using TMPro;
using UnityEngine.UI;
using System.IO;
using System.Text;


public class HueBridge : MonoBehaviour
{
	[Header("Login")]
	public string hostName = "127.0.0.1";
	public string username = "newdeveloper";
	public int portNumber = 8000;

	[Header("UI")]
	[SerializeField] private TMP_InputField hostInputField = null;
	[SerializeField] private TMP_InputField userInputField = null;
	[Space]
	[SerializeField] private Button searchLightButton = null;

	[Header("Lamp Controller")]
	[SerializeField] private LampController lampController = null;


	//Flag
	private bool connected;

	//Colors
	private Color defaultColor;


    private void Awake()
    {
		//Color
		defaultColor = ParametersHolder.Instance.applicationParameters.defaultColor;

		//Listener
		hostInputField.onValueChanged.AddListener(input => { hostName = input; IsValid(); });
		userInputField.onValueChanged.AddListener(input => { username = input; IsValid(); });
		searchLightButton.onClick.AddListener(DiscoverLights);

		//Setup
		hostInputField.text = hostName;
		userInputField.text = username;
		IsValid();
	}


	private void IsValid()
    {
		searchLightButton.interactable = hostInputField.text != "" && userInputField.text != "";
	}


    public void DiscoverLights()
	{
		HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://" + hostName + "/api/" + username + "/lights");
		HttpWebResponse response = (HttpWebResponse)request.GetResponse();
		Debug.Log("http" + hostName + portNumber + "/api/" + username + "/lights");

		Stream stream = response.GetResponseStream();
		StreamReader streamReader = new StreamReader(stream, Encoding.UTF8);

		Dictionary<string, object> lights = (Dictionary<string, object>)Json.Deserialize(streamReader.ReadToEnd());

		foreach (string key in lights.Keys)
		{
			Dictionary<string, object> light = (Dictionary<string, object>)lights[key];

			foreach (HueLamp hueLamp in GetComponentsInChildren<HueLamp>())
			{
				if (hueLamp.devicePath.Equals(key)) goto Found;
			}

			if (light["type"].Equals("Extended color light"))
			{
				GameObject gameObject = new GameObject();
				gameObject.name = "HueLamp " + (string)light["name"];
				gameObject.transform.parent = transform;
				gameObject.AddComponent<HueLamp>();

				HueLamp lamp = gameObject.GetComponent<HueLamp>();
				lamp.devicePath = key;

				if (lamp.on)
                {
					lampController.AddLamp(lamp);
				}
			}

		Found:
			;
		}
	}
}