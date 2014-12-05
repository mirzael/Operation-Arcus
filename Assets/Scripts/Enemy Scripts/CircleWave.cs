using UnityEngine;
using System.Collections;

public class CircleWave : Wave {


	// Use this for initialization
	public override void Start () {
		base.Start ();
		currentCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentCooldown % cooldown == 0) {
			GameObject[] projs = new GameObject[4];
			if (isBoss && Random.Range (0, 100) <= bossBulletChance) {
				projs[0] = (GameObject)InstantiateBullet(bossProj, transform.position + Vector3.left, projectile.transform.rotation);
				projs[0].rigidbody.velocity = Vector3.left * 5;

				projs[1] = (GameObject)InstantiateBullet(bossProj, transform.position + Vector3.right, projectile.transform.rotation);
				projs[1].rigidbody.velocity = Vector3.right * 5;

				projs[2] = (GameObject)InstantiateBullet(bossProj, transform.position + Vector3.up, projectile.transform.rotation);
				projs[2].rigidbody.velocity = Vector3.up * 5;

				projs[3] = (GameObject)InstantiateBullet(bossProj, transform.position + Vector3.down, projectile.transform.rotation);
				projs[3].rigidbody.velocity = Vector3.down * 5;
			}
			else
			{
				projs[0] = (GameObject)InstantiateBullet(projectile, transform.position + Vector3.left, projectile.transform.rotation);
				projs[0].rigidbody.velocity = Vector3.left * 5;
				
				projs[1] = (GameObject)InstantiateBullet(projectile, transform.position + Vector3.right, projectile.transform.rotation);
				projs[1].rigidbody.velocity = Vector3.right * 5;
				
				projs[2] = (GameObject)InstantiateBullet(projectile, transform.position + Vector3.up, projectile.transform.rotation);
				projs[2].rigidbody.velocity = Vector3.up * 5;
				
				projs[3] = (GameObject)InstantiateBullet(projectile, transform.position + Vector3.down, projectile.transform.rotation);
				projs[3].rigidbody.velocity = Vector3.down * 5;
			}
		}
		currentCooldown = currentCooldown + 1;
	}
}
