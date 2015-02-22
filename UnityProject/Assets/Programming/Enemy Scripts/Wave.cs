using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {

	public float cooldown;
	public GameObject projectile;
	public GameObject bossProj;
	public bool isBoss;
	public float currentCooldown;


	// Use this for initialization
	public virtual void Start () {
		var shooter = gameObject.GetComponent<Shooter> ();
		projectile = shooter.projectile;
		bossProj = shooter.bossProjectile;
		isBoss = shooter.isBoss;
		currentCooldown = 20;
		cooldown = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentCooldown < cooldown) 
		{
			GameObject proj;
			if(Time.timeScale != 0) currentCooldown = currentCooldown + 1;
			int aVal = (int) currentCooldown % 10;
			switch (aVal)
			{
				case 0:
					proj = (GameObject)InstantiateBullet (projectile, gameObject.transform.position + Vector3.down * 2, projectile.transform.rotation);
					proj.rigidbody.velocity = Vector3.down * 25 + Vector3.left * 5;
					break;
				case 1:
					proj = (GameObject)InstantiateBullet (projectile, gameObject.transform.position + Vector3.down * 2, projectile.transform.rotation);
					proj.rigidbody.velocity = Vector3.down * 25 + Vector3.left;
					break;
				case 2:
					proj = (GameObject)InstantiateBullet (projectile, gameObject.transform.position + Vector3.down * 2, projectile.transform.rotation);
					proj.rigidbody.velocity = Vector3.down * 25 + Vector3.left * -5;
					break;
			}
		}
	}

	public virtual void resetCooldown()
	{
		currentCooldown = 0;
	}

	public virtual void triggerDesperation()
	{
		currentCooldown = 0;
	}

	public GameObject InstantiateBullet(GameObject projectile, Vector3 location, Quaternion rotation){
		return (GameObject)Instantiate(projectile, location, rotation);
	}
}
