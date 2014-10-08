using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
	public float cooldown;
	public GameObject projectile;	
	float currentCooldown;



	// Use this for initialization
	void Start () {
		currentCooldown = cooldown;
	}
	
	// Update is called once per frame
	void Update () {
		currentCooldown -= Time.deltaTime;

		if (currentCooldown <= 0) {
			var proj = (GameObject)Instantiate(projectile, transform.position + Vector3.left * 2, projectile.transform.rotation);
			proj.rigidbody.velocity = Vector3.left * 25;
			currentCooldown = cooldown;
		}
	}
}
