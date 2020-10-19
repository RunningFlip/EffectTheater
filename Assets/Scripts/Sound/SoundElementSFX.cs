using UnityEngine;
using UnityEngine.UI;
using CustomSlider;
using System.Collections;


[RequireComponent(typeof(SoundHighlighterOneShot))]
public class SoundElementSFX : SoundElement
{
    [Header("Timer")]
    [SerializeField] private Button timerButton = null;
    [SerializeField] private Image timerImage = null;
    [SerializeField] private MinMaxSlider minMaxSlider = null;

    [Header("Sprites")]
    [SerializeField] private Sprite playSprite = null;
    [SerializeField] private Sprite pauseSprite = null;

    [Header("Color UI")]
    [SerializeField] protected Button overrideAllLampsButton = null;


    //Flag
    private bool activeTimer = false;
    private bool playingTimer;
    private bool overideAllLamps;

    //Color
    private Color defaultTimerColor;
    private Color activeTimerColor;

    //Highlighter
    private SoundHighlighterOneShot highlighter;


    public override void Setup(AudioSource _audioSource, AudioContainer _audioContainer, string _clipName)
    {
        base.Setup(_audioSource, _audioContainer, _clipName);

        //Timer
        defaultTimerColor = timerImage.color;
        activeTimerColor = ParametersHolder.Instance.applicationParameters.timerColor;
        timerButton.onClick.AddListener(OnTimerClicked);

        //Coloring
        colorImage.color = _audioContainer.ColorSet.mainColor;

        //Highlighter
        highlighter = GetComponent<SoundHighlighterOneShot>();
        playButton.onClick.AddListener(ShowHighlight);

        //Listener
        overrideAllLampsButton.onClick.AddListener(
            delegate
            {
                overideAllLamps = !overideAllLamps;
                overrideAllLampsButton.image.color = overideAllLamps
                    ? ParametersHolder.Instance.applicationParameters.timerColor
                    : Color.white;
            });
    }


    protected override void OnClick()
    {
        if (activeTimer)
        {
            playingTimer = !playingTimer;

            if (playingTimer)
            {
                StopCoroutine(LoopedSFX());
                StartCoroutine(LoopedSFX());
                playButtonImage.sprite = pauseSprite;
            }
            else
            {
                StopCoroutine(LoopedSFX());
                playButtonImage.sprite = playSprite;
            }
        }
        else
        {

            AudioContainer.AudioClipInfo clipInfo = audioContainer.GetRdmClipInfo();
            customVolumeScale = clipInfo.volume;
            audioSource.PlayOneShot(clipInfo.audioClip, Volume);

            float audioClipLength = clipInfo.audioClip.length;
            highlighter.Setup(audioClipLength);

            LampController.Instance.SetColor(colorImage.color, ColorType.SFX, overideAllLamps, clipInfo.coloringDuration);
        }
    }


    private void OnTimerClicked()
    {
        activeTimer = !activeTimer;

        if (activeTimer)
        {
            timerImage.color = activeTimerColor;

            playButtonImage.color = activeTimerColor;
            playButton.onClick.RemoveListener(ShowHighlight);
        }
        else
        {
            timerImage.color = defaultTimerColor;
            
            StopCoroutine(LoopedSFX());

            playingTimer = false;
            playButtonImage.sprite = playSprite;
            playButtonImage.color = defaultTimerColor;
            playButton.onClick.AddListener(ShowHighlight);
        }
    }


    private IEnumerator LoopedSFX()
    {
        MinMaxSlider.MinMaxValues values = minMaxSlider.Values;

        float passedTime = 0.0f;
        float rdmSec = 0.0f;

        while (activeTimer && playingTimer)
        {
            if (passedTime < rdmSec)
            {
                passedTime += Time.deltaTime;
            }
            else
            {
                AudioContainer.AudioClipInfo clipInfo = audioContainer.GetRdmClipInfo();
                customVolumeScale = clipInfo.volume;
                audioSource.PlayOneShot(clipInfo.audioClip, Volume);

                float audioClipLength = clipInfo.audioClip.length;

                LampController.Instance.SetColor(colorImage.color, ColorType.SFX, overideAllLamps, clipInfo.coloringDuration);

                highlighter.Setup(audioClipLength);
                highlighter.Highlight(true);

                passedTime = 0.0f;

                values = minMaxSlider.Values;
                rdmSec = Random.Range(values.minValue, values.maxValue);

                if (rdmSec < audioClipLength)
                {
                    rdmSec += audioClipLength;
                }
            }
            yield return null;
        }
    }


    private void ShowHighlight()
    {
        highlighter.Highlight(true);
    }
}