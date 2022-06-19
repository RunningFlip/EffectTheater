using System;
using UnityEngine;
using UnityEngine.UI;

//--------------------------------------------------------------------------------

namespace Theater.UI {

    public class ColorPickerButton : Button {

        //--------------------------------------------------------------------------------
        // Properties
        //--------------------------------------------------------------------------------

        private static ColorPickerPanel colorPickerPanel;
        private static ColorPickerPanel ColorPickerPanel {

            get {

                if (ColorPickerButton.colorPickerPanel == null) {
                    ColorPickerButton.colorPickerPanel = FindObjectOfType<ColorPickerPanel>();
                }
                
                return ColorPickerButton.colorPickerPanel;
            }
        }

        //--------------------------------------------------------------------------------
        // Fields
        //--------------------------------------------------------------------------------

        public Action<Color> OnColorPicked;

        //--------------------------------------------------------------------------------
        // Methods
        //--------------------------------------------------------------------------------

        private void Awake() {
            this.onClick.AddListener(this.Click);
        }

        private void Click() {

            if (ColorPickerButton.ColorPickerPanel.Open(this.image.color)) {

                ColorPickerButton.ColorPickerPanel.OnColorPicked += this.ReturnColor;
                ColorPickerButton.ColorPickerPanel.OnClosed += this.ResetActions;
            }
        }

        //--------------------------------------------------------------------------------

        private void ReturnColor(Color color) {

            color.a = 1f;

            this.image.color = color;
            this.OnColorPicked?.Invoke(color);       
        }

        //--------------------------------------------------------------------------------

        private void ResetActions() {

            ColorPickerButton.ColorPickerPanel.OnClosed -= this.ResetActions;
            ColorPickerButton.ColorPickerPanel.OnColorPicked -= this.ReturnColor;
        }

        //--------------------------------------------------------------------------------
    }
}