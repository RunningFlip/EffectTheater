using UnityEngine;
using System;


[CreateAssetMenu(fileName = "AudioContainer", menuName = "ScriptableObjects/AudioContainer")]
public class AudioContainer : ScriptableObject
{
    [Serializable]
    public class AudioClipInfo
    {
        public AudioClip audioClip;
        [Range(0.0f, 1.0f)] public float volume = 1.0f;
        public bool useCustomDuration;
        [Range(0.0f, 60.0f)] public float coloringDuration;
    }


    [SerializeField] private SoundType soundType = SoundType.SFX;

    [Header("Coloring")]
    [SerializeField] private ColorSet colorSet = null;

    [Header("Audio Clips")]
    public AudioClipInfo[] audioClipInfos;


    //Getter
    public SoundType SoundType => soundType;
    public ColorSet ColorSet => colorSet;


    public AudioClipInfo GetRdmClipInfo()
    {
        return audioClipInfos[UnityEngine.Random.Range(0, audioClipInfos.Length)]; 
    }


    public AudioClipInfo PlayOneShot(AudioSource _source, float _volumeScale)
    {
        AudioClipInfo info = audioClipInfos[UnityEngine.Random.Range(0, audioClipInfos.Length)];
        _source.PlayOneShot(info.audioClip, info.volume * _volumeScale);

        return info;
    }
}