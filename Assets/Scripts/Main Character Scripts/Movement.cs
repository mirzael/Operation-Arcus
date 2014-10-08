using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

public class Movement : MonoBehaviour {

	public Transform ship;
	public float xFactor;
	public float yFactor;
	public List<GameObject> projectiles;
	float currentCooldown = 0;
	List<Weapon> weapons;
	Weapon currentWeapon;
	int currentWeaponIndex = 0;

	// Use this for initialization
	void Start () {
		weapons = new List<Weapon> ();
		foreach(GameObject proj in projectiles){
			weapons.Add (new Weapon (0.25f, proj, 50f));
		}

		currentWeapon = weapons [currentWeaponIndex];
	}
	
	// Update is called once per frame
	void Update () {
		//Get space to move
		float hspeed = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
		float vspeed = Input.GetAxisRaw ("Vertical") * Time.deltaTime;

		var toMoveVector = Vector3.right * hspeed * xFactor + Vector3.up * vspeed * yFactor;
		ship.Translate(toMoveVector);

		//Check the cooldown of the main weapon
		currentCooldown -= Time.deltaTime;

		if (Input.GetKeyDown(KeyCode.Space) && currentCooldown <= 0) {
			Debug.Log("FIRING");
			currentCooldown = currentWeapon.cooldown;
			GameObject projectile = (GameObject)Instantiate(currentWeapon.projectile, transform.position + Vector3.right * 2, currentWeapon.projectile.transform.rotation);
			projectile.rigidbody.velocity = Vector3.right * currentWeapon.speed;
		}else if(Input.GetKeyDown(KeyCode.Q)){
			currentWeaponIndex = currentWeaponIndex-1 < 0 ? 0 : currentWeaponIndex-1;
			currentWeapon = weapons[currentWeaponIndex];
		}else if(Input.GetKeyDown(KeyCode.E)){
			currentWeaponIndex = currentWeaponIndex+1 >= weapons.Count ? weapons.Count-1 : currentWeaponIndex+1;
			currentWeapon = weapons[currentWeaponIndex];
		}
	}
}

public class Weapon {
	public float cooldown;
	public GameObject projectile;
	public float speed;

	public Weapon(float cooldown,GameObject projectile, float speed){
		this.cooldown = cooldown;
		this.projectile = projectile;
		this.speed = speed;
	}

}
