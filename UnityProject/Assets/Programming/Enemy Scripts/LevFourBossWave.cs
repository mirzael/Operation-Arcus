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
			GameObject[] proj = new GameObject[11];
			proj[0] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
			proj[0].rigidbody.velocity = Vector3.down * 9;
			proj[1] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
			proj[1].rigidbody.velocity = Vector3.down * 8.5f + Vector3.left * 1f;
			proj[2] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
			proj[2].rigidbody.velocity = Vector3.down * 8f + Vector3.left * 2f;
			proj[3] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
			proj[3].rigidbody.velocity = Vector3.down * 7.5f + Vector3.left * 3f;
			proj[4] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
			proj[4].rigidbody.velocity = Vector3.down * 7f + Vector3.left * 4f;
			proj[5] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
			proj[5].rigidbody.velocity = Vector3.down * 6.75f + Vector3.left * 5f;
			proj[6] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
			proj[6].rigidbody.velocity = Vector3.down * 6.5f + Vector3.left * 6f;
			proj[7] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
			proj[7].rigidbody.velocity = Vector3.down * 6.25f + Vector3.left * 7f;
			proj[8] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
			proj[8].rigidbody.velocity = Vector3.down * 6f+ Vector3.left * 8f;
			proj[9] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
			proj[9].rigidbody.velocity = Vector3.down * 8.5f + Vector3.right * 1f;
			proj[10] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
			proj[10].rigidbody.velocity = Vector3.down * 8f + Vector3.right * 2f;
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
