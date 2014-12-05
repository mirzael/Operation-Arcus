using UnityEngine;
using System.Collections;

public class TrackShooter : MonoBehaviour {
	public float cooldown;
	public GameObject projectile;	
	GameObject player;
	float currentCooldown;


	// Use this for initialization
	void Start () {
		currentCooldown = cooldown;
		player = GameObject.FindWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		//currentCooldown -= Time.deltaTime;

		if (currentCooldown > 0) 
		{
			var heading = player.transform.position - transform.position;
			var distance = heading.magnitude;
			var direction = heading / distance;
			var proj = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, projectile.transform.rotation);
			proj.rigidbody.velocity = direction * 10;
		}
		currentCooldown = currentCooldown - 1;
		if (currentCooldown < (cooldown * -2))
				currentCooldown = cooldown;
	}
}
