using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(AudioContainer)), CanEditMultipleObjects]
public class AudioContainerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        AudioContainer container = (AudioContainer)target;

        foreach (AudioContainer.AudioClipInfo info in container.audioClipInfos)
        {
            if (!info.useCustomDuration)
            {
                info.coloringDuration = info.audioClip.length;
            }
        }
    }
}
