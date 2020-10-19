using System.Collections;
using UnityEngine;


public class LampContainer : MonoBehaviour
{
	//Lamp	
	private HueLamp hueLamp;
	private float updateTime;
	private Color defaultColor;

	//SFX
	private bool sfxOn;	
	public Color sfxColor;

	//Ambient
	private bool ambientOn;
	private float ambientLerpTime;
	public Color ambientMainColor;
	public Color ambientLerpColor;
	public Color ambientCurrentLerpColor;

	//Music
	private int musicOn = 0;
	public Color musicColor;


	private bool MusicIsOn => musicOn > 0;


	private void Awake()
    {
		defaultColor = ParametersHolder.Instance.applicationParameters.defaultColor;
		updateTime = ParametersHolder.Instance.applicationParameters.updateTime;
	}


    public void SetLamp(HueLamp _hueLamp)
    {
        hueLamp = _hueLamp;
		hueLamp.color = defaultColor;
	}


	public void ActivateLamp(ColorType _colorType, ColorSet _colorSet, float _time = 0.5f)
	{
		switch (_colorType) 
		{
			case ColorType.None:
				musicOn = 0;
				ambientOn = false;
				sfxOn = false;

				musicColor = defaultColor;
				ambientMainColor = defaultColor;
				ambientLerpColor = defaultColor;
				ambientCurrentLerpColor = defaultColor;
				sfxColor = defaultColor;
				hueLamp.color = defaultColor;

				StopAllCoroutines();
				break;

			case ColorType.SFX:
				sfxOn = true;

				sfxColor = _colorSet.mainColor;
				hueLamp.color = sfxColor;

				StopCoroutine(ColorRoutineSFX(_time));
				StartCoroutine(ColorRoutineSFX(_time));
				break;

			case ColorType.Ambient:
				ambientOn = true;
				ambientLerpTime = _time;

				ambientMainColor = _colorSet.mainColor;
				ambientLerpColor = _colorSet.lerpColor;
				hueLamp.color = ambientMainColor;
				StopCoroutine(ColorRoutineAmbient());
				StartCoroutine(ColorRoutineAmbient());
				break;

			case ColorType.Music:
				musicOn += 1;

				musicColor = _colorSet.mainColor;
				hueLamp.color = musicColor;
				break;
		}
	}


	public void DeactivateLamp(ColorType _colorType)
    {
		switch (_colorType)
		{
			case ColorType.Ambient:
				StopCoroutine(ColorRoutineAmbient());

				ambientOn = false;

				ambientMainColor = defaultColor;
				ambientLerpColor = defaultColor;
				ambientCurrentLerpColor = defaultColor;
				hueLamp.color = (MusicIsOn) ? musicColor : defaultColor;
				break;

			case ColorType.Music:
				musicOn -= 1;

				if (!MusicIsOn)
                {
					musicColor = defaultColor;
					hueLamp.color = (ambientOn) ? ambientMainColor : defaultColor;
				}
				break;
		}
	}


	private IEnumerator ColorRoutineSFX(float _time)
	{
		yield return new WaitForSeconds(_time);
		hueLamp.color = (ambientOn)
			? ambientCurrentLerpColor
			: (MusicIsOn)
				? musicColor
				: defaultColor;

		sfxOn = false;
	}


	private IEnumerator ColorRoutineAmbient()
    {
		bool lerpIn = true;

		float lerpTime = Random.Range(1.0f, ambientLerpTime);
		float passedTime = 0.0f;

		while (ambientOn)
        {
			if (passedTime > lerpTime)
            {
				passedTime = 0.0f;
				lerpIn = !lerpIn;
			}

			if (!sfxOn)
            {
				if (lerpIn)
				{
					ambientCurrentLerpColor = Color.Lerp(ambientMainColor, ambientLerpColor, passedTime / lerpTime);
					hueLamp.color = ambientCurrentLerpColor;
				}
				else
				{
					ambientCurrentLerpColor = Color.Lerp(ambientLerpColor, ambientMainColor, passedTime / lerpTime);
					hueLamp.color = ambientCurrentLerpColor;
				}
			}

			yield return new WaitForSeconds(updateTime);
			passedTime += updateTime;
		}		
    }
}