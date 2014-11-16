using UnityEngine;
using System.Collections;

public class MachineGunWave : Wave {
	
	// Use this for initialization
	public override void Start () {
		base.Start ();
		projectile = gameObject.GetComponent<Shooter> ().projectile;
		currentCooldown = 0;
	}

	// Update is called once per frame
	void Update () {
		if (currentCooldown % 10 == 0 && currentCooldown < cooldown) 
		{
			int aVal = (int)currentCooldown % 30;
			GameObject proj;
			switch (aVal)
			{
			case 0:
				proj = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 2, transform.rotation);
				proj.rigidbody.velocity = Vector3.down * 30;
				break;
			case 10:
				proj = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 1.8f + Vector3.left * .2f, transform.rotation);
				proj.rigidbody.velocity = Vector3.down * 30;
				break;
			case 20:
				proj = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 1.8f + Vector3.right * .2f, transform.rotation);
				proj.rigidbody.velocity = Vector3.down * 30;
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
