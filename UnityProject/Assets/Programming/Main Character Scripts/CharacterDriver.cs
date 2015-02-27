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


    public void WinLevel()
    {
        gameOver = true;
        gameObject.GetComponent<EndAnimation>().PlayWinAnimation();
    }
}