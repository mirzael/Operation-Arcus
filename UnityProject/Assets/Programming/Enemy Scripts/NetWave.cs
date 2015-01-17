using UnityEngine;
using System.Collections;

public class NetWave : Wave {

	// Use this for initialization
	public override void Start () {
		base.Start ();
		projectile = gameObject.GetComponent<Shooter> ().projectile;
		currentCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentCooldown % 20 == 0 && currentCooldown < cooldown) {
				GameObject[] proj = new GameObject[5];
				int aVal = (int)currentCooldown / 20;
				switch (aVal) {
				case 0:
						proj [0] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, projectile.transform.rotation);
						proj [0].rigidbody.velocity = Vector3.down * 15;
						break;
				case 1:
						proj [0] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, projectile.transform.rotation);
						proj [0].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 2f;
						proj [1] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, projectile.transform.rotation);
						proj [1].rigidbody.velocity = Vector3.down * 15 + Vector3.left * -2f;
						break;
				case 2:
						proj [0] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, projectile.transform.rotation);
						proj [0].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 4f;
						proj [1] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, projectile.transform.rotation);
						proj [1].rigidbody.velocity = Vector3.down * 15;
						proj [2] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, projectile.transform.rotation);
						proj [2].rigidbody.velocity = Vector3.down * 15 + Vector3.left * -4f;
						break;
				case 3:
						proj [0] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, projectile.transform.rotation);
						proj [0].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 6f;
						proj [1] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, projectile.transform.rotation);
						proj [1].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 2f;
						proj [2] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, projectile.transform.rotation);
						proj [2].rigidbody.velocity = Vector3.down * 15 + Vector3.left * -2f;
						proj [3] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, projectile.transform.rotation);
						proj [3].rigidbody.velocity = Vector3.down * 15 + Vector3.left * -6f;
						break;
				case 4:
						proj [0] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, projectile.transform.rotation);
						proj [0].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 8f;
						proj [1] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, projectile.transform.rotation);
						proj [1].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 4f;
						proj [2] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, projectile.transform.rotation);
						proj [2].rigidbody.velocity = Vector3.down * 15;
						proj [3] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, projectile.transform.rotation);
						proj [3].rigidbody.velocity = Vector3.down * 15 + Vector3.left * -4f;
						proj [4] = (GameObject)Instantiate (projectile, transform.position + Vector3.down * 2, projectile.transform.rotation);
						proj [4].rigidbody.velocity = Vector3.down * 15 + Vector3.left * -8f;
						break;
				}
		}
		if(Time.timeScale != 0) currentCooldown = currentCooldown + 1;
	}

	public override void resetCooldown()
	{
		currentCooldown = 0;
	}

}
