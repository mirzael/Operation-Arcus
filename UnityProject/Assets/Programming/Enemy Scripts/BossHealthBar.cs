using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider slider;
    public Text text;

    protected void Start()
    {
        BackgroundUI.Instance.AddGameEndEvent(HideSelf);
    }

    public void HideSelf()
    {
        gameObject.SetActive(false);
    }

    public void DestroySelf()
    {
        BackgroundUI.Instance.RemoveGameEndEvent(HideSelf);
        GameObject.Destroy(gameObject);
    }

    public void SetBossName(string name)
    {
        text.text = name;
    }

    public void SetRelativeHealth(float value)
    {
        if(value < slider.minValue)
        {
            value = slider.minValue;
        }
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

