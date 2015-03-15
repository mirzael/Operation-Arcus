using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {
	public float cooldown;
	public GameObject projectile;
	public bool isBoss;
	public GameObject bossProjectile;
	public GameObject bossBlue;
	public GameObject bossRed;
	public GameObject bossYellow;

	public float bossPattern;
	public int pattern;
	float currentCooldown;
	public Wave bulletWave;
	EnemyDeath death;
	float maxHealth;
	float health;

	//health bar
	public GameObject bossHealth;
	public string bossName;
	private BossHealthBar healthBar;

	// Use this for initialization
	void Start () {
		if (isBoss == true) 
		{
			/*healthBar = ((GameObject)GameObject.Instantiate (bossHealth)).GetComponent<BossHealthBar>();
			healthBar.SetRelativeHealth (1.0f);
            healthBar.SetBossName(bossName);*/
			death = gameObject.GetComponent<EnemyDeath>();
			maxHealth = death.health;
			health = death.health;
		}
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
		case 8:
			bulletWave = gameObject.AddComponent<LevOneBossWave>();
			break;
		case 9:
			bulletWave = gameObject.AddComponent<LevTwoBossWave>();
			break;
		case 10:
			bulletWave = gameObject.AddComponent<LevThreeBossWave>();
			break;
		case 11:
			bulletWave = gameObject.AddComponent<LevFourBossWave>();
			break;
		case 12:
			bulletWave = gameObject.AddComponent<LevFiveBossWave>();
			break;
		case 13:
			bulletWave = gameObject.AddComponent<LevSixBossWave>();
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//currentCooldown -= Time.deltaTime;
		if (isBoss) {
			health = death.health;
			float percent = (float)health / (float)maxHealth;
			if (percent < 0.33) {
				bulletWave.triggerDesperation();
			}

		}
		if (currentCooldown == 0) 
		{
			bulletWave.resetCooldown ();
			currentCooldown = cooldown;
		}
		if (Time.timeScale != 0)
			currentCooldown = currentCooldown - 1;
	}

    private bool applicationIsQutting = false;

    protected void OnApplicationQuit()
    {
        applicationIsQutting = true;
    }

    protected void OnDestroy()
    {
        if(!applicationIsQutting && healthBar!=null)
        {
            healthBar.DestroySelf();
        }
    }
}
