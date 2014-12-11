using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
	public float cooldown;
	public GameObject projectile;
	public bool isBoss;
	public GameObject bossProjectile;
	public float bossBulletChance;
	public int pattern;
	float currentCooldown;
	public Wave bulletWave;


	// Use this for initialization
	void Start () {
		currentCooldown = cooldown;
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
		case 3:
			bulletWave = gameObject.AddComponent<TrackNetWave> ();
			break;
		case 4:
			bulletWave = gameObject.AddComponent<MachineGunWave> ();
			break;
		case 5:
			bulletWave = gameObject.AddComponent<DualPulseWave> ();
			break;
		case 6:
			bulletWave = gameObject.AddComponent<CascadeWave> ();
			break;
		case 7:
			bulletWave = gameObject.AddComponent<CircleWave> ();
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
		if (Time.timeScale != 0)
			currentCooldown = currentCooldown - 1;
	}
}
