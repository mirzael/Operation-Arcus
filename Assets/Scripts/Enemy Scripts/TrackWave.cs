using UnityEngine;
using System.Collections;

public class TrackWave : Wave {

	float cooldown;
	float currentCooldown;
	GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
		projectile = gameObject.GetComponent<Shooter> ().projectile;
		currentCooldown = 20;
		cooldown = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null && currentCooldown % 20 == 0 && currentCooldown < cooldown) 
			{
			var heading = player.transform.position - transform.position;
			var distance = heading.magnitude;
			var direction = heading / distance;
			var proj = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, transform.rotation);
			proj.rigidbody.velocity = direction * 10;
			}
		currentCooldown = currentCooldown + 1;
	}

	public override void resetCooldown()
	{
		currentCooldown = 0;
	}

}
