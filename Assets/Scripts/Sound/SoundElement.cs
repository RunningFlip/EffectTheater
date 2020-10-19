using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Animator))]
public abstract class SoundElement : MonoBehaviour
{   
    [Header("General UI")]
    public Button playButton = null;
    [SerializeField] protected Image playButtonImage = null;
    [SerializeField] protected TextMeshProUGUI clipLabel = null;

    [Header("Expand UI")]
    [SerializeField] protected Button expandButton = null;
    [SerializeField] protected Image expandButtonImage = null;
    [Space]
    [SerializeField] protected Sprite expandSprite = null;
    [SerializeField] protected Sprite collapseSprite = null;

    [Header("Color UI")]
    [SerializeField] protected Button colorButton = null;
    [SerializeField] protected Image colorImage = null;


    //Flag
    private bool expand;

    //Audio
    protected AudioSource audioSource;
    protected float volumeScale;
    protected float customVolumeScale = 1.0f;
    protected AudioContainer audioContainer;

    //Coloring
    protected float defaultColoringTime;

    //Components
    private Animator animator;

    //Event
    protected event Action OnVolumeChanged;


    //Getter
    public string AudioContainerName => audioContainer.name;
    protected float Volume => volumeScale * customVolumeScale;


    public virtual void Setup(AudioSource _audioSource, AudioContainer _audioContainer, string _clipName)
    {
        //Components
        animator = GetComponent<Animator>();

        //AudioClip
        audioSource = _audioSource;
        audioContainer = _audioContainer;

        //Coloring
        defaultColoringTime = ParametersHolder.Instance.applicationParameters.defaultColoringTime;

        //Text
        clipLabel.text = _clipName;

        //Listener
        playButton.onClick.AddListener(OnClick);
        expandButton.onClick.AddListener(OnExpand);
        OnVolumeChanged += () => audioSource.volume = Volume;
    }


    public virtual void OnStop() { }
    protected abstract void OnClick(); 


    private void OnExpand()
    {
        expand = !expand;

        if (expand)
        {
            expandButtonImage.sprite = collapseSprite;
            animator.SetTrigger("Expand");   
        }
        else 
        {
            expandButtonImage.sprite = expandSprite;
            animator.SetTrigger("Collapse");
        }
    }


    public void SetVolume(float _volumeScale)
    {
        volumeScale = _volumeScale;
        OnVolumeChanged?.Invoke();
    }
}