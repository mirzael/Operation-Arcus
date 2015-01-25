﻿using UnityEngine;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading;
using MainCharacter;

public class MainCharacterDriver : MonoBehaviour {
	GameObject[] colorPieces;
	float currentCooldown = 0;
	int rainbowCooldown = 2;

	public float invulnTime;
	public float invulnCounter = 0;
	public float prevAlpha = 1;
	public int health;
	public bool gameOver = false;
	bool pause = false;


	/*These are the Forms of the ship
	 *The forms comprise of
	 *	- Projectile Cooldown
	 *	- Projectile itself
	 *	- Projectile Speed
	 *	- Color of ship
	 *	- Speed of ship
	 */
	public RotatingList<Form> forms = new RotatingList<Form>();

	//Arcus Animator
	Animator anim;

	public const float TRANSFORM_AMOUNT = 50f;
	const float ALPHA_PER_SEC = 0.1f;

	//This is the current form the ship is using
	private Form currentForm;

	//Used for returning to the form we were in before switching to secondary
	private Form previousForm;

	private RedForm redForm;
	private BlueForm blueForm;
	private YellowForm yellowForm;
	private GreenForm greenForm;
	private OrangeForm orangeForm;
	private PurpleForm purpleForm;
	private RainbowForm rainbowForm;

	public static float powerRed = 0.0f;
	public static float powerBlue = 0.0f;
	public static float powerYellow = 0.0f;

	public float shipXMin = -10.25f;
	public float shipXMax = 10.25f;
	public float shipYMin = -12.0f;
	public float shipYMax = 19.0f;

	public UIDriver uiDriver;

	public static string arcusName = "";

	//Sounds
	public AudioClip bulletSound;
	public AudioClip bumpSound;
	public AudioClip absorbSound;

	//Pause screenshot
	public Texture pauseButton;

	// Use this for initialization
	void Start () {
		Application.targetFrameRate = 60;
		arcusName = gameObject.name;
		
		anim = GetComponent<Animator> ();
		colorPieces = GameObject.FindGameObjectsWithTag ("ArcusColor");
		
		redForm = GetComponent<RedForm>();
		blueForm = GetComponent<BlueForm>();
		yellowForm = GetComponent<YellowForm>();
		greenForm = GetComponent<GreenForm>();
		orangeForm = GetComponent<OrangeForm>();
		purpleForm = GetComponent<PurpleForm>();
		rainbowForm = GetComponent<RainbowForm>();
		
		powerRed = redForm.power;
		powerBlue = blueForm.power;
		powerYellow = yellowForm.power;
		
		forms.Add (redForm);
		forms.Add (blueForm);
		forms.Add (yellowForm);
		previousForm = previousForm ?? forms[0];
		currentForm = currentForm ?? forms [0];
		switchForm (currentForm);
		
		uiDriver = GameObject.Find("UI Camera").GetComponent<UIDriver>();

		if (currentForm.shipColor == ShipColor.BLUE) {
			uiDriver.RotateToBlue();
		} else if (currentForm.shipColor == ShipColor.RED) {
			uiDriver.RotateToRed();
		} else if (currentForm.shipColor == ShipColor.YELLOW) {
			uiDriver.RotateToYellow();
		}
		uiDriver.UpdateBars();
		
		pauseButton = (Texture)Resources.Load("Textures/PauseButton", typeof(Texture));
	}

	void OnGUI(){
		if (pause) {
			GUI.DrawTexture(new Rect(100,200,250,300), pauseButton, ScaleMode.StretchToFill);
		}
	}
	
