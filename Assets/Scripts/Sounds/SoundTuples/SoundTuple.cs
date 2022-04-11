using UnityEngine;
using System;

//--------------------------------------------------------------------------------

namespace Theater.Sounds {

    [Serializable]
    public class SoundTuple {

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        public string Title => this.title;
        public float Volume => this.volume;
        public AudioClip AudioClip => this.audioClip;

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        [SerializeField] private string title;
        [SerializeField, Range(0f, 1f)] private float volume;
        [SerializeField] public AudioClip audioClip;
        
        //-------------------------------------------------------------------------------
        // Constructor
        //-------------------------------------------------------------------------------

        public SoundTuple(string title, AudioClip audioClip, float volume) {

            this.title = title;
            this.volume = volume;
            this.audioClip = audioClip;
        }

        //--------------------------------------------------------------------------------
    }
}