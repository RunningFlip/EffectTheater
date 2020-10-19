using System.Collections;
using UnityEngine;


[RequireComponent(typeof(SoundHighlighterLoop))]
public class SoundElementMusic : SoundElement
{
    [Header("Sprites")]
    [SerializeField] private Sprite playSprite = null;
    [SerializeField] private Sprite pauseSprite = null;


    //Flag
    private bool isPlaying = false;

    //Time
    private float currentTime;
    private float easeTime;
    private float passedTimeEase;

    //AnimationCurve
    private AnimationCurve curve;

    //Highlighter
    private SoundHighlighterLoop highlighter;


    //Getter
    public bool IsPlaying => isPlaying;


    public override void Setup(AudioSource _audioSource, AudioContainer _audioContainer, string _clipName)
    {
        //AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();

        base.Setup(audioSource, _audioContainer, _clipName);

        //Audio
        audioSource.loop = true;

        //Time
        easeTime = ParametersHolder.Instance.applicationParameters.easeTime;
        curve = ParametersHolder.Instance.applicationParameters.easeCurve;

        //Coloring
        colorImage.color = _audioContainer.ColorSet.mainColor;

        //Highlighter
        highlighter = GetComponent<SoundHighlighterLoop>();
    }


    protected override void OnClick()
    {
        isPlaying = !isPlaying;

        if (isPlaying)
        {
            Play();
        }
        else
        {
            Stop();
        }
    }


    public override void OnStop()
    {
        if (isPlaying)
        {
            isPlaying = false;
            Stop();
        }
    }


    public void Play()
    {
        AudioContainer.AudioClipInfo clipInfo = audioContainer.GetRdmClipInfo();
        float audioClipLength = clipInfo.audioClip.length;
        customVolumeScale = clipInfo.volume;

        audioSource.clip = clipInfo.audioClip;
        audioSource.time = currentTime;
        audioSource.volume = Volume;

        highlighter.Setup(audioClipLength);
        highlighter.Highlight(true);

        playButtonImage.sprite = pauseSprite;
        
        StopAllCoroutines();
        StartCoroutine(EaseIn(clipInfo.coloringDuration));
    }


    public void Stop()
    {
        highlighter.Highlight(false);

        playButtonImage.sprite = playSprite;
        currentTime = audioSource.time;
        StopAllCoroutines();
        StartCoroutine(EaseOut());
    }


    private IEnumerator EaseIn(float _coloringTime)
    {
        LampController.Instance.SetColor(colorImage.color, ColorType.Music, true, _coloringTime);
        audioSource.Play();

        while (passedTimeEase < easeTime)
        {
            float volume = curve.Evaluate(passedTimeEase / easeTime) * Volume;
            audioSource.volume = volume;

            passedTimeEase += Time.deltaTime;
            yield return null;
        }
        passedTimeEase = 0.0f;
        audioSource.volume = Volume;
    }


    private IEnumerator EaseOut()
    {
        while (passedTimeEase < easeTime)
        {
            float volume = (1.0f - curve.Evaluate(passedTimeEase / easeTime)) * Volume;
            audioSource.volume = volume;

            passedTimeEase += Time.deltaTime;
            yield return null;
        }
        passedTimeEase = 0.0f;
        audioSource.volume = 0.0f;

        audioSource.Stop();
        LampController.Instance.DeactivateColor(ColorType.Music);
    } 
}