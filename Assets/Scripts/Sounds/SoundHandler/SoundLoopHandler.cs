using Assets.Scripts.Misc;
using System;
using Theater.Coloring;
using UnityEngine;

//--------------------------------------------------------------------------------

namespace Theater.Sounds {

    [Serializable]
    public class SoundLoopHandler : SoundHandlerBase<LerpColorizer> {

        //--------------------------------------------------------------------------------
        // Properties
        //--------------------------------------------------------------------------------

        public float ClipLength => this.currentTuple?.AudioClip?.length ?? 0f;
        public float Time => this.source?.time ?? 0f;

        //--------------------------------------------------------------------------------

        public override float MasterVolume { 

            get {
                return this.masterVolume;
            }

            set {
                this.masterVolume = value;

                if (this.source != null) {
                    this.source.volume = this.currentTuple.Volume * this.masterVolume;
                }
            }
        }

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        private float masterVolume = 1f;

        private AudioSource source;
        private SoundTuple currentTuple;

        private static AudioSourcePool audioSourcePool = new AudioSourcePool(10);

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        public SoundLoopHandler(SearchTag searchTag, string title, SoundTuple[] soundTuples) 
            : base(title, searchTag, soundTuples) {
        }

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public override void Play() {

            if (this.source == null) {
                this.source = audioSourcePool.Get();
            }

            if (!this.source.isPlaying) {

                this.currentTuple = this.soundTuples.GetRandom();
                this.source.clip = this.currentTuple.AudioClip;
                this.source.volume = this.currentTuple.Volume * this.MasterVolume;
                this.source.loop = true;
                this.source.Play();
            }
            else {

                this.source.Stop();
                audioSourcePool.Return(this.source);
                this.source = null;
            }
        }

        //--------------------------------------------------------------------------------
    }
}