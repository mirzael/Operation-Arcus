using UnityEngine;
using System.Collections;

public class LevOneBossWave : Wave {
	int desperation;
	int ability;
	int activeRed;
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
			if (ability == 0)
			{
				Debug.Log ("STANDARD!");
				if (currentCooldown % 60 == 0)
				{
					// Left "blade"
					activeBullet = bossYellow;
					GameObject[] proj = new GameObject[16];

					proj [0] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 2f + Vector3.left * 5f, projectile.transform.rotation);
					proj [0].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 10f;

					proj [1] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 2.5f + Vector3.left * 4.5f, projectile.transform.rotation);
					proj [1].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 8.5f;

					proj [2] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 3f + Vector3.left * 4f, projectile.transform.rotation);
					proj [2].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 7f;

					proj [3] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 3.5f + Vector3.left * 3.5f, projectile.transform.rotation);
					proj [3].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 5.5f;

					proj [4] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 4f + Vector3.left * 3f, projectile.transform.rotation);
					proj [4].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 4f;

					proj [5] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 4.5f + Vector3.left * 2f, projectile.transform.rotation);
					proj [5].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 2.5f;

					proj [6] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.left * 1.5f, projectile.transform.rotation);
					proj [6].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 1f;

					proj [7] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5.5f + Vector3.left * 1f, projectile.transform.rotation);
					proj [7].rigidbody.velocity = Vector3.down * 15;

					// Right "blade"
					activeBullet = bossBlue;
					proj [8] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 2 + Vector3.right * 5f, projectile.transform.rotation);
					proj [8].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 10f;

					proj [9] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 2.5f + Vector3.right * 4.5f, projectile.transform.rotation);
					proj [9].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 8.5f;

					proj [10] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 3f + Vector3.right * 4f, projectile.transform.rotation);
					proj [10].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 7f;

					proj [11] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 3.5f + Vector3.right * 3.5f, projectile.transform.rotation);
					proj [11].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 5.5f;

					proj [12] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 4f + Vector3.right * 3f, projectile.transform.rotation);
					proj [12].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 4f;

					proj [13] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 4.5f + Vector3.right * 2f, projectile.transform.rotation);
					proj [13].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 2.5f;

					proj [14] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.right * 1.5f, projectile.transform.rotation);
					proj [14].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 1f;

					proj [15] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5.5f + Vector3.right * 1f, projectile.transform.rotation);
					proj [15].rigidbody.velocity = Vector3.down * 15;
				}
				else if (currentCooldown % 30 == 0)
				{
					//Center Pulse
					activeBullet = bossRed;
					GameObject[] proj = new GameObject[8];
					
					proj [0] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.left * 1f, projectile.transform.rotation);
					proj [0].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 5.5f;
					
					proj [1] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.left * .7f, projectile.transform.rotation);
					proj [1].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 4f;
					
					proj [2] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.left * .4f, projectile.transform.rotation);
					proj [2].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 2.5f;
					
					proj [3] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.left * .1f, projectile.transform.rotation);
					proj [3].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 1f;
					
					proj [4] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.right * .1f, projectile.transform.rotation);
					proj [4].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 1f;
					
					proj [5] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.right * .4f, projectile.transform.rotation);
					proj [5].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 2.5f;
					
					proj [6] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.right * .7f, projectile.transform.rotation);
					proj [6].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 4f;
					
					proj [7] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.right * 1f, projectile.transform.rotation);
					proj [7].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 5.5f;
				}
			}
			else //ability activate!
			{
				Debug.Log("ABILITY!");
				if (currentCooldown % 60 == 0)
				{
					// Left "blade"
					switch (activeBullet.tag)
					{
						case "Blue":
							activeBullet = bossYellow;
							break;
						case "Yellow":
							activeBullet = bossRed;
							break;
						case "Red":
							activeBullet = bossBlue;
							break;
					}

					GameObject[] proj = new GameObject[16];
					
					proj [0] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 2f + Vector3.left * 5f, projectile.transform.rotation);
					proj [0].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 10f;
					
					proj [1] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 2.5f + Vector3.left * 4.5f, projectile.transform.rotation);
					proj [1].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 8.5f;
					
					proj [2] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 3f + Vector3.left * 4f, projectile.transform.rotation);
					proj [2].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 7f;
					
					proj [3] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 3.5f + Vector3.left * 3.5f, projectile.transform.rotation);
					proj [3].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 5.5f;
					
					proj [4] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 4f + Vector3.left * 3f, projectile.transform.rotation);
					proj [4].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 4f;
					
					proj [5] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 4.5f + Vector3.left * 2f, projectile.transform.rotation);
					proj [5].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 2.5f;
					
					proj [6] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.left * 1.5f, projectile.transform.rotation);
					proj [6].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 1f;
					
					proj [7] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5.5f + Vector3.left * 1f, projectile.transform.rotation);
					proj [7].rigidbody.velocity = Vector3.down * 15;
					
					// Right "blade"
					proj [8] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 2 + Vector3.right * 5f, projectile.transform.rotation);
					proj [8].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 10f;
					
					proj [9] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 2.5f + Vector3.right * 4.5f, projectile.transform.rotation);
					proj [9].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 8.5f;
					
					proj [10] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 3f + Vector3.right * 4f, projectile.transform.rotation);
					proj [10].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 7f;
					
					proj [11] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 3.5f + Vector3.right * 3.5f, projectile.transform.rotation);
					proj [11].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 5.5f;
					
					proj [12] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 4f + Vector3.right * 3f, projectile.transform.rotation);
					proj [12].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 4f;
					
					proj [13] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 4.5f + Vector3.right * 2f, projectile.transform.rotation);
					proj [13].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 2.5f;
					
					proj [14] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.right * 1.5f, projectile.transform.rotation);
					proj [14].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 1f;
					
					proj [15] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5.5f + Vector3.right * 1f, projectile.transform.rotation);
					proj [15].rigidbody.velocity = Vector3.down * 15;
				}
				else if (currentCooldown % 30 == 0)
				{
					//Center Pulse
					GameObject[] proj = new GameObject[8];
					
					proj [0] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.left * 1f, projectile.transform.rotation);
					proj [0].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 5.5f;
					
					proj [1] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.left * .7f, projectile.transform.rotation);
					proj [1].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 4f;
					
					proj [2] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.left * .4f, projectile.transform.rotation);
					proj [2].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 2.5f;
					
					proj [3] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.left * .1f, projectile.transform.rotation);
					proj [3].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 1f;
					
					proj [4] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.right * .1f, projectile.transform.rotation);
					proj [4].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 1f;
					
					proj [5] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.right * .4f, projectile.transform.rotation);
					proj [5].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 2.5f;
					
					proj [6] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.right * .7f, projectile.transform.rotation);
					proj [6].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 4f;
				
					proj [7] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.right * 1f, projectile.transform.rotation);
					proj [7].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 5.5f;
				}
			}
			if (currentCooldown % 240 == 0)
				ability = 0;
		} 
		else //DESPERATION MODE!
		{
			Debug.Log ("DESPERATION!");
			int color = Random.Range(1,4);
			if (currentCooldown % 60 == 0)
			{
				// Left "blade"
				if (color == 1)
					activeBullet = bossWhite;
				else
					activeBullet = bossYellow;
				GameObject[] proj = new GameObject[16];
				
				proj [0] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 2f + Vector3.left * 5f, projectile.transform.rotation);
				proj [0].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 10f;
				
				proj [1] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 2.5f + Vector3.left * 4.5f, projectile.transform.rotation);
				proj [1].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 8.5f;
				
				proj [2] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 3f + Vector3.left * 4f, projectile.transform.rotation);
				proj [2].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 7f;
				
				proj [3] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 3.5f + Vector3.left * 3.5f, projectile.transform.rotation);
				proj [3].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 5.5f;
				
				proj [4] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 4f + Vector3.left * 3f, projectile.transform.rotation);
				proj [4].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 4f;
				
				proj [5] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 4.5f + Vector3.left * 2f, projectile.transform.rotation);
				proj [5].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 2.5f;
				
				proj [6] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.left * 1.5f, projectile.transform.rotation);
				proj [6].rigidbody.velocity = Vector3.down * 15 + Vector3.left * 1f;
				
				proj [7] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5.5f + Vector3.left * 1f, projectile.transform.rotation);
				proj [7].rigidbody.velocity = Vector3.down * 15;
				
				// Right "blade"
				if (color == 2)
					activeBullet = bossWhite;
				else
					activeBullet = bossBlue;
				proj [8] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 2 + Vector3.right * 5f, projectile.transform.rotation);
				proj [8].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 10f;
				
				proj [9] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 2.5f + Vector3.right * 4.5f, projectile.transform.rotation);
				proj [9].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 8.5f;
				
				proj [10] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 3f + Vector3.right * 4f, projectile.transform.rotation);
				proj [10].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 7f;
				
				proj [11] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 3.5f + Vector3.right * 3.5f, projectile.transform.rotation);
				proj [11].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 5.5f;
				
				proj [12] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 4f + Vector3.right * 3f, projectile.transform.rotation);
				proj [12].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 4f;
				
				proj [13] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 4.5f + Vector3.right * 2f, projectile.transform.rotation);
				proj [13].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 2.5f;
				
				proj [14] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5f + Vector3.right * 1.5f, projectile.transform.rotation);
				proj [14].rigidbody.velocity = Vector3.down * 15 + Vector3.right * 1f;
				
				proj [15] = (GameObject)Instantiate (activeBullet, transform.position + Vector3.down * 5.5f + Vector3.right * 1f, projectile.transform.rotation);
				proj [15].rigidbody.velocity = Vector3.down * 15;
			}
			// Beam section
			if ((activeRed == 1) && (currentCooldown % 60 == 0))
				activeRed = 0;
			if (color == 3)
			{
				if (currentCooldown % 60 == 0)
					activeRed = 1;
			}
			if (activeRed == 1)
				activeBullet = bossWhite;
			else
				activeBullet = bossRed;
			int aVal = (int)(currentCooldown) % 42;
			float offset;
			int side = 0;

			if (aVal >= 21)
					side = -1;
			else 
					side = 1;

			offset = ((currentCooldown % 42) - 10) / 10 * side;
			GameObject projl, projr, projc;

			projc = (GameObject)InstantiateBullet (activeBullet, transform.position + Vector3.down * 2 + Vector3.right * offset, projectile.transform.rotation);
			projc.rigidbody.velocity = Vector3.down * 30;

			projl = (GameObject)InstantiateBullet (activeBullet, transform.position + Vector3.down * 1.8f + Vector3.left * 1f, projectile.transform.rotation);
			projl.rigidbody.velocity = Vector3.down * 30;

			projr = (GameObject)InstantiateBullet (activeBullet, transform.position + Vector3.down * 1.8f + Vector3.right * 1f, projectile.transform.rotation);
			projr.rigidbody.velocity = Vector3.down * 30;
		}
		//}
		if (currentCooldown % 720 == 0)
			ability = 1;
		currentCooldown = currentCooldown + 1;
	}
	
	public override void resetCooldown()
	{
	}

	public override void triggerDesperation()
	{
		desperation = 1;
	}
	
}
