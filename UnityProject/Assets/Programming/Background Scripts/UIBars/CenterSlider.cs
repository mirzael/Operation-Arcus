using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class CenterSlider : MonoBehaviour
{
    public float minValue = 0;
    public float maxValue = 100;
    private float curValue;

    public Image background;
    public Image fill;

    protected void Start()
    {
        fill.transform.localScale = Vector3.zero;
    }

    public void UpdatePercentage(float percent)
    {
        fill.transform.localScale = background.transform.localScale * (percent / maxValue);
    }
}

