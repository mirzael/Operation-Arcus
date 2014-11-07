using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PurpleWeapon : MonoBehaviour {
	public GameObject mirvBullet;
	public float bulletSpeed;
	public float timeBeforeExplosion;
	float purpleDist = .75f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Yellow") {
			var bullet0 = (GameObject)Instantiate(mirvBullet, col.contacts[0].point + (Vector3.up + Vector3.right) * purpleDist, mirvBullet.transform.rotation);
			var bullet1 = (GameObject)Instantiate(mirvBullet, col.contacts[0].point + (-Vector3.up + Vector3.right) * purpleDist, mirvBullet.transform.rotation);
			var bullet2 = (GameObject)Instantiate(mirvBullet, col.contacts[0].point + (Vector3.up - Vector3.right) * purpleDist, mirvBullet.transform.rotation);
			var bullet3 = (GameObject)Instantiate(mirvBullet, col.contacts[0].point + (-Vector3.up - Vector3.right) * purpleDist, mirvBullet.transform.rotation);
			bullet0.transform.Rotate(0,0,-45);
			bullet1.transform.Rotate(0,0,-135);
			bullet2.transform.Rotate(0,0,45);
			bullet3.transform.Rotate(0,0,135);

			addMirvScript(bullet0);
			addMirvScript(bullet1);
			addMirvScript(bullet2);
			addMirvScript(bullet3);
		}
	}

	void addMirvScript(GameObject obj){
		var script = obj.AddComponent<MirvBullet>();
		script.bulletSpeed = bulletSpeed;
		script.timeBeforeExplosion = timeBeforeExplosion;
	}
}
