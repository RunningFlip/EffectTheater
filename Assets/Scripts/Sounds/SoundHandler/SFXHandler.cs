using System;
using Theater.Coloring;
using UnityEngine;

//--------------------------------------------------------------------------------

namespace Theater.Sounds {

    [Serializable]
    public class SFXHandler : SoundHandlerBase<SimpleColorizer> {

        //--------------------------------------------------------------------------------
        // Properties
        //--------------------------------------------------------------------------------

        public override float MasterVolume { get; set; } = 1f;

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        public SFXHandler(string title, SearchTag[] searchTags, string[] associations, SoundTuple[] soundTuples) 
            : base (title, searchTags, associations, soundTuples) { }

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public override void Play() {

            SoundTuple tuple = this.soundTuples.GetRandom();
            AudioSource.PlayClipAtPoint(tuple.AudioClip, Vector3.zero, tuple.Volume * this.MasterVolume);
        }

        //--------------------------------------------------------------------------------
    }
}