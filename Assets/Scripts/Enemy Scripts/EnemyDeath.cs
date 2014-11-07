using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDeath : MonoBehaviour {
	const float SPHERE_DURATION = 0.5f;
	const float SPHERE_RADIUS = 1f;
	PointMaster points;

	// Use this for initialization
	void Start () {
		points = Component.FindObjectOfType<PointMaster> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision col){
		//AOE, BABY
		if (col.gameObject.tag != "Purple" && col.gameObject.tag != "Green" && col.gameObject.tag != "Player") {
			Destroy (col.gameObject);
		}
		points.Notify (new DeathInfo{ shipTag = gameObject.tag, bulletTag = col.gameObject.tag, shipPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z)} );
		Destroy (gameObject);
	}
}
