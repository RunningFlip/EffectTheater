using System;
using UnityEngine;

//--------------------------------------------------------------------------------

namespace Theater.Sounds {

    [Serializable]
    public class SFXHandler : SoundHandlerBase {

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        public SFXHandler(string title, SoundTuple[] soundTuple) : base (title, soundTuple) { }

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public override void Play() {

            SoundTuple tupel = this.SoundTuple.GetRandom();
            AudioSource.PlayClipAtPoint(tupel.audioClip, Vector3.zero, tupel.volume);
        }

        //--------------------------------------------------------------------------------
    }
}