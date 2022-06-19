using TMPro;
using UnityEngine;
using UnityEngine.UI;

//--------------------------------------------------------------------------------

public class ColorPreview : MonoBehaviour {

    //--------------------------------------------------------------------------------
    // Fields
    //--------------------------------------------------------------------------------

    public Image previewImage;
    public TextMeshProUGUI previewText;
    public ColorPicker colorPicker;

    //--------------------------------------------------------------------------------
    // Methods
    //--------------------------------------------------------------------------------

    private void Awake() {

        this.UpdateColor(this.colorPicker.color);
        this.colorPicker.onColorChanged += OnColorChanged;
    }

    //--------------------------------------------------------------------------------

    private void OnDestroy() {

        if (this.colorPicker != null) {
            this.colorPicker.onColorChanged -= OnColorChanged;
        }
    }

    //--------------------------------------------------------------------------------

    public void OnColorChanged(Color color) {
        this.UpdateColor(color);
    }

    //--------------------------------------------------------------------------------

    private void UpdateColor(Color color) {

        this.previewImage.color = color;
        this.previewText.text = "#" + ColorUtility.ToHtmlStringRGB(color);
    }

    //--------------------------------------------------------------------------------
}