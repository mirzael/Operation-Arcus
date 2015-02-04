using UnityEngine;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading;
using MainCharacter;
using InControl;

public class MainCharacterDriver : MonoBehaviour {
	GameObject[] colorPieces;
	float currentCooldown = 0;
	int rainbowCooldown = 2;

	public float invulnTime;
	public float invulnCounter = 0;
	public float prevAlpha = 1;
	public static int health = 100;
	public bool gameOver = false;
	bool pause = false;
	
	public string inputRed = "Red";
	public string inputBlue = "Blue";
	public string inputYellow = "Yellow";
	public string inputGreen = "Green";
	public string inputOrange = "Orange";
	public string inputPurple = "Purple";
	
	public string inputHorizontal = "Horizontal";
	public string inputVertical = "Vertical";
	
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

	public const float TRANSFORM_AMOUNT = 100f;
	private const float ALPHA_PER_SEC = 0.1f;
	static bool lostGame;

	//This is the current form the ship is using
	static Form currentForm;

	//Used for returning to the form we were in before switching to secondary
	static Form previousForm;

	private RedForm redForm;
	private BlueForm blueForm;
	private YellowForm yellowForm;
	private GreenForm greenForm;
	private OrangeForm orangeForm;
	private PurpleForm purpleForm;
	private RainbowForm rainbowForm;
	private bool isInSecondary = false;

	private float shipXMin;
	private float shipXMax;
	private float shipYMin;
	private float shipYMax;

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
		
		forms.Add (redForm);
		forms.Add (blueForm);
		forms.Add (yellowForm);
		
		if (previousForm == null) {
			previousForm = forms[0];
		} else {
			if (previousForm.shipColor == ShipColor.RED) {
				previousForm = redForm;
			} else if (previousForm.shipColor == ShipColor.BLUE) {
				previousForm = blueForm;
			} else if (previousForm.shipColor == ShipColor.YELLOW) {
				previousForm = yellowForm;
			} else {
				previousForm = forms[0];
			}
		}
		if (currentForm == null) {
			currentForm = forms[0];
		} else {
			if (currentForm.shipColor == ShipColor.RED) {
				currentForm = redForm;
			} else if (currentForm.shipColor == ShipColor.BLUE) {
				currentForm = blueForm;
			} else if (currentForm.shipColor == ShipColor.YELLOW) {
				currentForm = yellowForm;
			} else if (currentForm.shipColor == ShipColor.GREEN) {
				currentForm = greenForm;
			} else if (currentForm.shipColor == ShipColor.ORANGE) {
				currentForm = orangeForm;
			} else if (currentForm.shipColor == ShipColor.PURPLE) {
				currentForm = purpleForm;
			} else if (currentForm.shipColor == ShipColor.RAINBOW) {
				currentForm = rainbowForm;
			} else {
				currentForm = forms[0];
			}
		}
		if (lostGame) {
			health = 100;
			ColorPower.Instance.powerRed = 0;
			ColorPower.Instance.powerBlue = 0;
			ColorPower.Instance.powerYellow = 0;
		} else {
			redForm.setPower(ColorPower.Instance.powerRed);
			blueForm.setPower(ColorPower.Instance.powerBlue);
			yellowForm.setPower(ColorPower.Instance.powerYellow);
		}
		switchForm (currentForm);
		
		uiDriver = Camera.main.GetComponent<UIDriver>();

		if (currentForm.shipColor == ShipColor.BLUE) {
			uiDriver.RotateToBlue();
		} else if (currentForm.shipColor == ShipColor.RED) {
			uiDriver.RotateToRed();
		} else if (currentForm.shipColor == ShipColor.YELLOW) {
			uiDriver.RotateToYellow();
		}
		uiDriver.UpdateBars();
		
		pauseButton = (Texture)Resources.Load("Textures/PauseButton", typeof(Texture));
		lostGame = false;

		//Get the distance from the ship to the camera
		float z = Mathf.Abs(transform.position.z);
		var tmp = transform;
		while (tmp.parent != null) {
			tmp = tmp.parent;
			z += Mathf.Abs(tmp.position.z);
		}

