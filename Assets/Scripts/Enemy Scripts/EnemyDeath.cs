﻿using UnityEngine;
using MainCharacter;
using System.Collections;
using System.Collections.Generic;

public class EnemyDeath : MonoBehaviour {
	public float health;
	public GameObject explosion;
	const float SPHERE_DURATION = 0.5f;
	const float SPHERE_RADIUS = 1f;
	PointMaster points;

	// Use this for initialization
	void Start () {
		points = Component.FindObjectOfType<PointMaster> ();
	}

	void OnHit(WeaponDamage wep){
		points.Notify (new DeathInfo{ shipTag = gameObject.tag, bulletTag = wep.tag, shipPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z)});
		health -= wep.damage;
		Debug.Log ("Hit enemy. Health Remaining: " + health);
		if (health <= 0) {
			if(animation != null) animation.Stop();
			rigidbody.isKinematic = false;
			GetComponent<Shooter> ().enabled = false;
			GetComponent<Wave>().enabled = false;
			if (explosion != null) {
				Instantiate(explosion, transform.position, transform.rotation);
				explosion = null; // make sure it occurs on the first hit
			}
			Destroy (gameObject, 3f);
		}


	}
}
