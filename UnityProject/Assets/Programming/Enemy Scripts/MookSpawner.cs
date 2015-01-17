using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using MainCharacter;

public class MookSpawner : MonoBehaviour {
	public List<GameObject> mooks;
	RotatingList<GameObject> myMooks;
	public float cooldown;
	float origCooldown;

	// Use this for initialization
	void Start () {
		origCooldown = cooldown;
		myMooks = new RotatingList<GameObject> (mooks);

	}
	
	// Update is called once per frame
	void Update () {
		cooldown -= Time.deltaTime;

		if (cooldown <= 0) {
			var ship = myMooks.Next();
			switch(Random.Range(0,2)){
				case 0:
					var mook = (GameObject) Instantiate (ship, transform.position + Vector3.up * 4 + Vector3.left, ship.transform.rotation);
					var behav = mook.AddComponent<BossSpawnBehavior>();
					if(mook.tag == "Blue"){
						mook.transform.localScale = new Vector3(1f,1f,1f);
						behav.multiplier = 30;
					}else{
						mook.transform.localScale = new Vector3(0.01f ,0.01f ,0.01f);
					}
					var movement = (EnemyMovement)mook.GetComponent(typeof(EnemyMovement));
					movement.pattern = 1;
					break;
				case 1:
					var mook2 = (GameObject) Instantiate (ship, transform.position + Vector3.up * 4 + Vector3.right, ship.transform.rotation);
					var behav2 = mook2.AddComponent<BossSpawnBehavior>();

					if(mook2.tag == "Blue"){
						mook2.transform.localScale = new Vector3(1,1,1);
						behav2.multiplier = 30;
					}else{
						mook2.transform.localScale = new Vector3(0.01f ,0.01f ,0.01f);
					}
					var movement2 = (EnemyMovement)mook2.GetComponent(typeof(EnemyMovement));
					movement2.pattern = 2;
					break;
			}
			cooldown = origCooldown;
		}
	}
}
