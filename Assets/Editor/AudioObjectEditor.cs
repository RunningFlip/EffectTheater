using System.Reflection;
using UnityEditor;
using UnityEngine;
using System;

[CustomEditor(typeof(AudioObject))]
public class AudioObjectEditor : Editor {

    public static AudioObject lastObject;
    public static AudioSource source;
    private float lastVolume;

    public override void OnInspectorGUI() {

        base.OnInspectorGUI();

        AudioObject audioObject = (AudioObject) target;

        if (audioObject == null) {
            return;
        }

        if (audioObject != null && lastObject != audioObject) {
            lastObject = audioObject;
            source?.Stop();
        }

        if (this.lastVolume != audioObject.volume && source != null) {

            this.lastVolume = audioObject.volume;
            source.volume = this.lastVolume;
        }

        EditorGUI.BeginDisabledGroup(audioObject.clip == null);
        {
            EditorGUILayout.Space();

            EditorGUI.BeginDisabledGroup(source == null);
            {
                if (source == null) {
                    EditorGUILayout.Slider(0, 0, 1);
                }
                else{
                    float progress = source.time / source.clip.length;
                    progress = EditorGUILayout.Slider(progress, 0, 1);
                    source.time = progress * source.clip.length;
                }
                
            }
            EditorGUI.EndDisabledGroup();

            EditorGUILayout.Space();

            bool sourceIsPlaying = source != null && source.isPlaying;
            string label = sourceIsPlaying
                ? "Stop"
                : "Play";

            if (GUILayout.Button(label, GUILayout.Height(40)) && audioObject.clip != null) {

                if (sourceIsPlaying) {
                    source.Stop();
                }
                else {

                    if (source == null) {

                        source = new GameObject().AddComponent<AudioSource>();
                        source.loop = false;
                    }

                    source.Stop();
                    source.clip = audioObject.clip;
                    source.volume = audioObject.volume;
                    source.Play();
                }
            }
        }
        EditorGUI.EndDisabledGroup();
    }
}