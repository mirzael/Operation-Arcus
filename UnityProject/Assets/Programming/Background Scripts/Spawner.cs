using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using MainCharacter;
using System.Text;
using System.IO;

public class Spawner : MonoBehaviour {
	public List<GameObject> enemyToSpawn;
	public float spawnRate;
	
	private Queue<float> enemySpawnTimes;
	private Queue<string> enemyDetails;
    private Queue<float> waveTimes = new Queue<float>();

    private float? curWaveTime;

	private float levelTimeCounter;
	private float lastSpawnTime;
	public int level = 1;

    public bool showTimer = false;

	void OnGUI () {
        if(showTimer)
        {
            GUI.Label(new Rect(0, 0, 100, 50), levelTimeCounter.ToString());
        }
	}

	// Use this for initialization
	public void Start () {
		levelTimeCounter = 0;
		
		Debug.Log("Loading level " + level);
		
		if (!Load("level" + level)) {
			Debug.Log("Failed to read spawn data for level " + level);
		}
		
		GameObject.Find("Background").AddComponent<ScrollBackground>().numSeconds = lastSpawnTime;
		
		Debug.Log("Level has " + enemyDetails.Count + " enemies");

        BackgroundUI.Instance.AddGameEndEvent(delegate()
        {
            gameObject.SetActive(false);
        });
	}

	// source: http://answers.unity3d.com/questions/279750/loading-data-from-a-txt-file-c.html
	private bool Load(string fileName) {
		enemySpawnTimes = new Queue<float>();
		enemyDetails = new Queue<String>();
		lastSpawnTime = 0;
		
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
						if (timeToAppear > lastSpawnTime) {
							lastSpawnTime = timeToAppear;
						}
						enemyDetails.Enqueue(otherDetails);
					}
                    else if (entries.Length==3)
                    {
                        if (entries[0].Trim().ToLower().Equals("wave"))
                        {
                            float timeToAppear = float.Parse(entries[1]);
                            Debug.Log("there is a wave here");
                            waveTimes.Enqueue(timeToAppear);
                        }
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
		levelTimeCounter += Time.deltaTime;
		
        //get through the waves
        while (waveTimes.Count > 0 && waveTimes.Peek() <= levelTimeCounter)
        {
            waveTimes.Dequeue();
        }

		// there is something to spawn, and the current object should have been spawned by now
		while (enemySpawnTimes.Count > 0 && enemySpawnTimes.Peek() <= levelTimeCounter) {
			enemySpawnTimes.Dequeue();
			string[] details = enemyDetails.Dequeue().Split(',');
			
			GameObject spawn = null;
			var enemy = details[0].IndexOf("GOB");
			var enemyNum = int.MaxValue;
			if (enemy != -1) {
				try{
					enemyNum = Convert.ToInt32 (details[0].Substring(enemy+3));
					spawn = enemyToSpawn[enemyNum];
				}catch(FormatException){
					Debug.LogError(details[0] + " is not a valid gameobject string in the level script. SPAWNING FIRST ENEMY IN LIST");
					spawn = enemyToSpawn[0];
				}
			} else {
				Debug.LogError("Cannot find enemy " + details[0] + ". SPAWNING FIRST ENEMY IN LIST");
				spawn = enemyToSpawn[0];
			}

			if(enemyNum >= enemyToSpawn.Count){
				Debug.LogError("Attempted to spawn GameObject that is not in list. SPAWNING FIRST ENEMY IN LIST");
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
			var multi = gameObject.GetComponent<MultiplierScript>();
            EnemyDeath enemyDeath = ship.GetComponent<EnemyDeath>();
			if(enemyDeath!=null)
            {
                enemyDeath.health *= multi.enemyHealthMultiplier;
            }
            Wave wave = ship.GetComponent<Wave>();
            if(wave!=null)
            {
                //should this ever be null?
			    wave.cooldown *= multi.enemyCooldownMultiplier;
            }
			if (enemyDetails.Count == 0) {
				GameObject.Find("Main Camera").AddComponent<EndLevel>();
			}
		}
	}
}
