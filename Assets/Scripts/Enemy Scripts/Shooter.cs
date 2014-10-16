﻿using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
	public float cooldown;
	public GameObject projectile;
	float offset;
	float currentCooldown;



	// Use this for initialization
	void Start () {
		currentCooldown = cooldown;
		offset = 5;
	}
	
	// Update is called once per frame
	void Update () {
		//currentCooldown -= Time.deltaTime;

		if (currentCooldown > 0) 
		{
			var proj = (GameObject)Instantiate (projectile, transform.position + Vector3.left * 2, transform.rotation);
			proj.rigidbody.velocity = Vector3.left * 15 + Vector3.down * offset;
			int aVal = (int) currentCooldown % 3;
			switch (aVal)
			{
			case 0:
				offset = 5;
				break;
			case 1:
				offset = 0;
				break;
			case 2:
				offset = -5;
				break;
			}
		}
		currentCooldown = currentCooldown - 1;
		if (currentCooldown < (cooldown * -2))
			currentCooldown = cooldown;
	}
}
