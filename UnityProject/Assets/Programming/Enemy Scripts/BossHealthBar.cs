using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider slider;

    public void SetRelativeHealth(float value)
    {
        slider.value = value;
    }

    public void SetMaxHealth(float max)
    {
        slider.maxValue = max;
        slider.value = max;
    }

    public void LoseHealth(float health)
    {
        slider.value -= health;
    }
}

