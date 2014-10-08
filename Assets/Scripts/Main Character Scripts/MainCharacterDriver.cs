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

	//This is the current form the ship is using
	Form currentForm;

	// Use this for initialization
	void Start () {
		if (projectiles.Count != mats.Count) {
			Debug.Log ("You must have an equal amount of projectiles and materials!");
			Application.Quit ();
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
		if (Input.GetKeyDown(KeyCode.Space) && currentCooldown <= 0) {
			currentCooldown = currentForm.cooldown;
			GameObject projectile = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.right * 2, currentForm.projectile.transform.rotation);
			projectile.rigidbody.velocity = Vector3.right * currentForm.projectileSpeed;
		//Switch to Previous Form
		}else if(Input.GetKeyDown(KeyCode.Q)){
			currentForm = forms.Previous();
			renderer.material = currentForm.material;
			currentCooldown = currentForm.cooldown;
		//Switch to Next Form
		}else if(Input.GetKeyDown(KeyCode.E)){
			currentForm = forms.Next();
			renderer.material = currentForm.material;
			currentCooldown = currentForm.cooldown;
		}
	}

	void OnCollisionEnter(Collision col){
		if (currentForm.projectile.tag != col.gameObject.tag) {
			Destroy (gameObject);
			Debug.Log("MISSION FAILED");
			Application.Quit();
		} 
		Destroy (col.gameObject);
	}
}
