using UnityEngine;
using System;

//--------------------------------------------------------------------------------

namespace Theater.Sounds {

    [Serializable]
    public class SoundTuple {

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        public string title = "none";
        public float volume = 1f;
        public AudioClip audioClip;
        public Color[] colors;

        //--------------------------------------------------------------------------------
        // Constructor
        //-------------------------------------------------------------------------------

        public SoundTuple(string title, AudioClip audioClip, float volume, Color[] colors) {

            this.title = title;
            this.volume = volume;
            this.audioClip = audioClip;
            this.colors = colors;
        }

        //--------------------------------------------------------------------------------
    }
}