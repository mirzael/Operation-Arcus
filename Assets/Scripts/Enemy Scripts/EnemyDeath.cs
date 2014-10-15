using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDeath : MonoBehaviour {
	float purpleDist = .75f;
	Material redBlast;
	Material orangeBlast;
	Material purpleBlast;

	// Use this for initialization
	void Start () {
		redBlast = (Material)Resources.Load ("Materials/AoeBlasts/RedBlast", typeof(Material));
		orangeBlast = (Material)Resources.Load ("Materials/AoeBlasts/OrangeBlast", typeof(Material));
		purpleBlast = (Material)Resources.Load ("Materials/AoeBlasts/PurpleBlast", typeof(Material));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		//AOE, BABY
		if (col.gameObject.tag == "Red") {
			CreateAoe (col.contacts [0].point, redBlast, MainCharacterDriver.powerRed/100+2.5f, 0.5f);
		} else if (col.gameObject.tag == "Orange") {
			if (gameObject.tag == "Blue") {
				CreateAoe (col.contacts [0].point, orangeBlast, 10f, 0.25f, true);
			} else {
				CreateAoe (col.contacts [0].point, orangeBlast, 4f, 0.5f);
			}
		} else if (col.gameObject.tag == "Purple" && gameObject.tag == "Yellow") {
			CreateAoe(col.contacts[0].point + (Vector3.up + Vector3.right) * purpleDist, purpleBlast, 1f, .5f);
			CreateAoe(col.contacts[0].point + (-Vector3.up + Vector3.right) * purpleDist, purpleBlast, 1f, .5f);
			CreateAoe(col.contacts[0].point + (Vector3.up - Vector3.right) * purpleDist, purpleBlast, 1f, .5f);
			CreateAoe(col.contacts[0].point + (-Vector3.up - Vector3.right) * purpleDist, purpleBlast, 1f, .5f);
		}

		if (col.gameObject.tag != "Purple") {
			Destroy (col.gameObject);
		}
		Destroy (gameObject);
	}

	void CreateAoe(Vector3 center, Material mat, float radius, float duration, bool gravity = false){
		var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.renderer.material = mat;
		sphere.transform.position = center;
		sphere.transform.localScale = new Vector3(radius, radius, radius);
		if(gravity){ 
			sphere.AddComponent<GravityField>();
		}else{
			sphere.layer = LayerMask.NameToLayer("Character Bullet");
			Destroy(gameObject);
		}
		Destroy (sphere, duration);
	}
}
