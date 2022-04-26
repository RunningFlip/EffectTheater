using System;
using Theater.Coloring;
using Theater.Misc;

//--------------------------------------------------------------------------------

namespace Theater.Sounds {

    [Serializable]
    public class LoopHandler : SoundHandlerBase<LerpColorizer> {

        //--------------------------------------------------------------------------------
        // Properties
        //--------------------------------------------------------------------------------

        public float ClipLength => this.currentTuple?.AudioClip?.length ?? 0f;
        public float Time => this.soundEase?.Source?.time ?? 0f;

        //--------------------------------------------------------------------------------

        public override float MasterVolume { 

            get {
                return this.masterVolume;
            }

            set {
                this.masterVolume = value;

                if (this.soundEase != null) {
                    this.soundEase.Source.volume = this.currentTuple.Volume * this.masterVolume;
                }
            }
        }

        //--------------------------------------------------------------------------------

        public float Volume => this.currentTuple.Volume * this.MasterVolume;

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        private bool isPlaying;
        private float masterVolume = 1f;

        private SoundEase soundEase;
        private SoundTuple currentTuple;

        private static AudioSourcePool audioSourcePool = new AudioSourcePool(10);

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        public LoopHandler(SearchTag[] searchTags, string title, string[] associations, SoundTuple[] soundTuples) 
            : base(title, searchTags, associations, soundTuples) {
        }

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        public override void Play() {

            if (this.soundEase == null) {
                this.soundEase = LoopHandler.audioSourcePool.Get();
            }

            if (!this.isPlaying) {

                this.isPlaying = true;

                this.currentTuple = this.soundTuples.GetRandom();
                this.soundEase.Source.clip = this.currentTuple.AudioClip;
                this.soundEase.Source.loop = true;

                this.soundEase.Play(this);
            }
            else {

                this.isPlaying = false;

                this.soundEase.Stop();
                this.soundEase.OnStop -= this.OnStop;
                this.soundEase.OnStop += this.OnStop;
            }
        }

        //--------------------------------------------------------------------------------

        private void OnStop() {

            this.soundEase.OnStop -= this.OnStop;

            LoopHandler.audioSourcePool.Return(this.soundEase);
            this.soundEase = null;
        }

        //--------------------------------------------------------------------------------
    }
}