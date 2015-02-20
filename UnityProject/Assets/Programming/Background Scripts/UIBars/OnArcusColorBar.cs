using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class OnArcusColorBar : MonoBehaviour
{
    public Slider slider;

    private Image sliderImage;
    public Image borderImage;

    protected void Awake()
    {
        if(slider==null)
        {
            slider = gameObject.GetComponent<Slider>();
        }
        slider.minValue = 0;
        slider.maxValue = 100;
        sliderImage = slider.fillRect.GetComponent<Image>();
    }

    public void UpdatePercentage(float percent)
    {
        slider.value = percent;
    }

    public Color currentColor
    {
        get
        {
            return sliderImage.color;
        }
        set
        {
            ChangeColor(value);
        }
    }

    public void ChangeColor(Color color)
    {
        sliderImage.color = color;
        if (borderImage != null)
        {
            borderImage.color = color;
        }
    }
}
