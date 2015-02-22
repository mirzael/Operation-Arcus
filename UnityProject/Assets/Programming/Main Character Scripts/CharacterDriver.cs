using UnityEngine;
using System.Collections;

public abstract class CharacterDriver : MonoBehaviour {
	protected PrimaryForm redForm;
	protected PrimaryForm blueForm;
	protected PrimaryForm yellowForm;
	public float health = 100;
	public const float TRANSFORM_AMOUNT = 100f;
	public bool gameOver = false;
	public UIDriver uiDriver;
	public abstract void PressGreen();
	public abstract void PressPurple();
	public abstract void PressOrange();

	public void Start(){
		redForm = GetComponent<RedForm> ();
		blueForm = GetComponent<BlueForm> ();
		yellowForm = GetComponent<YellowForm> ();

		var multi = GameObject.Find ("WaveSpawner").GetComponent<MultiplierScript> ();

		redForm.damage *= multi.playerDamageMultipler;
		blueForm.damage *= multi.playerDamageMultipler;
		yellowForm.damage *= multi.playerDamageMultipler;
	}


    public void WinLevel()
    {
        gameOver = true;
        gameObject.GetComponent<EndAnimation>().PlayWinAnimation();
    }
}