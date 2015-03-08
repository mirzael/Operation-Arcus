using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using MainCharacter;

public class BlindSecondaries : MonoBehaviour
{
    public SpriteRenderer greenBar;
    public SpriteRenderer purpleBar;
    public SpriteRenderer orangeBar;

    protected void Update()
    {
        UpdateBars();
    }

    public void UpdateBars()
    {
        greenBar.color = SetNewTransparency(greenBar.color,ColorPower.Instance.powerGreen);
        purpleBar.color = SetNewTransparency(purpleBar.color, ColorPower.Instance.powerPurple);
        orangeBar.color = SetNewTransparency(orangeBar.color, ColorPower.Instance.powerOrange);
    }

    private Color SetNewTransparency(Color color, float a)
    {
        //format a from the 0-100 to the 0-1f range
        a = a / 100f;
        //Debug.Log("a: " + a);
        Color newColor = color;
        newColor.a = a;
        //Debug.Log("color: " + color.ToString());
        return newColor;
    }
}

