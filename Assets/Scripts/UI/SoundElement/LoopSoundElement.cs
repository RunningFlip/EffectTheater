using System.Collections.Generic;
using Theater.Coloring;
using Theater.Sounds;
using UnityEngine;
using UnityEngine.UI;

//--------------------------------------------------------------------------------

namespace Theater.UI {

    public class LoopSoundElement : SoundElementBase<SoundLoopHandler, LerpColorizer> {

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        [Header("Play Button Image")]
        [SerializeField] private Sprite playSprite;
        [SerializeField] private Sprite stopSprite;

        private bool isPlaying;

        private Image playButtonImage;
        private Color highlightColor = new Color(0.7764706f, 0f, 1f, 1f);

        private static List<LoopSoundElement> playingElements = new List<LoopSoundElement>();

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        protected override void OnInitialize(SoundLoopHandler soundHandler) {

            this.playButtonImage = this.playButton.image;
            this.playButton.onClick.AddListener(this.TogglePlayButton);
        }

        //--------------------------------------------------------------------------------

        protected override void OnApplyColor(Color color) { }

        //--------------------------------------------------------------------------------

        private void Update() {
            this.UpdateProgressBar();
        }

        //--------------------------------------------------------------------------------

        private void UpdateProgressBar() {

            if (this.isPlaying) {
                this.outline.fillAmount = this.soundHandler.Time / this.soundHandler.ClipLength;
            }
            else {
                this.outline.fillAmount = 0f;
            }
        }

        //--------------------------------------------------------------------------------

        private void TogglePlayButton() {

            this.isPlaying = !this.isPlaying;
            this.playButtonImage.sprite = !this.isPlaying
                ? this.playSprite
                : this.stopSprite;

            if (this.isPlaying) {

                this.StopAllElements();
                LoopSoundElement.playingElements.Add(this);
            }
            else {
                LoopSoundElement.playingElements.Remove(this);
            }

            this.ApplyColor(this.isPlaying ? highlightColor : defaultColor);
        }

        //--------------------------------------------------------------------------------

        private void StopAllElements() {

            for (int i = 0; i < LoopSoundElement.playingElements.Count; i++) {

                LoopSoundElement element = LoopSoundElement.playingElements[i];

                if (this.soundType == element.soundType) {
                    element.playButton.onClick.Invoke();
                }
            }
        }

        //--------------------------------------------------------------------------------
    }
}