		//Max an min points of the screen at the ship distance from the camera
		Vector3 wrldMax = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, z));
		Vector3 wrldMin = Camera.main.ScreenToWorldPoint (new Vector3 (0.0f, 0.0f, z));
		Debug.Log (wrldMin);
		Debug.Log (wrldMax);

		shipXMax = wrldMax.x;
		shipYMax = wrldMax.y;
		shipXMin = wrldMin.x;
		shipYMin = wrldMin.y;
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
			
			if (pause) {
				Time.timeScale = 0;
				return;
			} else {
				Time.timeScale = 1;
			}
		}
		if (gameOver) {
			return;
		}
		
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

        //Get the most recent input device from incontrol
        //Keyboard controls can be represented as an InputDevice using a CustomController
        var inputDevice = InputManager.ActiveDevice;

		//Get where to move given user input
        PressMove(Input.GetAxisRaw(inputHorizontal),Input.GetAxisRaw(inputVertical));
        PressMove(inputDevice.Direction.X, inputDevice.Direction.Y);

		//change the cooldown of the main weapon, as one frame has passed
		currentCooldown -= Time.deltaTime;

		//FIRE!!!
		if (Input.GetKey (KeyCode.Space) || inputDevice.Action1) {
            PressFire();
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
				setRedPower(ColorPower.Instance.powerRed - 1);
				setBluePower(ColorPower.Instance.powerRed - 1);
				setYellowPower(ColorPower.Instance.powerRed - 1);
				uiDriver.UpdateBars ();
			}
			if (ColorPower.Instance.powerBlue <= 0) {
				previousForm = previousForm.shipColor == ShipColor.RAINBOW ? redForm : previousForm;
				switchForm (previousForm);
				blueForm.resetCooldown ();
				redForm.resetSpeed ();
			}
			return;
		} else if (isInSecondary) {
			if (((SecondaryForm)currentForm).isDeactivated()) {
				switchForm(previousForm);
				isInSecondary = false;
			}
		}

        //Take input
        //For multiplayer these will need to be exclusive, but for now both can move ship
        if (Input.GetButtonDown(inputYellow) || inputDevice.Action4)
        {
            PressYellow();		
		} else if (Input.GetButtonDown(inputBlue) || inputDevice.Action3) {
            PressBlue();		
		} else if (Input.GetButtonDown(inputRed) || inputDevice.Action2) {
            PressRed();		
		} else if (Input.GetButtonDown(inputOrange) || inputDevice.LeftBumper) {
            PressOrange();		
		} else if (Input.GetButtonDown(inputPurple) || inputDevice.LeftTrigger) {
            PressPurple();		
		} else if (Input.GetButtonDown(inputGreen) || inputDevice.RightBumper) {
            PressGreen();
        }
        else if (Input.GetKeyDown(KeyCode.PageDown))
        {
			setRedPower(100);
			setBluePower(100);
			setYellowPower(100);
			forms [0].setSpeed (forms [0].getSpeed () + ColorPower.Instance.powerRed / 30);
			forms [1].setCooldown (forms [1].getCooldown () - 0.15f);
			uiDriver.UpdateBars();
		}
	}

    public void PressMove(float horizontal, float vertical)
    {
        float hspeed = horizontal * -(Time.deltaTime);
        float vspeed = vertical * Time.deltaTime;

        var toMoveVector = Vector3.right * hspeed * currentForm.formSpeed + Vector3.back * vspeed * currentForm.formSpeed;
        Vector3 orig = transform.position;
        transform.Translate(toMoveVector);

        float posX = transform.position.x - transform.parent.position.x;
        float posY = transform.position.y - transform.parent.position.y;

        if (posX > shipXMax || posX < shipXMin || posY > shipYMax || posY < shipYMin)
        {
            transform.position = orig;
        }
    }

    public void PressFire()
    {
        //FIRE!!!
        if (currentCooldown <= 0)
        {
            currentCooldown = currentForm.getCooldown();
            Fire();
        }
    }

    //Switch to Yellow Form
    public void PressYellow()
    {
        if (!isInSecondary)
        {
            switchForm(yellowForm);
            uiDriver.RotateToYellow();
        }
    }

    //Switch to Red Form
    public void PressRed()
    {
        if (!isInSecondary)
        {
            switchForm(redForm);
            uiDriver.RotateToRed();
        }
    }

    //Switch to Blue Form
    public void PressBlue()
    {
        if (!isInSecondary)
        {
            switchForm(blueForm);
            uiDriver.RotateToBlue();
        }
    }

    //Switch to PURPLE FORM
    public void PressPurple()
    {
        if (ColorPower.Instance.powerRed >= TRANSFORM_AMOUNT && ColorPower.Instance.powerBlue >= TRANSFORM_AMOUNT)
        {
            setRedPower(ColorPower.Instance.powerRed - TRANSFORM_AMOUNT);
            setBluePower(ColorPower.Instance.powerBlue - TRANSFORM_AMOUNT);
            switchForm(purpleForm);
            purpleForm.Activate();
            uiDriver.UpdateBars();
            isInSecondary = true;
        }
    }

    //Switch to GREEN FORM
    public void PressGreen()
    {
        if (ColorPower.Instance.powerBlue >= TRANSFORM_AMOUNT && ColorPower.Instance.powerYellow >= TRANSFORM_AMOUNT)
        {
            setBluePower(ColorPower.Instance.powerBlue - TRANSFORM_AMOUNT);
            setYellowPower(ColorPower.Instance.powerYellow - TRANSFORM_AMOUNT);
            switchForm(greenForm);
            greenForm.Activate();
            uiDriver.UpdateBars();
            isInSecondary = true;
        }
    }

    //Switch to ORANGE Form
    public void PressOrange()
    {
        if (Input.GetButtonDown(inputOrange) && ColorPower.Instance.powerRed >= TRANSFORM_AMOUNT && ColorPower.Instance.powerYellow >= TRANSFORM_AMOUNT)
        {
            setRedPower(ColorPower.Instance.powerRed - TRANSFORM_AMOUNT);
            setYellowPower(ColorPower.Instance.powerYellow - TRANSFORM_AMOUNT);
            switchForm(orangeForm);
            orangeForm.Activate();
            uiDriver.UpdateBars();
            isInSecondary = true;
        }
    }

	public void OnCollisionEnter(Collision col) {
		
		// Form.TakeHit() returns true if the bullet cannot be absorbed, else it returns false
		if (/*col.gameObject.layer == LayerMask.NameToLayer("Enemy") ||*/ currentForm.TakeHit(col)) {
			
			// Only handle hit if not invulnerable
			if (invulnCounter <= 0) {
				
				// Set invulnerability
				invulnCounter = currentForm.shipColor == ShipColor.RAINBOW ? 0 : invulnTime;
				audio.PlayOneShot(bumpSound);
				
				// If in a secondary form, switch back to the previous form
				/*if (currentForm.shipColor == ShipColor.PURPLE || currentForm.shipColor == ShipColor.ORANGE || currentForm.shipColor == ShipColor.GREEN) {
					switchForm(previousForm);
				
				// Only take damage if not in rainbow mode
				} else */
				if (currentForm.shipColor != ShipColor.RAINBOW) {
					health -= 10;
					if (health < 0) {
						if (gameOver) return;
						Destroy (gameObject);
						Debug.Log("MISSION FAILED");
						uiDriver.ShowLoseScreen();
						gameOver = true;
						lostGame = true;
					}
				}
			}
		
		// Absorbed the bullet
		} else {
			ColorPower.Instance.powerBlue = blueForm.power;
			ColorPower.Instance.powerRed = redForm.power;
			ColorPower.Instance.powerYellow = yellowForm.power;
			uiDriver.UpdateBars();
			audio.PlayOneShot(absorbSound);
		}
		
		if (yellowForm.atMaxPower() && blueForm.atMaxPower() && redForm.atMaxPower()) {
			previousForm = currentForm;
			switchForm(rainbowForm);
		}
	}
	
	void Fire() {
		if (currentForm is PrimaryForm) {
			audio.PlayOneShot(bulletSound);
			currentForm.Fire();
		}
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
		ColorPower.Instance.powerRed = p;
	}

	private void setBluePower(float p) {
		blueForm.power = p;
		ColorPower.Instance.powerBlue = p;
	}

	private void setYellowPower(float p) {
		yellowForm.power = p;
		ColorPower.Instance.powerYellow = p;
	}
}
