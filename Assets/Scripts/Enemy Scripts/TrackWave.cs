using UnityEngine;
using System.Collections;

public class TrackWave : Wave {
	GameObject player;

	// Use this for initialization
	public override void Start () {
		base.Start ();
		player = GameObject.FindWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null && currentCooldown % 20 == 0 && currentCooldown < cooldown) 
			{
			var heading = player.transform.position - transform.position;
			var distance = heading.magnitude;
			var direction = heading / distance;
			var proj = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 2, transform.rotation);
			proj.rigidbody.velocity = direction * 10;
			}
		currentCooldown = currentCooldown + 1;
	}

	public override void resetCooldown()
	{
		currentCooldown = 0;
	}

}
