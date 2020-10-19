using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class SoundHighlighterOneShot : SoundHighlighter
{
    //UI
    [SerializeField] private Image progressImage = null;


    private void Awake()
    {
        progressImage.color = ParametersHolder.Instance.applicationParameters.playingProgressColor;
    }


    public override void Highlight(bool _enabled)
    {
        if (_enabled)
        {
            StopCoroutine(Progress());
            StartCoroutine(Progress());
        }   
    }


    private IEnumerator Progress()
    {
        float passedTime = 0.0f;

        while (passedTime < audioClipDuration)
        {
            progressImage.fillAmount = passedTime / audioClipDuration;

            passedTime += Time.deltaTime;
            yield return null;
        }
        progressImage.fillAmount = 0.0f;
    }
}