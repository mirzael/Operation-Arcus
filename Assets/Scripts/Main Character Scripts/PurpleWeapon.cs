using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PurpleWeapon : MonoBehaviour {

	List<GameObject> bullets = new List<GameObject>();	
	const float SPHERE_DURATION = 0.5f;
	const float SPHERE_RADIUS = 1f;
	public GameObject mirvBullet;
	public float bulletSpeed;
	public float timeBeforeExplosion;
	Material explosionMat;
	float purpleDist = .75f;

	// Use this for initialization
	void Start () {
		explosionMat = (Material)Resources.Load ("Materials/AoeBlasts/PurpleBlast", typeof(Material));
	}
	
	// Update is called once per frame
	void Update () {
		timeBeforeExplosion -= Time.deltaTime;

		if (timeBeforeExplosion >= 0) {
			foreach (GameObject obj in bullets) {
					obj.transform.position += obj.transform.up * bulletSpeed / 100;
			}
		} else {
			for(int i = 0; i < bullets.Count; i++){
				var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				sphere.renderer.material = explosionMat;
				sphere.transform.position = bullets[i].transform.position;
				sphere.transform.localScale = new Vector3(SPHERE_RADIUS,SPHERE_RADIUS,SPHERE_RADIUS);
				sphere.tag = "Purple";
				sphere.layer = LayerMask.NameToLayer("Character Bullet");
				Destroy (sphere, SPHERE_DURATION);
				Destroy(bullets[i]);
				bullets.RemoveAt(i);
				i--;
			}
		}
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Yellow") {
			Debug.Log("Collision at: " + col.contacts[0].point);
			bullets.Add((GameObject)Instantiate(mirvBullet, col.contacts[0].point + (Vector3.up + Vector3.right) * purpleDist, mirvBullet.transform.rotation));
			bullets.Add ((GameObject)Instantiate(mirvBullet, col.contacts[0].point + (-Vector3.up + Vector3.right) * purpleDist, mirvBullet.transform.rotation));
			bullets.Add((GameObject)Instantiate(mirvBullet, col.contacts[0].point + (Vector3.up - Vector3.right) * purpleDist, mirvBullet.transform.rotation));
			bullets.Add ((GameObject)Instantiate(mirvBullet, col.contacts[0].point + (-Vector3.up - Vector3.right) * purpleDist, mirvBullet.transform.rotation));
			bullets[0].transform.Rotate(0,0,-45);
			bullets[1].transform.Rotate(0,0,-135);
			bullets[2].transform.Rotate(0,0,45);
			bullets[3].transform.Rotate(0,0,135);
		}
	}
}
