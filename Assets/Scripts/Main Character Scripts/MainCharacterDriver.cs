using UnityEngine;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading;

public class MainCharacterDriver : MonoBehaviour {

	public float shipSpeed;
	public List<GameObject> projectiles;
	public List<Material> mats;
	float currentCooldown = 0;
	RotatingList<Form> forms;
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
		for(int i = 0; i < projectiles.Count; i++){
			forms.Add (new Form (shipSpeed,0.25f, projectiles[i], 50f, mats[i]));
		}

		currentForm = forms[0];
	}
	
	// Update is called once per frame
	void Update () {
		//Get space to move
		float hspeed = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
		float vspeed = Input.GetAxisRaw ("Vertical") * Time.deltaTime;

		var toMoveVector = Vector3.right * hspeed * currentForm.formSpeed + Vector3.up * vspeed * currentForm.formSpeed;
		transform.Translate(toMoveVector);

		//Check the cooldown of the main weapon
		currentCooldown -= Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.Space) && currentCooldown <= 0) {
			Debug.Log("FIRING");
			currentCooldown = currentForm.cooldown;
			GameObject projectile = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.right * 2, currentForm.projectile.transform.rotation);
			projectile.rigidbody.velocity = Vector3.right * currentForm.projectileSpeed;
		}else if(Input.GetKeyDown(KeyCode.Q)){
			currentForm = forms.Previous();
			renderer.material = currentForm.material;
			currentCooldown = currentForm.cooldown;
		}else if(Input.GetKeyDown(KeyCode.E)){
			currentForm = forms.Next();
			renderer.material = currentForm.material;
			currentCooldown = currentForm.cooldown;
		}
	}
}

public class RotatingList<T> : Collection<T> {
	int selectedIndex;
	 
	public RotatingList() : base(){
		selectedIndex = 0;
	}

	public T Next(){
		selectedIndex = ++selectedIndex % Count;
		return this[selectedIndex];
	}

	public T Previous(){
		selectedIndex = selectedIndex - 1 < 0 ? Count-1 : selectedIndex - 1;
		return this[selectedIndex];
	}


}


public class Form {
	public float cooldown;
	public GameObject projectile;
	public float projectileSpeed;
	public Material material;
	public float formSpeed;

	public Form(float formSpeed, float cooldown, GameObject projectile, float projectileSpeed, Material material){
		this.formSpeed = formSpeed;
		this.cooldown = cooldown;
		this.projectile = projectile;
		this.projectileSpeed = projectileSpeed;
		this.material = material;
	}

}
