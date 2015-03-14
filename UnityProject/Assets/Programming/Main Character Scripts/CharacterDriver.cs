using UnityEngine;
using System.Collections;
using MainCharacter;
using InControl;

public abstract class CharacterDriver : MonoBehaviour {
	protected PrimaryForm redForm;
	protected PrimaryForm blueForm;
	protected PrimaryForm yellowForm;
	public float health = 100f;
	public const float TRANSFORM_AMOUNT = 100f;
	public bool gameOver = false;
	public UIDriver uiDriver;

    protected InputDevice device;

    protected float shipXMin;
    protected float shipXMax;
    protected float shipYMin;
    protected float shipYMax;

	public void Start(){
		redForm = GetComponent<RedForm> ();
		blueForm = GetComponent<BlueForm> ();
		yellowForm = GetComponent<YellowForm> ();

		var multi = GameObject.Find ("WaveSpawner").GetComponent<MultiplierScript> ();

		redForm.damage *= multi.playerDamageMultipler;
		blueForm.damage *= multi.playerDamageMultipler;
		yellowForm.damage *= multi.playerDamageMultipler;

        //Get the distance from the ship to the camera
        float z = Mathf.Abs(transform.position.z);
        var tmp = transform;
        while (tmp.parent != null)
        {
            tmp = tmp.parent;
            z += Mathf.Abs(tmp.position.z);
        }

        //Max an min points of the screen at the ship distance from the camera
        Vector3 wrldMax = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, z));
        Vector3 wrldMin = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, 0.0f, z));

        shipXMax = wrldMax.x;
        shipYMax = wrldMax.y;
        shipXMin = wrldMin.x;
        shipYMin = wrldMin.y;
	}

    protected virtual void Update()
    {
        //ZH Added checking for secondaries
        if (device.LeftBumper) {
            PressOrange();		
		} else if (device.LeftTrigger) {
            PressPurple();		
		} else if (device.RightBumper) {
            PressGreen();
        }
    }


    public void WinLevel()
    {
        gameOver = true;
        gameObject.GetComponent<EndAnimation>().PlayWinAnimation();
    }

    /// Handle taking damage and losing the game as shared functionality
    public float healthLossPerHit = 10f;
    public static bool lostGame = false;

    protected virtual void TakeDamage()
    {
        health -= healthLossPerHit;
        if (health <= 0)
        {
            GameOver();
        }
    }
    protected virtual void GameOver() 
    {
        if (gameOver)
            return;
        Destroy(gameObject);
        Debug.Log("MISSION FAILED");
        gameOver = true;
        lostGame = true;
    }

    //ZH 3-14 Refactored to separate pressing & using since the code for pressing is the same in all drivers
    public virtual void PressGreen()
    {
        Debug.Log("Pressing Green D!!!!!");
        if (ColorPower.Instance.powerBlue >= TRANSFORM_AMOUNT && ColorPower.Instance.powerYellow >= TRANSFORM_AMOUNT)
        {
            //ZH MainCharacterDriver does more in decreasing power
            ColorPower.Instance.powerBlue -= CharacterDriver.TRANSFORM_AMOUNT;
            ColorPower.Instance.powerYellow -= CharacterDriver.TRANSFORM_AMOUNT;
            UseGreen();
            UIEvents.Instance.UseSecondary(UIEvents.Instance.Green);
        }
    }
    public virtual void PressPurple()
    {
        Debug.Log("Presin purple!!!!!");
        if (ColorPower.Instance.powerBlue >= TRANSFORM_AMOUNT && ColorPower.Instance.powerRed >= TRANSFORM_AMOUNT)
        {
            //ZH MainCharacterDriver does more in decreasing power
            ColorPower.Instance.powerBlue -= CharacterDriver.TRANSFORM_AMOUNT;
            ColorPower.Instance.powerRed -= CharacterDriver.TRANSFORM_AMOUNT;
            UsePurple();
            UIEvents.Instance.UseSecondary(UIEvents.Instance.Purple);
        }
    }
    public virtual void PressOrange()
    {
        Debug.Log("Presisng pornage!!!!!");
        if (ColorPower.Instance.powerBlue >= TRANSFORM_AMOUNT && ColorPower.Instance.powerYellow >= TRANSFORM_AMOUNT)
        {
            //ZH MainCharacterDriver does more in decreasing power
            ColorPower.Instance.powerRed -= CharacterDriver.TRANSFORM_AMOUNT;
            ColorPower.Instance.powerYellow -= CharacterDriver.TRANSFORM_AMOUNT;
            UseOrange();
            UIEvents.Instance.UseSecondary(UIEvents.Instance.Orange);
        }
    }
    public abstract void UseGreen();
    public abstract void UsePurple();
    public abstract void UseOrange();

}