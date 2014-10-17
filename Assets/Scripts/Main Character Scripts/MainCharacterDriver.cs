using UnityEngine;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading;
using HelperClasses;
using MainCharacter;

public class MainCharacterDriver : MonoBehaviour {
	//Right now all forms have the same ship speed
	public float shipSpeed;
	//These are the projectiles that the ship will fire
	public List<GameObject> projectiles;
	//These are the different colors of the ship
	public List<Material> mats;
	float currentCooldown = 0;

	/*These are the Forms of the ship
	 *The forms comprise of
	 *	- Projectile Cooldown
	 *	- Projectile itself
	 *	- Projectile Speed
	 *	- Color of ship
	 *	- Speed of ship
	 */
	RotatingList<Form> forms;


	const float POWER_MAX = 100.0f;
	const float POWER_INC = 25.0f;
	const float TRANSFORM_AMOUNT = 50f;
	const int PROJECTILE_DISTANCE = 2;
	const int GREEN_PROJECTILE_DISTANCE = 50;
	public static float powerRed = 0.0f;
	public static float powerBlue = 0.0f;
	public static float powerYellow = 0.0f;

	//This is the current form the ship is using
	Form currentForm;

	//The base forms
	Form redForm;
	Form blueForm;
	Form yellowForm;

	//These are the special ship forms
	Form orangeForm;
	Form purpleForm;
	Form greenForm;

	//Used for returning to the form we were in before switching to secondary
	Form previousForm;

	// Use this for initialization
	void Start () {
		if (projectiles.Count != mats.Count) {
			Debug.Log ("You must have an equal amount of projectiles and materials!");
			Application.Quit();
		} else if (projectiles.Count == 0) {
			Debug.Log("You must have at least one projectile!");
			Application.Quit();
		}

		forms = new RotatingList<Form> ();
		//Based on what is given in the unity editor, create the different forms (see "Main Character" in scene)
		
		redForm = new Form (shipSpeed * 0.5f, 0.4f, projectiles[1], 50f, mats[1], ShipColor.RED);
		blueForm = new Form (shipSpeed, 0.2f, projectiles[0], 50f, mats[0], ShipColor.BLUE);
		yellowForm = new Form (shipSpeed * 2.0f, 0.75f, projectiles[2], 50f, mats[2], ShipColor.YELLOW);
		
		forms.Add (blueForm);
		forms.Add (redForm);
		forms.Add (yellowForm);

		//Create the special forms
		orangeForm = new Form (shipSpeed, 0.6f, projectiles [3], 100f, mats [3], ShipColor.ORANGE);
		purpleForm = new Form (shipSpeed * 0.75f, 0.2f, projectiles [4], 75f, mats [4], ShipColor.PURPLE);
		greenForm = new Form (shipSpeed * 1.5f, 0.5f, projectiles [5], 0f, mats [5], ShipColor.GREEN);

		//Set the current form to the first form
		currentForm = forms[0];
	}
	
