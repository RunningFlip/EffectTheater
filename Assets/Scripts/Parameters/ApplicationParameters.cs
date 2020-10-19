using UnityEngine;


[CreateAssetMenu(fileName = "AppicationParameters", menuName = "ScriptableObjects/AppicationParameters")]
public class ApplicationParameters : ScriptableObject
{
    [Header("Paths")]
    public string sfxPath = "Assets/Audio/SFX";
    public string ambientPath = "Assets/Audio/Ambient";
    public string musicPath = "Assets/Audio/Music";

    [Header("Sound Element Prefabs")]
    public GameObject soundElementPrefabSFX;
    public GameObject soundElementPrefabAmbient;
    public GameObject soundElementPrefabMusic;

    [Header("Volume Scale")]
    [Range(0.0f, 1.0f)] public float volumeSfx = 1.0f;
    [Range(0.0f, 1.0f)] public float volumeScaleAmbient = 1.0f;
    [Range(0.0f, 1.0f)] public float volumeScaleMusic = 1.0f;

    [Header("Easeing")]
    public float easeTime = 1.0f;
    public AnimationCurve easeCurve;

    [Header("Highlight Coloring")]
    public Color playingBackgroundColor;
    public Color playingProgressColor;
    public Color timerColor;

    [Header("Hue Coloring")]
    public Color defaultColor = Color.white;
    public float updateTime = 0.4f;
    public float defaultColoringTime = 0.5f;

    [Header("Audio Containers")]
    public AudioContainer[] audioContainersSFX;
    public AudioContainer[] audioContainersAmbient;
    public AudioContainer[] audioContainersMusic;
}