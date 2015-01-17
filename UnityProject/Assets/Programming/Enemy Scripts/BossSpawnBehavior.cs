using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MainCharacter;

public class BossSpawnBehavior : MonoBehaviour {
	public float scale;
	public float multiplier = 1f;
	public float cooldown = 0f;

	Wave wave;
	Shooter shooter;
	// Use this for initialization
	void Start () {
		wave = (Wave)gameObject.GetComponent<Wave> ();
		shooter = (Shooter)gameObject.GetComponent<Shooter> ();
		wave.enabled = false;
		shooter.enabled = false;
		scale = transform.localScale.x;
	}
	
	// Update is called once per frame
	void Update () {
		cooldown += Time.deltaTime;
		if (cooldown >= 0.5) {
			wave.enabled = true;
			shooter.enabled = true;
			Destroy(this);
		}
		if(Time.timeScale != 0) scale += cooldown/20 * multiplier;
		transform.localScale = new Vector3 (scale, scale, scale);
	}
}
