using UnityEngine;
using System.Collections;

public class LevFiveBossWave : Wave {
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
					proj[i] = (GameObject)InstantiateBullet (projectile, transform.position + Vector3.down * 2, projectile.transform.rotation);
					proj[i].rigidbody.velocity = Vector3.down * (30 - (i * 3));
				}
			}
			else //ability activate!
			{
				
			}
			
		} 
		else //DESPERATION MODE!
		{
			
		}
		//}
		if (currentCooldown % 720 == 0)
		{
			ability = 2;
			waves = 0;
			offset = Random.Range (0,4);
		}
		else if (currentCooldown % 240 == 0)
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
