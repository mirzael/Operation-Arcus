﻿using UnityEngine;
using System.Collections;

public class LevSixBossWave : Wave {
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
	GameObject activeLeft;
	GameObject activeRight;
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
		activeLeft = bossBlue;
		activeRight = bossYellow;
		currentCooldown = 0;
	}
	
	// Update is called once per frame
	void Update () {
		currentCooldown = currentCooldown + 1;
		//if (currentCooldown % 8 == 0) 
		//{
		animator.SetInteger ("BasicState",Random.Range (0,2));
		if (desperation == 0) {
			//basic attack 1, circle-firing from the two spheres
			if (currentCooldown % 40 == 0)
			{
				GameObject[] proj = new GameObject[16];
				proj[0] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[0].rigidbody.velocity = Vector3.down * 9;
				proj[1] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[1].rigidbody.velocity = Vector3.down * 8.31f + Vector3.left * 3.44f;
				proj[2] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[2].rigidbody.velocity = Vector3.down * 6.36f + Vector3.left * 6.36f;
				proj[3] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[3].rigidbody.velocity = Vector3.down * 3.44f + Vector3.left * 8.31f;
				proj[4] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[4].rigidbody.velocity = Vector3.left * 9f;
				proj[5] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[5].rigidbody.velocity = Vector3.left * 8.31f + Vector3.up * 3.44f;
				proj[6] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[6].rigidbody.velocity = Vector3.left * 6.36f + Vector3.up * 6.36f;
				proj[7] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[7].rigidbody.velocity = Vector3.left * 3.44f + Vector3.up * 8.31f;
				proj[8] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[8].rigidbody.velocity = Vector3.up * 9f;
				proj[9] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[9].rigidbody.velocity = Vector3.up * 8.31f + Vector3.right * 3.44f;
				proj[10] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[10].rigidbody.velocity = Vector3.up * 6.36f + Vector3.right * 6.36f;
				proj[11] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[11].rigidbody.velocity = Vector3.up * 3.44f + Vector3.right * 8.31f;
				proj[12] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[12].rigidbody.velocity = Vector3.right * 9;
				proj[13] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[13].rigidbody.velocity = Vector3.right * 8.31f + Vector3.down * 3.44f;
				proj[14] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[14].rigidbody.velocity = Vector3.right * 6.36f + Vector3.down * 6.36f;
				proj[15] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[15].rigidbody.velocity = Vector3.right * 3.44f + Vector3.down * 8.31f;
			}
			else if (currentCooldown % 20 == 0)
			{
				GameObject[] proj = new GameObject[16];
				proj[0] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[0].rigidbody.velocity = Vector3.down * 9;
				proj[1] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[1].rigidbody.velocity = Vector3.down * 8.31f + Vector3.left * 3.44f;
				proj[2] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[2].rigidbody.velocity = Vector3.down * 6.36f + Vector3.left * 6.36f;
				proj[3] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[3].rigidbody.velocity = Vector3.down * 3.44f + Vector3.left * 8.31f;
				proj[4] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[4].rigidbody.velocity = Vector3.left * 9f;
				proj[5] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[5].rigidbody.velocity = Vector3.left * 8.31f + Vector3.up * 3.44f;
				proj[6] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[6].rigidbody.velocity = Vector3.left * 6.36f + Vector3.up * 6.36f;
				proj[7] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[7].rigidbody.velocity = Vector3.left * 3.44f + Vector3.up * 8.31f;
				proj[8] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[8].rigidbody.velocity = Vector3.up * 9f;
				proj[9] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[9].rigidbody.velocity = Vector3.up * 8.31f + Vector3.right * 3.44f;
				proj[10] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[10].rigidbody.velocity = Vector3.up * 6.36f + Vector3.right * 6.36f;
				proj[11] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[11].rigidbody.velocity = Vector3.up * 3.44f + Vector3.right * 8.31f;
				proj[12] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[12].rigidbody.velocity = Vector3.right * 9;
				proj[13] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[13].rigidbody.velocity = Vector3.right * 8.31f + Vector3.down * 3.44f;
				proj[14] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[14].rigidbody.velocity = Vector3.right * 6.36f + Vector3.down * 6.36f;
				proj[15] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[15].rigidbody.velocity = Vector3.right * 3.44f + Vector3.down * 8.31f;
			}
			//Second basic attack: sphere's bullet colors move in waves
			if (ability == 1)
			{
				if (currentCooldown % 10 == 0)
				{
					GameObject proj = new GameObject();
					int checkVal = (int)currentCooldown % 120;
					switch (checkVal)
					{
					case 0:
						proj = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 7f, projectile.transform.rotation);
						proj.rigidbody.velocity = Vector3.down * 4 + Vector3.left * 6f;
						break;
					case 10:
						proj = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 7f, projectile.transform.rotation);
						proj.rigidbody.velocity = Vector3.down * 5f + Vector3.left * 5f;
						break;
					case 20:
						proj = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 7f, projectile.transform.rotation);
						proj.rigidbody.velocity = Vector3.down * 6f + Vector3.left * 4f;
						break;
					case 30:
						proj = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 7f, projectile.transform.rotation);
						proj.rigidbody.velocity = Vector3.down * 7f + Vector3.left * 3f;
						break;
					case 40:
						proj = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 7f, projectile.transform.rotation);
						proj.rigidbody.velocity = Vector3.down * 8f + Vector3.right * 2f;
						break;
					case 50:
						proj = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 7f, projectile.transform.rotation);
						proj.rigidbody.velocity = Vector3.down * 9f + Vector3.right * 1f;
						break;
					case 60:
						proj = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 7f, projectile.transform.rotation);
						proj.rigidbody.velocity = Vector3.down * 9f + Vector3.right * 1f;
						break;
					case 70:
						proj = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 7f, projectile.transform.rotation);
						proj.rigidbody.velocity = Vector3.down * 8f + Vector3.right * 2f;
						break;
					case 80:
						proj = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 7f, projectile.transform.rotation);
						proj.rigidbody.velocity = Vector3.down * 7f + Vector3.right * 3f;
						break;
					case 90:
						proj = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 7f, projectile.transform.rotation);
						proj.rigidbody.velocity = Vector3.down * 6f + Vector3.right * 4f;
						break;
					case 100:
						proj = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 7f, projectile.transform.rotation);
						proj.rigidbody.velocity = Vector3.down * 5f + Vector3.right * 5f;
						break;
					case 110:
						proj = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 7f, projectile.transform.rotation);
						proj.rigidbody.velocity = Vector3.down * 4f + Vector3.right * 6f;
						break;
					}
				}
				if (currentCooldown % 360 == 0)
				{
					ability = 0;
					int bullets = Random.Range(0,6);
					switch (bullets)
					{
					case 0:
						activeLeft = bossRed;
						activeRight = bossBlue;
						break;
					case 1:
						activeLeft = bossRed;
						activeRight = bossYellow;
						break;
					case 2:
						activeLeft = bossBlue;
						activeRight = bossRed;
						break;
					case 3:
						activeLeft = bossBlue;
						activeRight = bossYellow;
						break;
					case 4:
						activeLeft = bossYellow;
						activeRight = bossBlue;
						break;
					case 5:
						activeLeft = bossYellow;
						activeRight = bossBlue;
						break;
					}
				}
			}
			
		} 
		else //DESPERATION MODE! Moving faster and faster, the ship keeps firing cones
			//of bullets alternating white, red, white, blue
		{
			Debug.Log ("DESPERATION!");
			if (currentCooldown % 40 == 0)
			{
				GameObject[] proj = new GameObject[11];
				proj[0] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[0].rigidbody.velocity = Vector3.down * 9;
				proj[1] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[1].rigidbody.velocity = Vector3.down * 8.5f + Vector3.left * 1f;
				proj[2] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[2].rigidbody.velocity = Vector3.down * 8f + Vector3.left * 2f;
				proj[3] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[3].rigidbody.velocity = Vector3.down * 7.5f + Vector3.left * 3f;
				proj[4] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[4].rigidbody.velocity = Vector3.down * 7f + Vector3.left * 4f;
				proj[5] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[5].rigidbody.velocity = Vector3.down * 6.75f + Vector3.left * 5f;
				proj[6] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[6].rigidbody.velocity = Vector3.down * 6.5f + Vector3.left * 6f;
				proj[7] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[7].rigidbody.velocity = Vector3.down * 6.25f + Vector3.left * 7f;
				proj[8] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[8].rigidbody.velocity = Vector3.down * 6f+ Vector3.left * 8f;
				proj[9] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[9].rigidbody.velocity = Vector3.down * 8.5f + Vector3.right * 1f;
				proj[10] = (GameObject)Instantiate (activeLeft, transform.position + Vector3.down * 4.5f + Vector3.left * 5f, projectile.transform.rotation);
				proj[10].rigidbody.velocity = Vector3.down * 8f + Vector3.right * 2f;
			}
			else if (currentCooldown % 20 == 0)
			{
				GameObject[] proj = new GameObject[11];
				proj[0] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[0].rigidbody.velocity = Vector3.down * 9;
				proj[1] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[1].rigidbody.velocity = Vector3.down * 8.5f + Vector3.right * 1f;
				proj[2] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[2].rigidbody.velocity = Vector3.down * 8f + Vector3.right * 2f;
				proj[3] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[3].rigidbody.velocity = Vector3.down * 7.5f + Vector3.right * 3f;
				proj[4] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[4].rigidbody.velocity = Vector3.down * 7f + Vector3.right * 4f;
				proj[5] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[5].rigidbody.velocity = Vector3.down * 6.75f + Vector3.right * 5f;
				proj[6] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[6].rigidbody.velocity = Vector3.down * 6.5f + Vector3.right * 6f;
				proj[7] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[7].rigidbody.velocity = Vector3.down * 6.25f + Vector3.right * 7f;
				proj[8] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[8].rigidbody.velocity = Vector3.down * 6f+ Vector3.right * 8f;
				proj[9] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[9].rigidbody.velocity = Vector3.down * 8.5f + Vector3.left * 1f;
				proj[10] = (GameObject)Instantiate (activeRight, transform.position + Vector3.down * 4.5f + Vector3.right * 5f, projectile.transform.rotation);
				proj[10].rigidbody.velocity = Vector3.down * 8f + Vector3.left * 2f;
			}

			if (currentCooldown % 50 == 0)
			{
				GameObject[] proj = new GameObject[8];
				proj[0] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 7f, projectile.transform.rotation);
				proj[0].rigidbody.velocity = Vector3.down * 6 + Vector3.left * 4f;
				proj[1] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 7f, projectile.transform.rotation);
				proj[1].rigidbody.velocity = Vector3.down * 7f + Vector3.left * 3f;
				proj[2] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 7f, projectile.transform.rotation);
				proj[2].rigidbody.velocity = Vector3.down * 8f + Vector3.left * 2f;
				proj[3] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 7f, projectile.transform.rotation);
				proj[3].rigidbody.velocity = Vector3.down * 9f + Vector3.left * 1f;
				proj[4] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 7f, projectile.transform.rotation);
				proj[4].rigidbody.velocity = Vector3.down * 9f + Vector3.right * 1f;
				proj[5] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 7f, projectile.transform.rotation);
				proj[5].rigidbody.velocity = Vector3.down * 8f + Vector3.right * 2f;
				proj[6] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 7f, projectile.transform.rotation);
				proj[6].rigidbody.velocity = Vector3.down * 7f + Vector3.right * 3f;
				proj[7] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 7f, projectile.transform.rotation);
				proj[7].rigidbody.velocity = Vector3.down * 6f + Vector3.right * 4f;
			}
		}

		if (currentCooldown % 360 == 0 && currentCooldown > 0)
		{
			ability = 1;
			waves = 0;
		}
		
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
