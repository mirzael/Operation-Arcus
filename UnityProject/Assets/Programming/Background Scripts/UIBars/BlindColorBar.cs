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
    public CenterSlider centerSlider;

    protected void Start()
    {
        if(slider!=null)
        {
            slider.minValue = 0;
            slider.maxValue = 100;
        }
		if(centerSlider!=null)
		{
			centerSlider.minValue = 0;
            centerSlider.maxValue = 100;
		}
    }

    public void UpdatePercentage(float percent)
    {
        if(slider!=null)
        {
            slider.value = percent;
        }
        if(centerSlider!=null)
        {
            centerSlider.UpdatePercentage(percent);
        }
        if(text!=null)
        {
            text.text = prefix + percent.ToString() + "%";
        }
    }
}
