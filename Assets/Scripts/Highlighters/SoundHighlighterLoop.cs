using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class SoundHighlighterLoop : SoundHighlighter
{
    //UI
    [SerializeField] private Image backgroundImage = null;
    [SerializeField] private Image outlineImage = null;


    //Color
    private Color defaultColor;
    private Color highlightColor;
    

    private void Awake()
    {
        //Color
        defaultColor = backgroundImage.color;
        highlightColor = ParametersHolder.Instance.applicationParameters.playingBackgroundColor;

        //Color Setup
        outlineImage.color = ParametersHolder.Instance.applicationParameters.playingProgressColor;
    }


    public override void Highlight(bool _enabled)
    {
        if (_enabled)
        {
            backgroundImage.color = highlightColor;

            StopAllCoroutines();
            StartCoroutine(Progress());
        }
        else
        {
            backgroundImage.color = defaultColor;

            StopAllCoroutines();
            outlineImage.fillAmount = 0.0f;
        }
    }


    private IEnumerator Progress()
    {
        float passedTime = 0.0f;

        while (true)
        {
            if (passedTime >= audioClipDuration)
            {
                passedTime = 0.0f;
            }
            outlineImage.fillAmount = passedTime / audioClipDuration;
            passedTime += Time.deltaTime;
            yield return null;
        }
    }
}
