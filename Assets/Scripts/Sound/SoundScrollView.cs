using System.Collections.Generic;
using UnityEngine;


public class SoundScrollView : MonoBehaviour
{
    [SerializeField] private SoundType soundType = SoundType.SFX;

    [Header("UI")]
    [SerializeField] private DefaultSlider volumeSlider = null;

    [Header("Spawning")]
    [SerializeField] private Transform contentTransform = null;


    //Elements
    private List<SoundElement> soundElements = new List<SoundElement>();

    //Components
    private AudioSource source;


    private void Start()
    {
        //Components
        source = GetComponent<AudioSource>();

        //Event
        volumeSlider.OnValueChanged += x => UpdateVolume(x);

        //Setup
        ApplicationParameters parameters = ParametersHolder.Instance.applicationParameters;
        float volume = 0.0f;

        switch (soundType)
        {
            case SoundType.SFX:
                CreateSoundSelectionElements(parameters.soundElementPrefabSFX, parameters.audioContainersSFX);
                volume = parameters.volumeSfx;
                break;

            case SoundType.Ambient:
                CreateSoundSelectionElements(parameters.soundElementPrefabAmbient, parameters.audioContainersAmbient);
                volume = parameters.volumeScaleAmbient;
                break;

            case SoundType.Music:
                CreateSoundSelectionElements(parameters.soundElementPrefabMusic, parameters.audioContainersMusic);
                volume = parameters.volumeScaleMusic;
                break;
        }
        UpdateVolume(volume);
        volumeSlider.SetValue(volume);
    }


    private void CreateSoundSelectionElements(GameObject _prefab, AudioContainer[] _audioContainers)
    {
        foreach (AudioContainer container in _audioContainers) 
        {
            GameObject element = Instantiate(_prefab, contentTransform);

            SoundElement soundElement = element.GetComponent<SoundElement>();
            soundElements.Add(soundElement);
            soundElement.Setup(source, container, container.name);

            if (container.SoundType == SoundType.Music)
            {
                soundElement.playButton.onClick.AddListener(() => StopAllElementsWihout((SoundElementMusic)soundElement));
            }
        }
    }


    private void StopAllElementsWihout(SoundElementMusic _elementException)
    {
        if (_elementException.IsPlaying)
        {
            SoundElement element = null;

            for (int i = 0; i < soundElements.Count; i++)
            {
                element = (SoundElementMusic)soundElements[i];

                if (element != _elementException)
                {
                    element.OnStop();
                }
            }
        }
    }


    private void UpdateVolume(float _volume)
    {
        switch (soundType)
        {
            case SoundType.SFX:
                ParametersHolder.Instance.applicationParameters.volumeSfx = _volume;
                break;
            case SoundType.Ambient:
                ParametersHolder.Instance.applicationParameters.volumeScaleAmbient = _volume;
                break;
            case SoundType.Music:
                ParametersHolder.Instance.applicationParameters.volumeScaleMusic = _volume;
                break;
        }

        for (int i = 0; i < soundElements.Count; i++)
        {
            soundElements[i].SetVolume(_volume);
        }
    }


    public void Filter(string _filter)
    {
        _filter = _filter.ToLower();

        for (int i = 0; i < soundElements.Count; i++)
        {
            SoundElement element = soundElements[i];
            string elementName = element.AudioContainerName.ToLower();

            element.gameObject.SetActive(elementName.Contains(_filter)); //TODO ! not an optimal solution
        }
    }
}