using UnityEngine;
using System.Collections;

public class Wave : MonoBehaviour {

	float cooldown;
	public GameObject projectile;
	float currentCooldown;


	// Use this for initialization
	void Start () {
		projectile = gameObject.GetComponent<Shooter> ().projectile;
		currentCooldown = 20;
		cooldown = 100;
	}
	
	// Update is called once per frame
	void Update () {
		if (currentCooldown < cooldown) 
			{
			var proj = new GameObject();
			currentCooldown = currentCooldown + 1;
			int aVal = (int) currentCooldown % 10;
			switch (aVal)
				{
				case 0:
					proj = (GameObject)Instantiate (projectile, gameObject.transform.position + Vector3.down * 2, transform.rotation);
					proj.rigidbody.velocity = Vector3.down * 25 + Vector3.left * 5;
					break;
				case 1:
					proj = (GameObject)Instantiate (projectile, gameObject.transform.position + Vector3.down * 2, transform.rotation);
					proj.rigidbody.velocity = Vector3.down * 25 + Vector3.left;
					break;
				case 2:
					proj = (GameObject)Instantiate (projectile, gameObject.transform.position + Vector3.down * 2, transform.rotation);
					proj.rigidbody.velocity = Vector3.down * 25 + Vector3.left * -5;
					break;
				}
			}
	}

	public virtual void resetCooldown()
		{
		currentCooldown = 0;
		}
}
