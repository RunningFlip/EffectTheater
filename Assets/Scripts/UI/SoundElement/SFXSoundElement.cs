using Theater.Coloring;
using Theater.Sounds;
using UnityEngine;
using UnityEngine.UI;

//--------------------------------------------------------------------------------

namespace Theater.UI {

    public class SFXSoundElement : SoundElementBase<SFXHandler, SimpleColorizer> {

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        [Header("SFX")]
        [SerializeField] private Button timerButton;
        [SerializeField] private Image timerProgressBar;
        [SerializeField] private MinMaxSlider minMaxSlider;

        private bool activeTimer;

        private float nextExecutionTime = -1f;
        private float passedTime;

        private Color highlightColor = new Color(0.7764706f, 0f, 1f, 1f);

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        protected override void OnInitialize(SFXHandler soundHandler) {
            this.timerButton.onClick.AddListener(this.ToggleTimer);
        }

        //--------------------------------------------------------------------------------

        protected override void OnApplyColor(Color color) {

            this.timerButton.image.color = color;
            this.timerProgressBar.color = color;
        }

        //--------------------------------------------------------------------------------

        private void Update() {
            
            if (this.activeTimer) {

                this.timerProgressBar.fillAmount = this.passedTime / this.nextExecutionTime;

                if (this.passedTime >= this.nextExecutionTime) {

                    this.soundHandler.MasterVolume = this.MasterVolume;
                    this.soundHandler.Play();
                    this.NextTimer();
                }

                this.passedTime += Time.deltaTime;
            }
        }

        //--------------------------------------------------------------------------------

        private void ToggleTimer() {

            this.activeTimer = !this.activeTimer;

            if (this.activeTimer) {

                this.ApplyColor(highlightColor);
                this.timerProgressBar.enabled = true;
                this.outline.fillAmount = 1f;
                this.playButton.interactable = false;
                this.NextTimer();
            }
            else {

                this.ApplyColor(defaultColor);
                this.timerProgressBar.enabled = false;
                this.outline.fillAmount = 0f;
                this.timerProgressBar.fillAmount = 0f;
                this.playButton.interactable = true;
                this.passedTime = 0f;
                this.nextExecutionTime = -1f;
            }
        }

        //--------------------------------------------------------------------------------

        private void NextTimer() {

            this.passedTime = 0f;

            MinMaxSlider.MinMaxValues values = this.minMaxSlider.Values;
            this.nextExecutionTime = UnityEngine.Random.Range(values.minValue, values.maxValue);
        }

        //--------------------------------------------------------------------------------
    }
}