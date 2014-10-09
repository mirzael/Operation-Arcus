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
	const float POWER_INC = 1.0f;
	float powerRed = 0.0f;
	float powerBlue = 0.0f;
	float powerYellow = 0.0f;

	//This is the current form the ship is using
	Form currentForm;

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
		for(int i = 0; i < projectiles.Count; i++){
			forms.Add (new Form (shipSpeed,0.25f, projectiles[i], 50f, mats[i]));
		}

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
			currentCooldown = currentForm.cooldown;
			GameObject projectile = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.right * 2, currentForm.projectile.transform.rotation);
			projectile.rigidbody.velocity = Vector3.right * currentForm.projectileSpeed;
		}
		//Switch to Previous Form
		if (Input.GetKeyDown(KeyCode.Q)){
			currentForm = forms.Previous();
			renderer.material = currentForm.material;
			currentCooldown = currentForm.cooldown;
		//Switch to Next Form
		} else if(Input.GetKeyDown(KeyCode.E)){
			currentForm = forms.Next();
			renderer.material = currentForm.material;
			currentCooldown = currentForm.cooldown;
		}
	}

	void OnCollisionEnter(Collision col){
		if (currentForm.projectile.tag != col.gameObject.tag || col.gameObject.tag == "Ship") {
			Destroy (gameObject);
			Debug.Log("MISSION FAILED");
		} else {
			if (col.gameObject.tag == "Red") {
				if (powerRed < POWER_MAX) {
					powerRed += POWER_INC;
					if (powerRed > POWER_MAX) {
						powerRed = POWER_MAX;
					}
				}
				Debug.Log("Absorbed red bullet, Red Power at " + powerRed);
			} else if (col.gameObject.tag == "Blue") {
				if (powerBlue < POWER_MAX) {
					powerBlue += POWER_INC;
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
}
