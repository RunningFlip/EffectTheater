using System;
using Theater.Sounds;
using UnityEditor;
using UnityEngine;

//--------------------------------------------------------------------------------

[CustomPropertyDrawer(typeof(SoundTuple))]
public class SoundTupleProperty : PropertyDrawer {

    //--------------------------------------------------------------------------------
    // Constants
    //--------------------------------------------------------------------------------

    private const int DEFAULT_HEIGHT = 16;
    private const int PROPERTY_HEIGHT = 16;
    private const int SLIDER_HEIGHT = 20;
    private const int BUTTON_HEIGHT = 20;

    private const int SPACE = 5;

    //--------------------------------------------------------------------------------
    // Fields
    //--------------------------------------------------------------------------------

    private static bool selectionChangedRegistered;

    private float drawerheight;

    private static AudioSource source;
    private static SoundTuple playingTuple;

    private AudioClip audioClip;
    private float volume;

    //--------------------------------------------------------------------------------
    // Abstract methods
    //--------------------------------------------------------------------------------

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {

        if (!selectionChangedRegistered) {

            selectionChangedRegistered = true;
            Selection.selectionChanged -= this.ResetAudioDebugging;
            Selection.selectionChanged += this.ResetAudioDebugging;
        }

        float backupHeight = this.drawerheight;
        this.drawerheight = DEFAULT_HEIGHT;

        EditorGUI.BeginProperty(position, label, property);
        {
            position.height = this.drawerheight;

            property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label, true);

            if (property.isExpanded) {

                int indent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;

                // Properties
                SerializedProperty titleProperty = property.FindPropertyRelative("title");
                SerializedProperty volumeProperty = this.DrawProperty(position, property, "volume", "Volume");

                this.volume = Math.Clamp(volumeProperty.floatValue, 0f, 1f);
                volumeProperty.SetValue<object>(this.volume);

                this.audioClip = this.DrawProperty(position, property, "audioClip", "AudioClip").GetValue<AudioClip>();
                titleProperty.SetValue(this.audioClip != null ? this.audioClip.name : "missing clip");

                // Action
                SoundTuple tuple = property.GetValue<SoundTuple>();
                bool isPlaying = playingTuple == tuple;
                bool disableUI = audioClip == null;
                string buttonLabel = isPlaying ? "Stop" : "Play";
                Action buttonAction = isPlaying ? this.StopAudioClip : () => { this.PlayAudioClip(tuple); };

                // Draw slider
                if (isPlaying && source != null && this.audioClip != null) {

                    source.volume = this.volume;
                    source.time = this.DrawSlider(position, source.time, 0, this.audioClip.length, !isPlaying || disableUI);

                    if (isPlaying && !source.isPlaying) {
                        this.StopAudioClip();
                    }
                }
                else {
                    this.DrawSlider(position, 0, 0f, 1f, true);
                }

                // Draw button
                this.DrawButton(position, buttonLabel, () => { isPlaying = !isPlaying; buttonAction.Invoke(); }, disableUI);

                EditorGUI.indentLevel = indent;
            }
        }
        EditorGUI.EndProperty();

        // We have to do this check, because sometimes OnGUI will not enter the "StartProperty" chunk, so the size is wrong.
        // Instead we will cache the last correct height and apply it in this case.
        if (this.drawerheight == DEFAULT_HEIGHT) {
            this.drawerheight = backupHeight;
        }
        else {
            this.drawerheight += SPACE + SPACE + DEFAULT_HEIGHT;
        }
    }

    //--------------------------------------------------------------------------------

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
        return property.isExpanded ? this.drawerheight : DEFAULT_HEIGHT;
    }

    //--------------------------------------------------------------------------------
    // Draw methods
    //--------------------------------------------------------------------------------

    private SerializedProperty DrawProperty(Rect position, SerializedProperty property, string propertyName, string propertyLabel, bool disabled = false) {

        this.drawerheight += SPACE + PROPERTY_HEIGHT;

        SerializedProperty foundProperty = property.FindPropertyRelative(propertyName);
        Rect rect = new Rect(position.x, position.y + drawerheight, position.width, PROPERTY_HEIGHT);
        EditorGUI.BeginDisabledGroup(disabled); 
        {
            EditorGUI.PropertyField(rect, foundProperty, new GUIContent(propertyLabel));
        }
        EditorGUI.EndDisabledGroup();

        return foundProperty;
    }

    //--------------------------------------------------------------------------------

    private void DrawButton(Rect position, string label, Action action, bool disabled = false) {

        this.drawerheight += SPACE + BUTTON_HEIGHT;

        Rect rect = new Rect(position.x, position.y + this.drawerheight, position.width, BUTTON_HEIGHT);
        EditorGUI.BeginDisabledGroup(disabled);
        {
            if (GUI.Button(rect, label)) {
                action?.Invoke();
            }
        }
        EditorGUI.EndDisabledGroup();
    }

    //--------------------------------------------------------------------------------

    private float DrawSlider(Rect position, float value, float minValue, float maxValue, bool disabled = false) {

        this.drawerheight += SPACE + SLIDER_HEIGHT;

        float valueFieldSize = 50f;

        Rect sliderRect = new Rect(position.x, position.y + this.drawerheight, position.width - valueFieldSize, SLIDER_HEIGHT);
        Rect labelRect  = new Rect(position.x + (position.width - valueFieldSize + SPACE), position.y + this.drawerheight, valueFieldSize - SPACE, SLIDER_HEIGHT);
        EditorGUI.BeginDisabledGroup(disabled); 
        {
            value = EditorGUI.FloatField(labelRect, value);
            value = Math.Clamp(value, minValue, maxValue);
            value = GUI.HorizontalSlider(sliderRect, value, minValue, maxValue);
        }
        EditorGUI.EndDisabledGroup();

        return value;
    }

    //--------------------------------------------------------------------------------
    // Methods
    //--------------------------------------------------------------------------------

    private void PlayAudioClip(SoundTuple tuple) {

        if (this.audioClip != null) {

            if (source != null && source.isPlaying) {
                this.StopAudioClip();
            }

            if (source == null) {
                source = new GameObject().AddComponent<AudioSource>();
                source.name = $"Tuple - {tuple.AudioClip.name}";
            }

            playingTuple = tuple;
            source.clip = this.audioClip;
            source.volume = this.volume;
            source.Play();
        }
    }

    //--------------------------------------------------------------------------------

    private void StopAudioClip() {

        playingTuple = null;

        if (source != null) {

            source.Stop();
            GameObject.DestroyImmediate(source.gameObject);
        }
    }

    //--------------------------------------------------------------------------------

    private void ResetAudioDebugging() {

        selectionChangedRegistered = false;
        Selection.selectionChanged -= this.ResetAudioDebugging;
        this.StopAudioClip();
    }

    //--------------------------------------------------------------------------------
}