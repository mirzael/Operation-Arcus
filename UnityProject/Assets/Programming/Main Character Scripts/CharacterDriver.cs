using UnityEngine;
using System.Collections;

public abstract class CharacterDriver : MonoBehaviour {
	public float health = 100;
	public const float TRANSFORM_AMOUNT = 100f;
	public bool gameOver = false;
	public UIDriver uiDriver;
	public abstract void PressGreen();
	public abstract void PressPurple();
	public abstract void PressOrange();

    public void WinLevel()
    {
        gameOver = true;
        gameObject.GetComponent<EndAnimation>().PlayWinAnimation();
    }
}