using UnityEngine;
using System.Collections;

public class LevTwoBossWave : Wave {
	int desperation;
	int ability;
	int activeRed;
	int startup;
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
	Animator animator;
	
	// Use this for initialization
	public override void Start () {
		desperation = 0;
		ability = 0;
		activeRed = 0;
		startup = 0;
		waves = 0;
		offset = 0;
		leftRight = 1;
		lastColor = "Red";
		animator = gameObject.GetComponent<Animator> ();
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
				/*GameObject aProj = new GameObject();
				aProj = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 2f + Vector3.left * 5f, projectile.transform.rotation);
				aProj.rigidbody.velocity = Vector3.down * 40;*/
				animator.SetInteger ("BasicState",Random.Range (0,2));
				if (currentCooldown % 3 == 0)
					activeBullet = bossWhite;
				else
					activeBullet = bossBlue;
				if (currentCooldown % 5 == 0)
				{
					GameObject proj;
					proj = (GameObject)InstantiateBullet (activeBullet, transform.position + Vector3.down * 2, projectile.transform.rotation);
					proj.rigidbody.velocity = Vector3.down * 30;
				}
			}
			//Second basic attack: cones of bullets fire out while moving left and right
			//alternating blue and white
			else if (ability == 1)
			{
				/*GameObject aProj = new GameObject();
				aProj = (GameObject)Instantiate (bossRed, transform.position + Vector3.down * 2f + Vector3.left * 5f, projectile.transform.rotation);
				aProj.rigidbody.velocity = Vector3.down * 40;*/
				if (currentCooldown % 30 == 0)
				{
					waves = waves + 1;
					int projectileSpreadAngle = 170;
					int angleBetweenProjectiles = (projectileSpreadAngle / (15));
					float radToDeg =  Mathf.PI / 180;
					GameObject[] blast = new GameObject[16];
					for(int i = 0; i < 16; i++) {
						if (i % 4 == 0)
							activeBullet = bossWhite;
						else
							activeBullet = bossBlue;
						float trajectoryDegree = 90 + (projectileSpreadAngle / 2 - angleBetweenProjectiles * i);
						float currentAngularVelocity = Mathf.Cos(trajectoryDegree * radToDeg);
						blast[i] = (GameObject)Instantiate(activeBullet, transform.position + Vector3.down * 2, projectile.transform.rotation);
						blast[i].rigidbody.velocity = transform.TransformDirection(Vector3.back * 12 + Vector3.right * currentAngularVelocity * 12);
					}
					if (currentCooldown % 240 >= 120)
						ability = 0;
				}
			}
			else //ability activate! Ship charges forward and slowly inches back, while
				//firing cones of bullets.
			{
				/*GameObject aProj = new GameObject();
				aProj = (GameObject)Instantiate (bossYellow, transform.position + Vector3.down * 2f + Vector3.left * 5f, projectile.transform.rotation);
				aProj.rigidbody.velocity = Vector3.down * 40;*/
				Debug.Log("ABILITY!");
				if (currentCooldown % 20 == 0 && startup <= 0)
				{
					waves = waves + 1;

					GameObject[] blast = new GameObject[16];
					for(int i = 0; i < 16; i++) {
						if (i % 4 == offset)
							activeBullet = bossWhite;
						else
							activeBullet = bossBlue;
						float trajectoryDegree = 90 + (projectileSpreadAngle / 2 - angleBetweenProjectiles * i);
						float currentAngularVelocity = Mathf.Cos(trajectoryDegree * radToDeg);
						blast[i] = (GameObject)Instantiate(activeBullet, transform.position + Vector3.down * 2, projectile.transform.rotation);
						blast[i].rigidbody.velocity = transform.TransformDirection(Vector3.back * 12 + Vector3.right * currentAngularVelocity * 12);
					}
					offset = offset + 1;
					if (offset > 3)
						offset = 0;
					if (currentCooldown % 720 >= 240)
					{
						ability = 0;
					}
				}
				animator.SetInteger ("BossState", 0);
				startup = startup - 1;
			}

		} 
		else //DESPERATION MODE! Moving faster and faster, the ship keeps firing cones
			//of bullets alternating white, red, white, blue
		{
			Debug.Log ("DESPERATION!");
			if (currentCooldown % 1 == 0)
			{
				if (offset % 4 == 0)
					activeBullet = bossWhite;
				else if ((offset % 4 == 1) && (leftRight == 1))
				{
					if (lastColor == "Red")
					{
						activeBullet = bossBlue;
						lastColor = "Blue";
					}
					else
					{
						activeBullet = bossRed;
						lastColor = "Red";
					}
				}
				else if ((offset % 4 == 3) && (leftRight == -1))
				{
					if (lastColor == "Red")
					{
						activeBullet = bossBlue;
						lastColor = "Blue";
					}
					else
					{
						activeBullet = bossRed;
						lastColor = "Red";
					}
				}
				float trajectoryDegree = 90 + (projectileSpreadAngle / 2 - angleBetweenProjectiles * offset);
				float currentAngularVelocity = Mathf.Cos(trajectoryDegree * radToDeg);
				GameObject proj;
				proj = (GameObject)Instantiate(activeBullet, transform.position + Vector3.down * 2, projectile.transform.rotation);
				proj.rigidbody.velocity = transform.TransformDirection(Vector3.back * 6 + Vector3.right * currentAngularVelocity * 12);
				offset = offset + leftRight;
				if (offset > 15)
				{
					offset = 14;
					leftRight = -1;
				}

				if (offset < 0)
				{
					offset = 1;
					leftRight = 1;
				}
			}
		}
		//}
		if (currentCooldown % 720 >= 700)
			animator.SetInteger ("BossState", 7);
		else if (currentCooldown % 240 >= 220)
			animator.SetInteger ("BossState", 4);

		if (currentCooldown % 720 == 0 && currentCooldown > 0)
		{
			ability = 2;
			waves = 0;
			startup = 70;
			offset = Random.Range (0,4);
		}
		else if (currentCooldown % 240 == 0 && currentCooldown > 0)
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
		animator.SetInteger ("BossState", 10);
	}
	
}
