using Theater.Pool;
using UnityEngine;

//--------------------------------------------------------------------------------

namespace Assets.Scripts.Misc {

    public class AudioSourcePool : Pool<AudioSource> {

        //--------------------------------------------------------------------------------
        // Constructor
        //--------------------------------------------------------------------------------

        public AudioSourcePool(int capacity) : base(capacity) { }

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        protected override AudioSource CreatePoolElement() {

            GameObject g = new GameObject();
            g.name = $"{this.Size + 1}. Pool AudioSource";
            return g.AddComponent<AudioSource>();
        }

        //--------------------------------------------------------------------------------

        protected override void ResetReturnedItem(AudioSource item) {

            item.Stop();
            item.volume = 1f;
            item.time = 0f;
            item.clip = null;
            item.loop = false;
        }

        //--------------------------------------------------------------------------------
    }
}