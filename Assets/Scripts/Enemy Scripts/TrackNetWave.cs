using UnityEngine;
using System.Collections;

public class TrackNetWave : Wave {
	
	float cooldown;
	float currentCooldown;
	GameObject player;
	Vector3 direction;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player");
		projectile = gameObject.GetComponent<Shooter> ().projectile;
		currentCooldown = 0;
		cooldown = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (player != null && currentCooldown % 20 == 0 && currentCooldown < cooldown) 
		{
			int aVal = (int)currentCooldown / 20;
			//if (aVal == 0)
			//{
				var heading = player.transform.position - transform.position;
				var distance = heading.magnitude;
				direction = heading / distance;
				Debug.Log ("DIRECTION: " + direction.ToString());
			//}
			GameObject[] proj = new GameObject[5];
			switch (aVal) 
				{
				case 0:
					proj [0] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, transform.rotation);
					proj [0].rigidbody.velocity = direction * 8;
					break;
				case 1:
					proj [0] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, transform.rotation);
					proj [0].rigidbody.velocity = direction * 8 + Vector3.left * .8f;
					proj [1] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, transform.rotation);
					proj [1].rigidbody.velocity = direction * 8 + Vector3.left * -.8f;
					break;
				case 2:
					proj [0] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, transform.rotation);
					proj [0].rigidbody.velocity = direction * 8 + Vector3.left * 1.6f;
					proj [1] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, transform.rotation);
					proj [1].rigidbody.velocity = direction * 8;
					proj [2] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, transform.rotation);
					proj [2].rigidbody.velocity = direction * 8 + Vector3.left * -1.6f;
					break;
				case 3:
					proj [0] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, transform.rotation);
					proj [0].rigidbody.velocity = direction * 8 + Vector3.left * 2.4f;
					proj [1] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, transform.rotation);
					proj [1].rigidbody.velocity = direction * 8 + Vector3.left * .8f;
					proj [2] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, transform.rotation);
					proj [2].rigidbody.velocity = direction * 8 + Vector3.left * -.8f;
					proj [3] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, transform.rotation);
					proj [3].rigidbody.velocity = direction * 8 + Vector3.left * -2.4f;
					break;
				case 4:
					proj [0] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, transform.rotation);
					proj [0].rigidbody.velocity = direction * 8 + Vector3.left * 3.2f;
					proj [1] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, transform.rotation);
					proj [1].rigidbody.velocity = direction * 8 + Vector3.left * 1.6f;
					proj [2] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, transform.rotation);
					proj [2].rigidbody.velocity = direction * 8;
					proj [3] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, transform.rotation);
					proj [3].rigidbody.velocity = direction * 8 + Vector3.left * -1.6f;
					proj [4] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, transform.rotation);
					proj [4].rigidbody.velocity = direction * 8 + Vector3.left * -3.2f;
					break;
				}
		}
		currentCooldown = currentCooldown + 1;
	}
	
	public override void resetCooldown()
	{
		currentCooldown = 0;
	}
	
}
