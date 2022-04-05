using System;
using UnityEngine;

//--------------------------------------------------------------------------------

namespace Theater.Sounds {

    [Serializable]
    public class SoundLoopHandler : SoundHandlerBase {

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        private AudioSource source;

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        public SoundLoopHandler(AudioSource source, string title, SoundTuple[] soundTuple) 
            : base(title, soundTuple) {

            this.source = source;
        }

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public override void Play() {

            SoundTuple tupel = this.SoundTuple.GetRandom();

            this.source.clip = tupel.audioClip;
            this.source.volume = tupel.volume;
            this.source.Play();
        }

        //--------------------------------------------------------------------------------
    }
}