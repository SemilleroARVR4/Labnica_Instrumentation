using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PracticeParameter : MonoBehaviour
{
    public float actualValue, min, max;
    public Slider slider;
    public TMPro.TMP_InputField inputField;

    public void OnValidate() {
        ValidateValue();
    }

    public void ValidateValue()
    {
        slider.maxValue = max;
        slider.minValue = min;
        if(actualValue < min)
            actualValue = min;
        if(actualValue > max)
            actualValue = max;

        inputField.text = actualValue.ToString("F1");
        slider.value = actualValue;
    }
    
    private void Awake() {
        ChangeValue(actualValue);
    }

    public void ChangeValue(float value)
    {
        if(value > min)
            if(value<max)
                actualValue = value;
            else
                actualValue = max;
        else
            actualValue = min;
            
        slider.value = actualValue;
        inputField.text = actualValue.ToString("F1");
        FindObjectOfType<Practice1Preview>().OnValidate();
    }

    public void ChangedInput(string value)
    {
        ChangeValue(float.Parse(value));
    }
}
