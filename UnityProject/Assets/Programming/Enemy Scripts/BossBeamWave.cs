using UnityEngine;
using System.Collections;

public class BossBeamWave : Wave {
	
	// Use this for initialization
	public override void Start () {
		base.Start ();
		projectile = gameObject.GetComponent<Shooter> ().projectile;
		currentCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//if (currentCooldown % 8 == 0) 
		//{
				int aVal = (int)(currentCooldown) % 42;
				float offset;
				int side = 0;

				if (aVal >= 21)
						side = -1;
				else 
						side = 1;

				offset = ((currentCooldown % 42) - 10) / 10 * side;
				GameObject projl, projr, projc;

				projc = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 2 + Vector3.right * offset, projectile.transform.rotation);
				projc.rigidbody.velocity = Vector3.down * 30;

				projl = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 1.8f + Vector3.left * 1f, projectile.transform.rotation);
				projl.rigidbody.velocity = Vector3.down * 30;
		
				projr = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 1.8f + Vector3.right * 1f, projectile.transform.rotation);
				projr.rigidbody.velocity = Vector3.down * 30;
				
		//}
		currentCooldown = currentCooldown + 1;
	}
	
	public override void resetCooldown()
	{
	}
	
}
