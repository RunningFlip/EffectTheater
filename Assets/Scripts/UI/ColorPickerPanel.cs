using System;
using UnityEngine;
using UnityEngine.UI;

//--------------------------------------------------------------------------------

namespace Theater.UI {

    public class ColorPickerPanel : MonoBehaviour {

        //--------------------------------------------------------------------------------
        // Properties
        //--------------------------------------------------------------------------------

        public Color Color { get; private set; }

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        [Header("Panel")]
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private ColorPicker colorPicker;
        [SerializeField] private Button submitButton;
        [SerializeField] private Button closeButton;

        private bool colorPickerIsOpen;

        public Action<Color> OnColorPicked;
        public Action OnClosed;

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        private void Awake() {

            this.submitButton.onClick.AddListener(() => this.Close(true));
            this.closeButton.onClick.AddListener(() => this.Close(false));

            this.colorPicker.onColorChanged += (color) => this.Color = color;
        }

        //--------------------------------------------------------------------------------

        public bool Open(Color defaultColor) {

            if (!this.colorPickerIsOpen) {

                this.Color = defaultColor;
                this.colorPicker.color = this.Color;

                this.colorPickerIsOpen = true;
                this.canvasGroup.Show(true);

                return true;
            }

            return false;
        }

        //--------------------------------------------------------------------------------

        public void Close(bool submit) {

            if (this.colorPickerIsOpen) {

                this.colorPickerIsOpen = false;
                this.canvasGroup.Show(false);

                if (submit) {
                    this.OnColorPicked?.Invoke(this.Color);
                }

                this.OnClosed?.Invoke();
            }
        }

        //--------------------------------------------------------------------------------
    }
}