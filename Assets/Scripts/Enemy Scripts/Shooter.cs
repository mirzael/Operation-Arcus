using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
	public float cooldown;
	public GameObject projectile;
	public int pattern;
	float currentCooldown;
	Wave bulletWave;


	// Use this for initialization
	void Start () {
		currentCooldown = cooldown;
		int wave = Random.Range (0, 1);
		switch (pattern)
		{
		case 0:
			bulletWave = gameObject.AddComponent<Wave> ();
			break;
		case 1:
			bulletWave = gameObject.AddComponent<TrackWave> ();
			break;
		case 2:
			bulletWave = gameObject.AddComponent<NetWave> ();
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//currentCooldown -= Time.deltaTime;

		if (currentCooldown == 0) 
		{
			bulletWave.resetCooldown ();
			currentCooldown = cooldown;
		}
		currentCooldown = currentCooldown - 1;
	}
}
