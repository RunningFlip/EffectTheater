using UnityEngine;


public abstract class SoundHighlighter : MonoBehaviour
{
    //AudioClip
    protected float audioClipDuration;


    public void Setup(float _audioClipDuration)
    {
        audioClipDuration = _audioClipDuration;
    }


    public abstract void Highlight(bool _enabled);
}