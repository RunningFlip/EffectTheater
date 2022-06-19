using System;
using System.Collections;
using UnityEngine;

//--------------------------------------------------------------------------------

namespace Theater.Sounds {

    [RequireComponent(typeof(AudioSource))]
    public class SoundEase : MonoBehaviour {

        //--------------------------------------------------------------------------------
        // Constants
        //--------------------------------------------------------------------------------

        private const float EASE_TIME = 2f;

        //--------------------------------------------------------------------------------
        // Properties
        //--------------------------------------------------------------------------------

        public AudioSource Source => this.source;

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        private bool isPlaying;

        private AudioSource source;
        private LoopHandler soundHandler;

        public event Action OnStop;

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        private void Awake() {
            this.source = this.GetComponent<AudioSource>();
        }

        //--------------------------------------------------------------------------------

        public void Play(LoopHandler soundHandler) {

            this.soundHandler = soundHandler;

            if (!this.isPlaying) {

                this.source.Play();
                this.source.volume = 0f;
            }

            this.isPlaying = true;

            this.StopAllCoroutines();
            this.StartCoroutine(this.EaseVolume(this.source.volume, this.soundHandler.Volume, null));
        }

        //--------------------------------------------------------------------------------

        public void Stop() {

            if (this.isPlaying) {

                this.StopAllCoroutines();
                this.StartCoroutine(this.EaseVolume(this.source.volume, 0f, this.Clear));
            }
        }

        //--------------------------------------------------------------------------------

        private IEnumerator EaseVolume(float from, float to, Action onDone) {
         
            float stepSize = 1f / SoundEase.EASE_TIME;
            float progress = 0f;

            while (progress < 1f) {

                this.source.volume = Mathf.Lerp(from, to, SoundEase.EASE_TIME * progress);
                progress += Time.deltaTime * stepSize;
                yield return null;
            }

            this.source.volume = to;

            onDone?.Invoke();
        }

        //--------------------------------------------------------------------------------

        public void Clear() {

            this.source.volume = 0f;

            this.isPlaying = false;
            this.source.Stop();
            this.OnStop?.Invoke();

            this.soundHandler = null;
        }

        //--------------------------------------------------------------------------------
    }
}
