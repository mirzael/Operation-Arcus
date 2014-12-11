using UnityEngine;
using System.Collections;

public class DualPulseWave : Wave {
	
	// Use this for initialization
	public override void Start () {
		base.Start ();
		projectile = gameObject.GetComponent<Shooter> ().projectile;
		currentCooldown = 0;
		cooldown = 120;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentCooldown % 20 == 0 && currentCooldown < cooldown) {
			GameObject[] proj = new GameObject[3];
			int aVal = (int)currentCooldown % 40;
			Debug.Log (aVal.ToString() + "IVE GOT STUFF");
			switch (aVal) {
			case 0:
				proj [0] = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 2 + Vector3.left * .6f, transform.rotation);
				proj [0].rigidbody.velocity = Vector3.down * 15;
				proj [1] = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 2 + Vector3.left * .6f, transform.rotation);
				proj [1].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 2f;
				proj [2] = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 2 + Vector3.left * .6f, transform.rotation);
				proj [2].rigidbody.velocity = Vector3.down * 15 + Vector3.left * -2f;
				break;
			case 20:
				proj [0] = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 2 + Vector3.right * .6f, transform.rotation);
				proj [0].rigidbody.velocity = Vector3.down * 15;
				proj [1] = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 2 + Vector3.right * .6f, transform.rotation);
				proj [1].rigidbody.velocity = Vector3.down * 15 + Vector3.left * -2f;
				proj [2] = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 2 + Vector3.right * .6f, transform.rotation);
				proj [2].rigidbody.velocity = Vector3.down * 15 + Vector3.left * -2f;
				break;
			}
		}
		if (Time.timeScale != 0)
			currentCooldown = currentCooldown + 1;
	}
	
	public override void resetCooldown()
	{
		currentCooldown = 0;
	}

}
