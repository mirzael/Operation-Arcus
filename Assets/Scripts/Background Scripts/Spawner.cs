using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MainCharacter;
using System.Text;
using System.IO;

public class Spawner : MonoBehaviour {
	public List<GameObject> enemyToSpawn;
	public List<GameObject> projectilesEnemySpawns;
	public float spawnRate;
	RotatingList<GameObject> copyEnemyToSpawn;
	float currentRate;
	
	private Queue<float> enemySpawnTimes;
	private Queue<string> enemyDetails;
	private float levelTimeCounter;
	public int level = 1;
	public const int MAX_LEVELS = 3;

	// Use this for initialization
	public void Start () {
		currentRate = spawnRate;
		copyEnemyToSpawn = new RotatingList<GameObject> (enemyToSpawn);
		levelTimeCounter = 0;
		
		if (level > MAX_LEVELS) {
			Debug.Log("You won the game!");
			Application.Quit();
			return;
		}
		
		Debug.Log("Loading level " + level);
		
		if (!Load("level" + level)) {
			Debug.Log("Failed to read spawn data for level " + level);
		}
		
		Debug.Log("Level has " + enemyDetails.Count + " enemies");
	}

	// source: http://answers.unity3d.com/questions/279750/loading-data-from-a-txt-file-c.html
	private bool Load(string fileName) {
		enemySpawnTimes = new Queue<float>();
		enemyDetails = new Queue<String>();
		
		TextAsset levelData = (TextAsset)Resources.Load(fileName, typeof(TextAsset));
		StringReader reader = new StringReader(levelData.text);
		
		// Handle any problems that might arise when reading the text
		try {
			string line;
			
			// While there's lines left in the text file, do this:
			do {
				line = reader.ReadLine();
				
				// don't try to do anything if we didn't get any text
				// also ignore lines starting with a #
				if (line != null && !line.Trim().Equals("") && line[0] != '#') {
					string[] entries = line.Split(',');
					if (entries.Length == 6) {
						// format: enemyType, whenToAppear, xPos, yPos, movementPattern, stop
						float timeToAppear = float.Parse(entries[1]);
						string otherDetails = entries[0] + "," + entries[2] + "," + entries[3] + "," + entries[4] + "," + entries[5];
						enemySpawnTimes.Enqueue(timeToAppear);
						enemyDetails.Enqueue(otherDetails);
					}
				}
			} while (line != null);
		}
		
		// If anything broke in the try block, we throw an exception with information
		// on what didn't work
		catch (Exception e) {
			Console.WriteLine("{0}\n", e.Message);
			return false;
		}
		
		return true;
	}

	// Update is called once per frame
	void Update () {
		/*
		currentRate -= Time.deltaTime;

		if (currentRate <= 0) {
			currentRate = spawnRate;
			GameObject spawn = copyEnemyToSpawn.Next();
			GameObject ship = (GameObject)Instantiate(spawn, transform.position + Vector3.down * 2, spawn.transform.rotation);
			//ship.rigidbody.velocity = Vector3.down * 10 + Vector3.left * Random.Range(-10f, 10f);
		}
		/*/
		levelTimeCounter += Time.deltaTime;
		
		// there is something to spawn, and the current object should have been spawned by now
		while (enemySpawnTimes.Count > 0 && enemySpawnTimes.Peek() <= levelTimeCounter) {
			enemySpawnTimes.Dequeue();
			string[] details = enemyDetails.Dequeue().Split(',');
			
			GameObject spawn;
			if (details[0].Equals("BMachineGun")) {
				spawn = enemyToSpawn[0];
			} else if (details[0].Equals("YDualPulse")) {
				spawn = enemyToSpawn[1];
			} else if (details[0].Equals("RCascade")) {
				spawn = enemyToSpawn[2];
			} else {
				spawn = enemyToSpawn[0];
			}
			
			float xPos = float.Parse(details[1]);
			float yPos = float.Parse(details[2]);
			int movementPattern = int.Parse(details[3]);
			bool stops;
			if (int.Parse(details[4]) == 1)
				stops = true;
			else
				stops = false;
			
			GameObject ship = (GameObject)Instantiate(spawn, transform.position + Vector3.down * (2 + yPos) + Vector3.right * xPos, spawn.transform.rotation);
			ship.GetComponent<EnemyMovement>().pattern = movementPattern;
			ship.GetComponent<EnemyMovement>().stops = stops;
			if (enemyDetails.Count == 0) {
				GameObject.Find("Main Camera").AddComponent<EndLevel>();
			}
		}
		
		//*/
	}
}
