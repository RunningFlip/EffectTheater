using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//--------------------------------------------------------------------------------

namespace Theater.UI {

    [RequireComponent(typeof(Slider))]
    public class CustomSlider : MonoBehaviour {

        //--------------------------------------------------------------------------------
        // Properties
        //--------------------------------------------------------------------------------

        public float Value {

            get {

                if (this.slider == null) {
                    this.Initialize();
                }
                return this.slider.value;
            }

            set {

                if (this.slider == null) {
                    this.Initialize();
                }
                this.slider.value = value;
            }
        }

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        [SerializeField] private TextMeshProUGUI label;

        private Slider slider;

        public event Action<float> OnValueChanged;

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        private void Awake() {
            this.Initialize();
        }

        //--------------------------------------------------------------------------------

        private void Initialize() {

            this.slider = this.GetComponent<Slider>();

            this.slider.onValueChanged.AddListener((value) => {

                this.label.text = ((int)(value * 100f)).ToString();
                this.OnValueChanged?.Invoke(value);
            });
        }

        //--------------------------------------------------------------------------------
    }
}