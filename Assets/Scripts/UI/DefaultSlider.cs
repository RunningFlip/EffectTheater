using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Slider))]
public class DefaultSlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI sliderValueLabel = null;


    //Components
    private Slider slider;

    //Event
    public event Action<float> OnValueChanged;


    private void Awake()
    {
        //Components
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(UpdateValueLabel);

        //Setup
        UpdateValueLabel(slider.value);
    }


    private void UpdateValueLabel(float _value)
    {
        OnValueChanged?.Invoke(_value);
        sliderValueLabel.text = ((int)(_value * 100.0f)).ToString();
    }


    public void SetValue(float _value)
    {
        slider.value = _value;
        sliderValueLabel.text = ((int)(_value * 100.0f)).ToString();
    }
}
