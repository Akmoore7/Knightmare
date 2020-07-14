using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour
{
    //private float health = 100;
    public Slider slider;
    public Image fill;
    public Gradient gradient;

    public void SetValue(float value) {
        slider.value = value;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void SetMax(float newMax)
    {
        slider.maxValue = newMax;
        slider.value = newMax;

        //1f is the right most color, max color
        fill.color = gradient.Evaluate(1f);
    }
}
