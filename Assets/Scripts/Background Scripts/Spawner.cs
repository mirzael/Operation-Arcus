using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using HelperClasses;

public class Spawner : MonoBehaviour {
	public List<GameObject> enemyToSpawn;
	public List<GameObject> projectilesEnemySpawns;
	public float spawnRate;
	RotatingList<GameObject> copyEnemyToSpawn;
	float currentRate;

	// Use this for initialization
	void Start () {
		currentRate = spawnRate;
		copyEnemyToSpawn = new RotatingList<GameObject> (enemyToSpawn);
	}
	
	// Update is called once per frame
	void Update () {
		currentRate -= Time.deltaTime;

		if (currentRate <= 0) {
			currentRate = spawnRate;
			GameObject spawn = copyEnemyToSpawn.Next();
			var ship = (GameObject)Instantiate(spawn, transform.position + Vector3.left * 2, spawn.transform.rotation);
			ship.rigidbody.velocity = Vector3.left * 10;
		}
	}
}
