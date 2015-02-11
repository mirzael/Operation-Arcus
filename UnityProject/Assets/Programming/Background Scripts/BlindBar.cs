using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using MainCharacter;

public class BlindBar : Singleton<BlindBar>
{
    public BlindColorBar redSlider;
    public BlindColorBar blueSlider;
    public BlindColorBar yellowSlider;

    public BlindColorBar healthSlider;

    public PrefixedText colorText;
    public PrefixedText scoreText;

    protected void Start()
    {
        if(redSlider==null || blueSlider ==null || yellowSlider==null)
        {
            Debug.LogError("Make sure blind bar prefab is in scene and sliders are not null");
        }
        if (colorText==null || scoreText==null)
        {
            Debug.LogError("Make sure curColor & scoreText are dragged into blind bar");
        }
        UpdateColorBars();
    }

    public void UpdateColorBars()
    {
        redSlider.UpdatePercentage(ColorPower.Instance.powerRed);
        blueSlider.UpdatePercentage(ColorPower.Instance.powerBlue);
        yellowSlider.UpdatePercentage(ColorPower.Instance.powerYellow);
    }

    protected void Update()
    {
        UpdateCurrentColor();
        UpdateScore();
    }

    public void UpdateCurrentColor()
    {
        ShipColor color = MainCharacterDriver.currentForm.shipColor;
        colorText.UpdateText(color.ToString());
    }

    public void UpdateScore()
    {
        scoreText.UpdateText(Hiscores.latestScore.ToString());
    }

}
