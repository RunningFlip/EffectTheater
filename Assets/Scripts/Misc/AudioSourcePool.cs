using Theater.Pool;
using Theater.Sounds;
using UnityEngine;

//--------------------------------------------------------------------------------

namespace Theater.Misc {

    public class AudioSourcePool : Pool<SoundEase> {

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        public AudioSourcePool(int capacity) : base(capacity) { }

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        protected override SoundEase CreatePoolElement() {

            GameObject gameObject = new GameObject();
            gameObject.name = $"{this.Size + 1}. Pool AudioSource";

            return gameObject.AddComponent<SoundEase>();
        }

        //--------------------------------------------------------------------------------

        protected override void ResetReturnedItem(SoundEase item) {

            AudioSource source = item.Source;

            source.Stop();
            source.volume = 1f;
            source.time = 0f;
            source.clip = null;
            source.loop = false;
        }

        //--------------------------------------------------------------------------------
    }
}