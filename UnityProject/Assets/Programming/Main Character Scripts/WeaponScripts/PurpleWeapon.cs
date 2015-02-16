using UnityEngine;
using MainCharacter;
using System.Collections;
using System.Collections.Generic;

public class PurpleWeapon : MonoBehaviour {
	public GameObject mirvBullet;
	public float bulletSpeed;
	public float timeBeforeExplosion;
	public float damage;
	public float explosionSize;
	float purpleDist = .75f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	/*void OnCollisionEnter(Collision col){
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
		col.gameObject.BroadcastMessage ("OnHit", new WeaponDamage{tag=tag, damage=damage, hitLocation = col.contacts[0].point});
	}*/

	public void OnCollisionEnter(Collision col) {
		if((col.gameObject.layer == 10) /*|| (collider.gameObject.layer == 8)*/){
			Debug.Log("HIT OBJECT");
			Vector3 direction = (transform.position - col.transform.position).normalized;
			Debug.Log ("VECTOR MADE");
			Rigidbody theRigid;
			Transform temp = col.gameObject.transform;
			while (temp.parent != null)
				temp = temp.parent;
			theRigid = temp.rigidbody;
			Debug.Log (theRigid.ToString());
			theRigid.velocity = (direction * -20);
			Debug.Log ("FORCE!!!!");
		}
		/*if (col.gameObject.layer == LayerMask.NameToLayer("Enemy Bullet")) {
			// move the bullet away a bit
			col.gameObject.transform.position += col.gameObject.rigidbody.velocity;
			Vector3 direction = (transform.position - col.gameObject.transform.position).normalized;
			col.gameObject.rigidbody.velocity = direction * -10;
			// make sure the bullet disappears at some point
			Destroy(col.gameObject, 3);
		}*/
	}

	void addMirvScript(GameObject obj){
		var script = obj.AddComponent<MirvBullet>();
		script.bulletSpeed = bulletSpeed;
		script.timeBeforeExplosion = timeBeforeExplosion;
		script.explosionSize = explosionSize;
	}
}
