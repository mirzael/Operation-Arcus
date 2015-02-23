using UnityEngine;
using System.Collections;

public class LevThreeBossWave : Wave {
	int desperation;
	int ability;
	int startup;
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
		lastColor = "Red";
		startup = 0;
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
				if (currentCooldown % 60 == 0)
				{
					GameObject[] proj = new GameObject[16];
					proj[0] = (GameObject)Instantiate (bossYellow, transform.position + Vector3.down * 4.5f + Vector3.left * 1f, projectile.transform.rotation);
					proj[0].rigidbody.velocity = Vector3.down * 12;
					proj[1] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f + Vector3.left * 1f, projectile.transform.rotation);
					proj[1].rigidbody.velocity = Vector3.down * 10;
					proj[2] = (GameObject)Instantiate (bossYellow, transform.position + Vector3.down * 4.5f + Vector3.left * 1f, projectile.transform.rotation);
					proj[2].rigidbody.velocity = Vector3.down * 10.5f + Vector3.left * 2.5f;
					proj[3] = (GameObject)Instantiate (bossYellow, transform.position + Vector3.down * 4.5f + Vector3.left * 1f, projectile.transform.rotation);
					proj[3].rigidbody.velocity = Vector3.down * 10.5f + Vector3.right * 2.5f;
					proj[4] = (GameObject)Instantiate (bossYellow, transform.position + Vector3.down * 4.5f + Vector3.left * 1f, projectile.transform.rotation);
					proj[4].rigidbody.velocity = Vector3.down * 9 + Vector3.left * 4f;
					proj[5] = (GameObject)Instantiate (bossYellow, transform.position + Vector3.down * 4.5f + Vector3.left * 1f, projectile.transform.rotation);
					proj[5].rigidbody.velocity = Vector3.down * 9 + Vector3.right * 4f;
					proj[6] = (GameObject)Instantiate (bossYellow, transform.position + Vector3.down * 4.5f + Vector3.left * 1f, projectile.transform.rotation);
					proj[6].rigidbody.velocity = Vector3.down * 7.5f + Vector3.left * 5.5f;
					proj[7] = (GameObject)Instantiate (bossYellow, transform.position + Vector3.down * 4.5f + Vector3.left * 1f, projectile.transform.rotation);
					proj[7].rigidbody.velocity = Vector3.down * 7.5f + Vector3.right * 5.5f;
					proj[8] = (GameObject)Instantiate (bossYellow, transform.position + Vector3.down * 4.5f + Vector3.left * 1f, projectile.transform.rotation);
					proj[8].rigidbody.velocity = Vector3.down * 12;
					proj[9] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f + Vector3.right * 1f, projectile.transform.rotation);
					proj[9].rigidbody.velocity = Vector3.down * 10;
					proj[10] = (GameObject)Instantiate (bossYellow, transform.position + Vector3.down * 4.5f + Vector3.right * 1f, projectile.transform.rotation);
					proj[10].rigidbody.velocity = Vector3.down * 10.5f + Vector3.left * 2.5f;
					proj[11] = (GameObject)Instantiate (bossYellow, transform.position + Vector3.down * 4.5f + Vector3.right * 1f, projectile.transform.rotation);
					proj[11].rigidbody.velocity = Vector3.down * 10.5f + Vector3.right * 2.5f;
					proj[12] = (GameObject)Instantiate (bossYellow, transform.position + Vector3.down * 4.5f + Vector3.right * 1f, projectile.transform.rotation);
					proj[12].rigidbody.velocity = Vector3.down * 9 + Vector3.left * 4f;
					proj[13] = (GameObject)Instantiate (bossYellow, transform.position + Vector3.down * 4.5f + Vector3.right * 1f, projectile.transform.rotation);
					proj[13].rigidbody.velocity = Vector3.down * 9 + Vector3.left * 4f;
					proj[14] = (GameObject)Instantiate (bossYellow, transform.position + Vector3.down * 4.5f + Vector3.right * 1f, projectile.transform.rotation);
					proj[14].rigidbody.velocity = Vector3.down * 7.5f + Vector3.left * 5.5f;
					proj[15] = (GameObject)Instantiate (bossYellow, transform.position + Vector3.down * 4.5f + Vector3.right * 1f, projectile.transform.rotation);
					proj[15].rigidbody.velocity = Vector3.down * 7.5f + Vector3.left * 5.5f;
				}
			}
			else
			{
				if (currentCooldown % 720 >= 60)
				{
					if (currentCooldown % 10 == 0)
					{
						GameObject[] proj = new GameObject[7];
						proj[0] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 6f + Vector3.left * 1f, projectile.transform.rotation);
						proj[0].rigidbody.velocity = Vector3.left * 12;
						proj[1] = (GameObject)Instantiate (bossYellow, transform.position + Vector3.down * 3f + Vector3.left * 1f, projectile.transform.rotation);
						proj[1].rigidbody.velocity = Vector3.left * 12 + Vector3.up * 2f;
						proj[2] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 1.5f + Vector3.left * 1f, projectile.transform.rotation);
						proj[2].rigidbody.velocity = Vector3.left * 12f;
						proj[3] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.left * 1f, projectile.transform.rotation);
						proj[3].rigidbody.velocity = Vector3.left * 12f;
						proj[4] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.up * 1.5f + Vector3.left * 1f, projectile.transform.rotation);
						proj[4].rigidbody.velocity = Vector3.left * 12;
						proj[5] = (GameObject)Instantiate (bossYellow, transform.position + Vector3.up * 3f + Vector3.left * 1f, projectile.transform.rotation);
						proj[5].rigidbody.velocity = Vector3.left * 12 + Vector3.down * 2f;
						proj[6] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.up * 6f + Vector3.left * 1f, projectile.transform.rotation);
						proj[6].rigidbody.velocity = Vector3.left * 12f;
					}
				}
				if (currentCooldown % 720 >= 360)
				{
					ability = 0;
					//change animator
				}
			}
			
		} 
		else //DESPERATION MODE!
		{
			int pattern = Random.Range(1,4);
			if (currentCooldown % 120 == 0)
				startup = 30;
			startup = startup - 1;
			if (startup <= 0)
			{
				GameObject[] proj = new GameObject[9];
				switch (pattern)
				{
					case 1:
						proj[0] = (GameObject)Instantiate (bossYellow, transform.position + Vector3.up * 6f + Vector3.left * 1f, projectile.transform.rotation);
						proj[0].rigidbody.velocity = Vector3.left * 12 + Vector3.up * 6f;
						proj[1] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f + Vector3.left * 1f, projectile.transform.rotation);
						proj[1].rigidbody.velocity = Vector3.left * 12 + Vector3.up * 4f;
						proj[2] = (GameObject)Instantiate (bossYellow, transform.position + Vector3.down * 3f + Vector3.left * 1f, projectile.transform.rotation);
						proj[2].rigidbody.velocity = Vector3.left * 12 + Vector3.up * 2f;
						proj[3] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 1.5f + Vector3.left * 1f, projectile.transform.rotation);
						proj[3].rigidbody.velocity = Vector3.left * 12;
						proj[4] = (GameObject)Instantiate (bossYellow, transform.position + Vector3.left * 1f, projectile.transform.rotation);
						proj[4].rigidbody.velocity = Vector3.left * 12;
						proj[5] = (GameObject)Instantiate (bossYellow, transform.position + Vector3.up * 1.5f + Vector3.left * 1f, projectile.transform.rotation);
						proj[5].rigidbody.velocity = Vector3.left * 12;
						proj[6] = (GameObject)Instantiate (bossYellow, transform.position + Vector3.up * 3f + Vector3.left * 1f, projectile.transform.rotation);
						proj[6].rigidbody.velocity = Vector3.left * 12 + Vector3.down * 2f;
						proj[7] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.up * 4.5f + Vector3.left * 1f, projectile.transform.rotation);
						proj[7].rigidbody.velocity = Vector3.left * 12 + Vector3.down * 4f;
						proj[8] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.up * 6f + Vector3.left * 1f, projectile.transform.rotation);
						proj[8].rigidbody.velocity = Vector3.left * 12 + Vector3.down * 6f;
						break;
					case 2:
						proj[0] = (GameObject)Instantiate (bossBlue, transform.position + Vector3.up * 6f + Vector3.left * 1f, projectile.transform.rotation);
						proj[0].rigidbody.velocity = Vector3.left * 12 + Vector3.up * 6f;
						proj[1] = (GameObject)Instantiate (bossBlue, transform.position + Vector3.down * 4.5f + Vector3.left * 1f, projectile.transform.rotation);
						proj[1].rigidbody.velocity = Vector3.left * 12 + Vector3.up * 4f;
						proj[2] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 3f + Vector3.left * 1f, projectile.transform.rotation);
						proj[2].rigidbody.velocity = Vector3.left * 12 + Vector3.up * 2f;
						proj[3] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 1.5f + Vector3.left * 1f, projectile.transform.rotation);
						proj[3].rigidbody.velocity = Vector3.left * 12;
						proj[4] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.left * 1f, projectile.transform.rotation);
						proj[4].rigidbody.velocity = Vector3.left * 12;
						proj[5] = (GameObject)Instantiate (bossBlue, transform.position + Vector3.up * 1.5f + Vector3.left * 1f, projectile.transform.rotation);
						proj[5].rigidbody.velocity = Vector3.left * 12;
						proj[6] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.up * 3f + Vector3.left * 1f, projectile.transform.rotation);
						proj[6].rigidbody.velocity = Vector3.left * 12 + Vector3.down * 2f;
						proj[7] = (GameObject)Instantiate (bossBlue, transform.position + Vector3.up * 4.5f + Vector3.left * 1f, projectile.transform.rotation);
						proj[7].rigidbody.velocity = Vector3.left * 12 + Vector3.down * 4f;
						proj[8] = (GameObject)Instantiate (bossBlue, transform.position + Vector3.up * 6f + Vector3.left * 1f, projectile.transform.rotation);
						proj[8].rigidbody.velocity = Vector3.left * 12 + Vector3.down * 6f;
						break;
					case 3:
						proj[0] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.up * 6f + Vector3.left * 1f, projectile.transform.rotation);
						proj[0].rigidbody.velocity = Vector3.left * 12 + Vector3.up * 6f;
						proj[1] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.down * 4.5f + Vector3.left * 1f, projectile.transform.rotation);
						proj[1].rigidbody.velocity = Vector3.left * 12 + Vector3.up * 4f;
						proj[2] = (GameObject)Instantiate (bossRed, transform.position + Vector3.down * 3f + Vector3.left * 1f, projectile.transform.rotation);
						proj[2].rigidbody.velocity = Vector3.left * 12 + Vector3.up * 2f;
						proj[3] = (GameObject)Instantiate (bossRed, transform.position + Vector3.down * 1.5f + Vector3.left * 1f, projectile.transform.rotation);
						proj[3].rigidbody.velocity = Vector3.left * 12;
						proj[4] = (GameObject)Instantiate (bossRed, transform.position + Vector3.left * 1f, projectile.transform.rotation);
						proj[4].rigidbody.velocity = Vector3.left * 12;
						proj[5] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.up * 1.5f + Vector3.left * 1f, projectile.transform.rotation);
						proj[5].rigidbody.velocity = Vector3.left * 12;
						proj[6] = (GameObject)Instantiate (bossRed, transform.position + Vector3.up * 3f + Vector3.left * 1f, projectile.transform.rotation);
						proj[6].rigidbody.velocity = Vector3.left * 12 + Vector3.down * 2f;
						proj[7] = (GameObject)Instantiate (bossRed, transform.position + Vector3.up * 4.5f + Vector3.left * 1f, projectile.transform.rotation);
						proj[7].rigidbody.velocity = Vector3.left * 12 + Vector3.down * 4f;
						proj[8] = (GameObject)Instantiate (bossWhite, transform.position + Vector3.up * 6f + Vector3.left * 1f, projectile.transform.rotation);
						proj[8].rigidbody.velocity = Vector3.left * 12 + Vector3.down * 6f;
						break;
				}
			}
		}
		//}
		if (currentCooldown % 720 == 0)
		{
			ability = 1;
		}
		currentCooldown = currentCooldown + 1;
	}
	
	public override void resetCooldown()
	{
	}
	
	public override void triggerDesperation()
	{
		desperation = 1;
		startup = 60;
	}
	
}
