using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AudioObject", menuName = "Create AudioObject")]
public class AudioObject : ScriptableObject {

    public enum SearchTag {

        //weapons
        Rifle = 0, 
        Shotgun = 1,
        Pistol = 5, 


        Shot = 2, 
        Shoot = 3, 

        Kill = 4, 
    }

    public string name = "none";
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.5f;
    public string[] associations;
    public SearchTag[] seachTags;
    public Color[] colors;
}
