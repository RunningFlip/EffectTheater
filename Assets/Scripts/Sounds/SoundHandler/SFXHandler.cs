using System;
using Theater.Coloring;
using UnityEngine;

//--------------------------------------------------------------------------------

namespace Theater.Sounds {

    [Serializable]
    public class SFXHandler : SoundHandlerBase<SimpleColorizer> {

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        public SFXHandler(string title, SearchTag searchTag, SoundTuple[] soundTuples) 
            : base (title, searchTag, soundTuples) { }

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public override void Play() {

            SoundTuple tuple = this.soundTuples.GetRandom();
            AudioSource.PlayClipAtPoint(tuple.AudioClip, Vector3.zero, tuple.Volume);
        }

        //--------------------------------------------------------------------------------
    }
}