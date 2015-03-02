using UnityEngine;
using System.Collections;

public class LevFourBossWave : Wave {
	int desperation;
	int ability;
	int activeRed;
	int beamStartup;
	int waves;
	int offset;
	int projectileSpreadAngle;
	int angleBetweenProjectiles;
	int leftRight;
	string lastColor;
	float radToDeg;
	GameObject bossRed;
	GameObject bossBlue;
	GameObject bossYellow;
	GameObject bossWhite;
	GameObject activeBullet;
	
	// Use this for initialization
	public override void Start () {
		desperation = 0;
		ability = 0;
		activeRed = 0;
		beamStartup = 0;
		waves = 0;
		offset = 0;
		leftRight = 1;
		lastColor = "Red";
		projectileSpreadAngle = 180;
		angleBetweenProjectiles = (projectileSpreadAngle / (15));
		radToDeg =  Mathf.PI / 180;
		base.Start ();
		bossRed = gameObject.GetComponent<Shooter> ().bossRed;
		bossBlue = gameObject.GetComponent<Shooter> ().bossBlue;
		bossYellow = gameObject.GetComponent<Shooter> ().bossYellow;
		bossWhite = gameObject.GetComponent<Shooter> ().bossProjectile;
		activeBullet = bossRed;
		currentCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//if (currentCooldown % 8 == 0) 
		//{
		if (desperation == 0) {
			//basic attack 1, beam of semi-alternating blue and white
			if (ability == 0)
			{
				if (currentCooldown % 30 == 0)
					if (currentCooldown % 90 == 0)
						activeBullet = bossWhite;
					else
						activeBullet = bossRed;
				GameObject[] proj = new GameObject[8];
				for (int i = 0; i < 8; i++)
				{
					proj[i] = (GameObject)InstantiateBullet (activeBullet, transform.position + Vector3.down * 2, projectile.transform.rotation);
					proj[i].rigidbody.velocity = Vector3.down * (30 - (i * 3));
				}
			}
			else //ability activate!
			{
				if (currentCooldown % 30 == 0)
				{
					GameObject[] proj = new GameObject[2];
					proj[0] = (GameObject)InstantiateBullet (bossWhite, transform.position, projectile.transform.rotation);
					proj[0].rigidbody.velocity = Vector3.left * 20;
					proj[1] = (GameObject)InstantiateBullet (bossWhite, transform.position, projectile.transform.rotation);
					proj[1].rigidbody.velocity = Vector3.right * 20;
				}
				if (currentCooldown % 240 == 0)
					ability = 0;
			}
			
		} 
		else //DESPERATION MODE!
		{
			GameObject[] proj = new GameObject[16];
			proj[0] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f, projectile.transform.rotation);
			proj[0].rigidbody.velocity = Vector3.down * 9;
			proj[1] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f, projectile.transform.rotation);
			proj[1].rigidbody.velocity = Vector3.down * 8.31f + Vector3.left * 3.44f;
			proj[2] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f, projectile.transform.rotation);
			proj[2].rigidbody.velocity = Vector3.down * 6.36f + Vector3.left * 6.36f;
			proj[3] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f, projectile.transform.rotation);
			proj[3].rigidbody.velocity = Vector3.down * 3.44f + Vector3.left * 8.31f;
			proj[4] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f, projectile.transform.rotation);
			proj[4].rigidbody.velocity = Vector3.left * 9f;
			proj[5] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f, projectile.transform.rotation);
			proj[5].rigidbody.velocity = Vector3.left * 8.31f + Vector3.up * 3.44f;
			proj[6] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f, projectile.transform.rotation);
			proj[6].rigidbody.velocity = Vector3.left * 6.36f + Vector3.up * 6.36f;
			proj[7] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f, projectile.transform.rotation);
			proj[7].rigidbody.velocity = Vector3.left * 3.44f + Vector3.up * 8.31f;
			proj[8] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f, projectile.transform.rotation);
			proj[8].rigidbody.velocity = Vector3.up * 9f;
			proj[9] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f, projectile.transform.rotation);
			proj[9].rigidbody.velocity = Vector3.up * 8.31f + Vector3.right * 3.44f;
			proj[10] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f, projectile.transform.rotation);
			proj[10].rigidbody.velocity = Vector3.up * 6.36f + Vector3.right * 6.36f;
			proj[11] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f, projectile.transform.rotation);
			proj[11].rigidbody.velocity = Vector3.up * 3.44f + Vector3.right * 8.31f;
			proj[12] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f, projectile.transform.rotation);
			proj[12].rigidbody.velocity = Vector3.right * 9;
			proj[13] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f, projectile.transform.rotation);
			proj[13].rigidbody.velocity = Vector3.right * 8.31f + Vector3.down * 3.44f;
			proj[14] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f, projectile.transform.rotation);
			proj[14].rigidbody.velocity = Vector3.right * 6.36f + Vector3.down * 6.36f;
			proj[15] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f, projectile.transform.rotation);
			proj[15].rigidbody.velocity = Vector3.right * 3.44f + Vector3.down * 8.31f;
		}
		//}
		if (currentCooldown % 360 == 0)
		{
			ability = 1;
			waves = 0;
		}
		currentCooldown = currentCooldown + 1;
	}
	
	public override void resetCooldown()
	{
	}
	
	public override void triggerDesperation()
	{
		desperation = 1;
		waves = 0;
	}
	
}
