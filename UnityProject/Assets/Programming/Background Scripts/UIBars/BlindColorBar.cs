using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class BlindColorBar : MonoBehaviour
{
    public Text text;
    public Slider slider;
    public string prefix;

    protected void Start()
    {
        slider.minValue = 0;
        slider.maxValue = 100;
    }

    public void UpdatePercentage(float percent)
    {
        slider.value = percent;
        if(text!=null)
        {
            text.text = prefix + percent.ToString() + "%";
        }
    }
}
