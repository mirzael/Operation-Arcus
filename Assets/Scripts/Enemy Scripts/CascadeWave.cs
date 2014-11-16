using UnityEngine;
using System.Collections;

public class CascadeWave : Wave {
	
	// Use this for initialization
	public override void Start () {
		base.Start ();
		currentCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentCooldown % 10 == 0 && currentCooldown < cooldown) {
			GameObject[] proj = new GameObject[5];
			int aVal = (int)currentCooldown / 10;
			switch (aVal) {
			case 0:
				proj [0] = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 2, transform.rotation);
				proj [0].rigidbody.velocity = Vector3.left * 9;
				break;
			case 1:
				proj [0] = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 2, transform.rotation);
				proj [0].rigidbody.velocity = Vector3.left * 8 + Vector3.down * 2;
				break;
			case 2:
				proj [0] = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 2, transform.rotation);
				proj [0].rigidbody.velocity = Vector3.left * 7 + Vector3.down * 3.5f;
				break;
			case 3:
				proj [0] = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 2, transform.rotation);
				proj [0].rigidbody.velocity = Vector3.left * 6 + Vector3.down * 5;
				break;
			case 4:
				proj [0] = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 2, transform.rotation);
				proj [0].rigidbody.velocity = Vector3.left * 5 + Vector3.down * 6.5f;
				break;
			case 5:
				proj [0] = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 2, transform.rotation);
				proj [0].rigidbody.velocity = Vector3.left * 4 + Vector3.down * 8;
				break;
			case 6:
				proj [0] = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 2, transform.rotation);
				proj [0].rigidbody.velocity = Vector3.left * 3 + Vector3.down * 9.5f;
				break;
			case 7:
				proj [0] = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 2, transform.rotation);
				proj [0].rigidbody.velocity = Vector3.left * 2 + Vector3.down * 11;
				break;
			case 8:
				proj [0] = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 2, transform.rotation);
				proj [0].rigidbody.velocity = Vector3.left * 1 + Vector3.down * 12;
				break;
			case 9:
				proj [0] = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 2, transform.rotation);
				proj [0].rigidbody.velocity = Vector3.down * 13;
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
