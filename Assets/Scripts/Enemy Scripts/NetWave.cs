using UnityEngine;
using System.Collections;

public class NetWave : Wave {

	float cooldown;
	float currentCooldown;

	// Use this for initialization
	void Start () {
		projectile = gameObject.GetComponent<Shooter> ().projectile;
		currentCooldown = 0;
		cooldown = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentCooldown % 20 == 0 && currentCooldown < cooldown) {
				GameObject[] proj = new GameObject[5];
				int aVal = (int)currentCooldown / 20;
				switch (aVal) {
				case 0:
						proj [0] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, transform.rotation);
						proj [0].rigidbody.velocity = Vector3.left * 15;
						break;
				case 1:
						proj [0] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, transform.rotation);
						proj [0].rigidbody.velocity = Vector3.left * 15 + Vector3.down * .2f;
						proj [1] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, transform.rotation);
						proj [1].rigidbody.velocity = Vector3.left * 15 + Vector3.down * -.2f;
						break;
				case 2:
						proj [0] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, transform.rotation);
						proj [0].rigidbody.velocity = Vector3.left * 15 + Vector3.down * .4f;
						proj [1] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, transform.rotation);
						proj [1].rigidbody.velocity = Vector3.left * 15;
						proj [2] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, transform.rotation);
						proj [2].rigidbody.velocity = Vector3.left * 15 + Vector3.down * -.4f;
						break;
				case 3:
						proj [0] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, transform.rotation);
						proj [0].rigidbody.velocity = Vector3.left * 15 + Vector3.down * .6f;
						proj [1] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, transform.rotation);
						proj [1].rigidbody.velocity = Vector3.left * 15 + Vector3.down * .2f;
						proj [2] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, transform.rotation);
						proj [2].rigidbody.velocity = Vector3.left * 15 + Vector3.down * -.2f;
						proj [3] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, transform.rotation);
						proj [3].rigidbody.velocity = Vector3.left * 15 + Vector3.down * -.6f;
						break;
				case 4:
						proj [0] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, transform.rotation);
						proj [0].rigidbody.velocity = Vector3.left * 15 + Vector3.down * .8f;
						proj [1] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, transform.rotation);
						proj [1].rigidbody.velocity = Vector3.left * 15 + Vector3.down * .4f;
						proj [2] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, transform.rotation);
						proj [2].rigidbody.velocity = Vector3.left * 15;
						proj [3] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, transform.rotation);
						proj [3].rigidbody.velocity = Vector3.left * 15 + Vector3.down * -.4f;
						proj [4] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, transform.rotation);
						proj [4].rigidbody.velocity = Vector3.left * 15 + Vector3.down * -.8f;
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
