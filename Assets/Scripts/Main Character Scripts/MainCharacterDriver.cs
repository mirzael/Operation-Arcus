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
	const float POWER_INC = 5.0f;
	const int PROJECTILE_DISTANCE = 2;
	const int GREEN_PROJECTILE_DISTANCE = 50;
	public static float powerRed = 0.0f;
	public static float powerBlue = 0.0f;
	public static float powerYellow = 0.0f;

	//This is the current form the ship is using
	Form currentForm;

	//These are the special ship forms
	Form orangeForm;
	Form purpleForm;
	Form greenForm;

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
		forms.Add (new Form (shipSpeed,0.2f, projectiles[0], 50f, mats[0], ShipColor.BLUE));
		forms.Add (new Form (shipSpeed*0.5f,0.4f, projectiles[1], 50f, mats[1], ShipColor.RED));
		forms.Add (new Form (shipSpeed*2.0f,0.75f, projectiles[2], 50f, mats[2], ShipColor.YELLOW));

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
		//Switch to Next Form
		} else if (Input.GetKeyDown (KeyCode.E)) {
			switchForm (forms.Next ());
		//Switch to ORANGE Form
		} else if (Input.GetKeyDown (KeyCode.Alpha1)) {
			switchForm (orangeForm);
		//Switch to PURPLE FORM
		} else if (Input.GetKeyDown (KeyCode.Alpha2)) {
			switchForm (purpleForm);
		//Switch to GREEN FORM
		} else if (Input.GetKeyDown (KeyCode.Alpha3)) {
			switchForm(greenForm);
		}	
	}

	void OnCollisionEnter(Collision col){
		if (currentForm.projectile.tag != col.gameObject.tag || col.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
			Destroy (gameObject);
			Debug.Log("MISSION FAILED");
			Application.Quit();
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
			projectile = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.right * PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
			projectile.rigidbody.velocity = Vector3.right * currentForm.getSpeed();
			break;
		case ShipColor.RED:
			projectile = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.right * PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
			projectile.rigidbody.velocity = Vector3.right * currentForm.getSpeed();
			break;
		case ShipColor.YELLOW:
			int size = 3 + (int)powerYellow;
			GameObject[] blast = new GameObject[size];
			Debug.Log (currentForm.projectile.transform.rotation.x);
			for (int i = 0; i < (3 + powerYellow / 5); i++)
			{
				blast[i] = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.right * PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
				blast[i].rigidbody.velocity = transform.TransformDirection(Vector3.right * currentForm.getSpeed() + Vector3.up * Random.Range(-8f, 8f));
			} 
			break;
		case ShipColor.ORANGE:
			var oBlast = new GameObject[2];
			oBlast[0] = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.right * PROJECTILE_DISTANCE + Vector3.up, currentForm.projectile.transform.rotation);
			oBlast[0].gameObject.AddComponent<HomingMissile>();
			oBlast[1] = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.right * PROJECTILE_DISTANCE - Vector3.up, currentForm.projectile.transform.rotation);
			oBlast[1].gameObject.AddComponent<HomingMissile>();
			break;
		case ShipColor.PURPLE:
			projectile = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.right * PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
			var moveScript = projectile.AddComponent<MoveProjectile>();
			moveScript.projectileSpeed = currentForm.projectileSpeed;
			break;
		case ShipColor.GREEN:
			projectile = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.right * GREEN_PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
			break;
		}
	}

	void switchForm(Form form){
		currentForm = form;
		renderer.material = currentForm.material;
		currentCooldown = currentForm.getCooldown();
	}

}