	// Update is called once per frame
	void Update () {
		//Get where to move given user input
		float hspeed = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
		float vspeed = Input.GetAxisRaw ("Vertical") * Time.deltaTime;

		var toMoveVector = Vector3.right * hspeed * currentForm.formSpeed + Vector3.up * vspeed * currentForm.formSpeed;
		transform.Translate(toMoveVector);

		//change the cooldown of the main weapon, as one frame has passed
		currentCooldown -= Time.deltaTime;

		//FIRE!!!
		if (Input.GetKey(KeyCode.Space) && currentCooldown <= 0) {
			currentCooldown = currentForm.getCooldown();
			Fire();
		}
		//Switch to Previous Form
		if (Input.GetKeyDown (KeyCode.Q)) {
			switchForm (forms.Previous ());
			previousForm = currentForm;
		//Switch to Next Form
		} else if (Input.GetKeyDown (KeyCode.E)) {
			switchForm (forms.Next ());
			previousForm = currentForm;
		//Switch to ORANGE Form
		} else if (Input.GetKeyDown (KeyCode.Alpha1)) && powerRed >= TRANSFORM_AMOUNT && powerYellow >= TRANSFORM_AMOUNT) {
			powerRed -= TRANSFORM_AMOUNT; powerYellow -= TRANSFORM_AMOUNT;
			switchForm (orangeForm);
		//Switch to PURPLE FORM
		} else if (Input.GetKeyDown (KeyCode.Alpha2) && powerRed >= TRANSFORM_AMOUNT && powerBlue >= TRANSFORM_AMOUNT) {
			powerRed -= TRANSFORM_AMOUNT; powerBlue -= TRANSFORM_AMOUNT;
			switchForm (purpleForm);
		//Switch to GREEN FORM
		} else if (Input.GetKeyDown (KeyCode.Alpha3) && powerBlue >= TRANSFORM_AMOUNT && powerYellow >= TRANSFORM_AMOUNT) {
			powerBlue -= TRANSFORM_AMOUNT; powerYellow -= TRANSFORM_AMOUNT;
			switchForm(greenForm);
		}	
	}

	void OnCollisionEnter(Collision col){
		if (currentForm.projectile.tag != col.gameObject.tag || col.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
			if(currentForm.shipColor == ShipColor.PURPLE){
				redForm.resetSpeed();
				redForm.setSpeed(redForm.getSpeed() + powerRed);
				blueForm.resetCooldown();
				blueForm.setCooldown(blueForm.getCooldown() - 0.01f * powerBlue);
				switchForm(previousForm);
			}else if(currentForm.shipColor == ShipColor.ORANGE){
				switchForm(previousForm);
				redForm.resetSpeed();
				redForm.setSpeed(redForm.getSpeed() + powerRed);
			}else if(currentForm.shipColor == ShipColor.GREEN){
				switchForm(previousForm);
				blueForm.resetCooldown();
				blueForm.setCooldown(blueForm.getCooldown() - 0.01f * powerBlue);
			}else{
				Destroy (gameObject);
				Debug.Log("MISSION FAILED");
				Application.Quit();
			}
		} else {
			if (col.gameObject.tag == "Red") {
				if (powerRed < POWER_MAX) {
					powerRed += POWER_INC;
					currentForm.setSpeed(currentForm.getSpeed() + powerRed);
					if (powerRed > POWER_MAX) {
						powerRed = POWER_MAX;
					}
				}
				Debug.Log("Absorbed red bullet, Red Power at " + powerRed);
			} else if (col.gameObject.tag == "Blue") {
				if (powerBlue < POWER_MAX) {
					powerBlue += POWER_INC;
					currentForm.setCooldown(currentForm.getCooldown() - 0.01f * powerBlue);
					if (powerBlue > POWER_MAX) {
						powerBlue = POWER_MAX;
					}
				}
				Debug.Log("Absorbed blue bullet, Blue Power at " + powerBlue);
			} else if (col.gameObject.tag == "Yellow") {
				if (powerYellow < POWER_MAX) {
					powerYellow += POWER_INC;
					if (powerYellow > POWER_MAX) {
						powerYellow = POWER_MAX;
					}
				}
				Debug.Log("Absorbed yellow bullet, Yellow Power at " + powerYellow);
			}
		}
		Destroy (col.gameObject);
	}

	void Fire(){
		GameObject projectile;
		switch (currentForm.shipColor)
		{
		case ShipColor.BLUE:
			projectile = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
			projectile.rigidbody.velocity = Vector3.up * currentForm.getSpeed();
			break;
		case ShipColor.RED:
			projectile = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
			projectile.rigidbody.velocity = Vector3.up * currentForm.getSpeed();
			break;
		case ShipColor.YELLOW:
			int numProjectiles = 3 + (int)(powerYellow / POWER_INC);
			int projectileSpreadAngle = 30;
			int angleBetweenProjectiles = (projectileSpreadAngle / (numProjectiles - 1));
			float radToDeg =  Mathf.PI / 180;
			GameObject[] blast = new GameObject[numProjectiles];
			Debug.Log (currentForm.projectile.transform.rotation.x);
			for (int i = 0; i < numProjectiles; i++)
			{
				float trajectoryDegree = 90 + (projectileSpreadAngle / 2 - angleBetweenProjectiles * i);
				float currentAngularVelocity = Mathf.Cos(trajectoryDegree * radToDeg);
				blast[i] = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
				blast[i].rigidbody.velocity = transform.TransformDirection(Vector3.up * currentForm.getSpeed() + Vector3.right * currentAngularVelocity * currentForm.getSpeed());
			} 
			break;
		case ShipColor.ORANGE:
			var oBlast = new GameObject[2];
			oBlast[0] = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE + Vector3.up, currentForm.projectile.transform.rotation);
			oBlast[0].gameObject.AddComponent<HomingMissile>();
			oBlast[1] = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE - Vector3.up, currentForm.projectile.transform.rotation);
			oBlast[1].gameObject.AddComponent<HomingMissile>();
			break;
		case ShipColor.PURPLE:
			projectile = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
			var moveScript = projectile.AddComponent<MoveProjectile>();
			moveScript.projectileSpeed = currentForm.projectileSpeed;
			break;
		case ShipColor.GREEN:
			projectile = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.up * GREEN_PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
			break;
		}
	}

	void switchForm(Form form){
		currentForm = form;
		renderer.material = currentForm.material;
		currentCooldown = currentForm.getCooldown();
	}

}
