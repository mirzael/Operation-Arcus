using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using MainCharacter;

public class BlindBar : MonoBehaviour
{
    public BlindColorBar redSlider;
    public BlindColorBar blueSlider;
    public BlindColorBar yellowSlider;

    public BlindColorBar healthSliderP1;
    public BlindColorBar healthSliderP2;
    public CharacterDriver player1;
    public CharacterDriver player2;
    public PrefixedText colorText;
    public PrefixedText colorTextP2;
    private bool isMultiplayer;

    public PrefixedText scoreText;

    protected void Start()
    {
        if(redSlider==null || blueSlider ==null || yellowSlider==null)
        {
            Debug.LogError("Make sure blind bar prefab is in scene and sliders are not null");
        }
        if (colorText==null || scoreText==null)
        {
            Debug.Log("Make sure curColor & scoreText are dragged into "+gameObject.name);
        }
        if(healthSliderP1==null || player1==null)
        {
            Debug.LogError("Make sure healthSliderP1 & Player1 are dragged into "+gameObject.name);
        }
        isMultiplayer = player2 != null;
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
        UpdateColorBars();
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        healthSliderP1.UpdatePercentage(player1.health);
        if(isMultiplayer)
        {
            healthSliderP2.UpdatePercentage(player2.health);
        }
    }

    public void UpdateCurrentColor()
    {
     /*   ShipColor color = player1.shipColor;
        colorText.UpdateText(color.ToString());*/
    }

    public void UpdateScore()
    {
        if(scoreText!=null)
        {
            scoreText.UpdateText(Hiscores.latestScore.ToString());
        }
    }

}