	// Update is called once per frame
	public void Update () {
		if(Input.GetKeyDown(KeyCode.F)){
			pause = !pause;
		}
		if (pause) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
			if (gameOver)
					return;
			invulnCounter -= Time.deltaTime;

			if (invulnCounter > 0) {
				foreach (Renderer obj in GetComponentsInChildren<Renderer>()) {
					obj.enabled = ! obj.enabled;
				}
			} else {
				foreach (Renderer obj in GetComponentsInChildren<Renderer>()) {
					obj.enabled = true;
				}
			}

			//Get where to move given user input
			float hspeed = Input.GetAxisRaw ("Horizontal") * -(Time.deltaTime);
			float vspeed = Input.GetAxisRaw ("Vertical") * Time.deltaTime;

			var toMoveVector = Vector3.right * hspeed * currentForm.formSpeed + Vector3.back * vspeed * currentForm.formSpeed;
			Vector3 orig = transform.position;
			transform.Translate (toMoveVector);

			float posX = transform.position.x - transform.parent.position.x;
			float posY = transform.position.y - transform.parent.position.y;
			if (posX > shipXMax || posX < shipXMin || posY > shipYMax || posY < shipYMin) {
				transform.position = orig;
			}

			//change the cooldown of the main weapon, as one frame has passed
			currentCooldown -= Time.deltaTime;

			//FIRE!!!
			if (Input.GetKey (KeyCode.Space) && currentCooldown <= 0) {
				currentCooldown = currentForm.getCooldown ();
				Fire ();
			}
			if (currentForm.shipColor == ShipColor.RAINBOW) {
				if (rainbowCooldown % 3 == 0) {
					//Color newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
					for (int i = 0; i < colorPieces.Length; i++) {
						Color newColor = new Color (Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), Random.Range (0.0f, 1.0f), 1.0f);
						colorPieces [i].renderer.material.color = newColor;
					}
				}
				rainbowCooldown = rainbowCooldown - 1;
				if (rainbowCooldown <= 0) {
					rainbowCooldown = 10;
					setRedPower(powerRed - 1);
					setBluePower(powerBlue - 1);
					setYellowPower(powerYellow - 1);
					uiDriver.UpdateBars ();
				}
				if (powerBlue <= 0) {
					previousForm = previousForm.shipColor == ShipColor.RAINBOW ? redForm : previousForm;
					switchForm (previousForm);
					blueForm.resetCooldown ();
					redForm.resetSpeed ();
				}
				return;
			}
			//Switch to Yellow Form
			if (Input.GetButtonDown ("Yellow")) {
				switchForm (yellowForm);
				uiDriver.RotateToYellow();
			//Switch to Blue Form
			} else if (Input.GetButtonDown ("Blue")) {
				switchForm (blueForm);
				uiDriver.RotateToBlue();
			//Switch to Red Form
			} else if (Input.GetButtonDown("Red")) {
				switchForm(redForm);
				uiDriver.RotateToRed();
			//Switch to ORANGE Form
			} else if (Input.GetButtonDown ("Orange") && powerRed >= TRANSFORM_AMOUNT && powerYellow >= TRANSFORM_AMOUNT) {
				setRedPower(powerRed - TRANSFORM_AMOUNT);
				setYellowPower(powerYellow - TRANSFORM_AMOUNT);
				switchForm (orangeForm);
				uiDriver.UpdateBars ();
			//Switch to PURPLE FORM
			} else if (Input.GetButtonDown ("Purple") && powerRed >= TRANSFORM_AMOUNT && powerBlue >= TRANSFORM_AMOUNT) {
				setRedPower(powerRed - TRANSFORM_AMOUNT);
				setBluePower(powerBlue - TRANSFORM_AMOUNT);
				switchForm (purpleForm);
				uiDriver.UpdateBars ();
			//Switch to GREEN FORM
			} else if (Input.GetButtonDown ("Green") && powerBlue >= TRANSFORM_AMOUNT && powerYellow >= TRANSFORM_AMOUNT) {
				setBluePower(powerBlue - TRANSFORM_AMOUNT);
				setYellowPower(powerYellow - TRANSFORM_AMOUNT);
				switchForm (greenForm);
				uiDriver.UpdateBars ();
			} else if (Input.GetKeyDown (KeyCode.PageDown)) {
				setRedPower(100);
				setBluePower(100);
				setYellowPower(100);
				forms [0].setSpeed (forms [0].getSpeed () + powerRed / 30);
				forms [1].setCooldown (forms [1].getCooldown () - 0.15f);
				uiDriver.UpdateBars ();
			}
		}
	}

	public void OnCollisionEnter(Collision col) {
		
		// Form.TakeHit() returns true if the bullet cannot be absorbed, else it returns false
		if (col.gameObject.layer == LayerMask.NameToLayer("Enemy") || currentForm.TakeHit(col)) {
			
			// Only handle hit if not invulnerable
			if (invulnCounter <= 0) {
				
				// Set invulnerability
				invulnCounter = currentForm.shipColor == ShipColor.RAINBOW ? 0 : invulnTime;
				audio.PlayOneShot(bumpSound);
				
				// If in a secondary form, switch back to the previous form
				if (currentForm.shipColor == ShipColor.PURPLE || currentForm.shipColor == ShipColor.ORANGE || currentForm.shipColor == ShipColor.GREEN) {
					switchForm(previousForm);
				
				// Only take damage if not in rainbow mode
				} else if (currentForm.shipColor != ShipColor.RAINBOW) {
					health -= 10;
					if (health < 0) {
						if (gameOver) return;
						Destroy (gameObject);
						Debug.Log("MISSION FAILED");
						uiDriver.ShowLoseScreen();
						gameOver = true;
					}
				}
			}
		
		// Absorbed the bullet
		} else {
			powerBlue = blueForm.power;
			powerRed = redForm.power;
			powerYellow = yellowForm.power;
			uiDriver.UpdateBars();
			audio.PlayOneShot(absorbSound);
		}
		
		Destroy (col.gameObject);
		if (yellowForm.atMaxPower() && blueForm.atMaxPower() && redForm.atMaxPower()) {
			previousForm = currentForm;
			switchForm(rainbowForm);
		}
	}
	
	void Fire(){
		audio.PlayOneShot(bulletSound);
		currentForm.Fire();
	}
	
	void switchForm(Form form){
		currentForm = form;
		for (int i = 0; i < colorPieces.Length; i++) 
		{
			colorPieces[i].renderer.material = currentForm.material;
		}
		currentCooldown = currentForm.getCooldown();
		anim.SetInteger ("TransformVar", currentForm.animationNum);
	}

	public void ResetForm(){
		switchForm (redForm);
	}

	private void setRedPower(float p) {
		redForm.power = p;
		powerRed = p;
	}

	private void setBluePower(float p) {
		blueForm.power = p;
		powerBlue = p;
	}

	private void setYellowPower(float p) {
		yellowForm.power = p;
		powerYellow = p;
	}
}
