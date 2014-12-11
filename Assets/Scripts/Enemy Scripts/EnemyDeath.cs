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
	EnemyMovement movement;
	static AudioClip boomSound;
	bool bossCheck = false;

	// Use this for initialization
	void Start () {
		points = Component.FindObjectOfType<PointMaster> ();
		movement = gameObject.GetComponent<EnemyMovement> ();
		boomSound = (AudioClip)Resources.Load ("Sounds/Enemyboom", typeof(AudioClip));

	}

	void OnHit(WeaponDamage wep){
		points.Notify (new DeathInfo{ shipTag = gameObject.tag, bulletTag = wep.tag, shipPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z)});
		health -= wep.damage;
		if (health <= 0) {
			if(!gameObject.GetComponent<Shooter>().isBoss) { gameObject.layer = LayerMask.NameToLayer("Enemy Bullet"); }
			if(animation != null) animation.Stop();
			rigidbody.isKinematic = false;
			GetComponent<Shooter> ().enabled = false;
			GetComponent<Wave>().enabled = false;

			if(gameObject.GetComponent<Shooter>().isBoss && bossCheck == false) 
			{ 
				bossCheck = true;
				Invoke ("explodeBoss", .5f);
				Invoke ("explodeBoss", 1f);
				Invoke ("explodeBoss", 1.5f);
				Invoke ("explodeBoss", 2f);
				Invoke ("explodeBoss", 2.5f);
				Invoke ("explodeBoss", 3f);
			}
			if (explosion != null && !gameObject.GetComponent<Shooter>().isBoss) {
				Instantiate(explosion, transform.position, transform.rotation);
				explosion = null; // make sure it occurs on the first hit
			}
			movement.enabled = false;

			rigidbody.AddForce(new Vector3(Random.Range(-500,500), Random.Range(-350,-200), Random.Range (-250, -100)));
			Destroy (gameObject, 3f);
			audio.PlayOneShot(boomSound);
		}


	}

	void explodeBoss()
	{
		GameObject bossExp = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
		bossExp.particleEmitter.minSize = 2f;
		bossExp.particleEmitter.maxSize = 15f;
	}
}
