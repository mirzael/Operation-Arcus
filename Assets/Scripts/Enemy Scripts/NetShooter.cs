using UnityEngine;
using System.Collections;

public class NetShooter : MonoBehaviour {
	public float cooldown;
	public GameObject projectile;	
	float currentCooldown;
	
	
	// Use this for initialization
	void Start () {
		currentCooldown = cooldown;
	}
	
	// Update is called once per frame
	void Update () {
		//currentCooldown -= Time.deltaTime;
		
		if (currentCooldown > 0) 
		{
			GameObject[] proj = new GameObject[5];
			int aVal = (int) currentCooldown % 10;
			switch (aVal)
			{
			case 0:
				proj[0] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, projectile.transform.rotation);
				proj[0].rigidbody.velocity = Vector3.left * 15;
				break;
			case 8:
				proj[0] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, projectile.transform.rotation);
				proj[0].rigidbody.velocity = Vector3.left * 15 + Vector3.down * .2f;
				proj[1] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, projectile.transform.rotation);
				proj[1].rigidbody.velocity = Vector3.left * 15 + Vector3.down * -.2f;
				break;
			case 6:
				proj[0] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, projectile.transform.rotation);
				proj[0].rigidbody.velocity = Vector3.left * 15 + Vector3.down * .4f;
				proj[1] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, projectile.transform.rotation);
				proj[1].rigidbody.velocity = Vector3.left * 15;
				proj[2] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, projectile.transform.rotation);
				proj[2].rigidbody.velocity = Vector3.left * 15 + Vector3.down * -.4f;
				break;
			case 4:
				proj[0] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, projectile.transform.rotation);
				proj[0].rigidbody.velocity = Vector3.left * 15 + Vector3.down * .6f;
				proj[1] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, projectile.transform.rotation);
				proj[1].rigidbody.velocity = Vector3.left * 15 + Vector3.down * .2f;
				proj[2] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, projectile.transform.rotation);
				proj[2].rigidbody.velocity = Vector3.left * 15 + Vector3.down * -.2f;
				proj[3] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, projectile.transform.rotation);
				proj[3].rigidbody.velocity = Vector3.left * 15 + Vector3.down * -.6f;
				break;
			case 2:
				proj[0] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, projectile.transform.rotation);
				proj[0].rigidbody.velocity = Vector3.left * 15 + Vector3.down * .8f;
				proj[1] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, projectile.transform.rotation);
				proj[1].rigidbody.velocity = Vector3.left * 15 + Vector3.down * .4f;
				proj[2] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, projectile.transform.rotation);
				proj[2].rigidbody.velocity = Vector3.left * 15;
				proj[3] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, projectile.transform.rotation);
				proj[3].rigidbody.velocity = Vector3.left * 15 + Vector3.down * -.4f;
				proj[4] = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, projectile.transform.rotation);
				proj[4].rigidbody.velocity = Vector3.left * 15 + Vector3.down * -.8f;
				break;
			}
		}
		currentCooldown = currentCooldown - 1;
		if (currentCooldown < (cooldown * -9))
			currentCooldown = cooldown;
	}
}
