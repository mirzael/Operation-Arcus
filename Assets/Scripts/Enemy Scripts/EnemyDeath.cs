using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDeath : MonoBehaviour {
	const float SPHERE_DURATION = 0.5f;
	const float SPHERE_RADIUS = 1f;
	Material redBlast;
	Material orangeBlast;
	Material greenBlast;
	PointMaster points;

	// Use this for initialization
	void Start () {
		points = Component.FindObjectOfType<PointMaster> ();
		redBlast = (Material)Resources.Load ("Materials/AoeBlasts/RedBlast", typeof(Material));
		orangeBlast = (Material)Resources.Load ("Materials/AoeBlasts/OrangeBlast", typeof(Material));
		greenBlast = (Material)Resources.Load ("Materials/AoeBlasts/GreenBlast", typeof(Material));
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnCollisionEnter(Collision col){
		//AOE, BABY
		if (col.gameObject.tag == "Red") {
			CreateAoe (col.contacts [0].point, redBlast, MainCharacterDriver.powerRed/100+2.5f, 0.5f, false);
		} else if(col.gameObject.tag == "Green" && gameObject.tag == "Red"){
			int layerMask = 1 << 8;
			//Create the green Sphere
			var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphere.renderer.material = greenBlast;
			sphere.transform.position = transform.position;
			sphere.transform.localScale = new Vector3(SPHERE_RADIUS*10f, SPHERE_RADIUS*10f, SPHERE_RADIUS*10f);
			Destroy(sphere.collider);
			Destroy (sphere, SPHERE_DURATION);
			//Add the disabler script - enemy can't move or shoot
			foreach(Collider collider in Physics.OverlapSphere(transform.position, SPHERE_RADIUS*10f, layerMask)){
				collider.gameObject.AddComponent<Disabler>();
			}
		}

		if (col.gameObject.tag != "Purple" && col.gameObject.tag != "Green") {
			Destroy (col.gameObject);
		}
		points.Notify (new DeathInfo{ shipTag = gameObject.tag, bulletTag = col.gameObject.tag, shipPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z)} );
		Destroy (gameObject);
	}

	void CreateAoe(Vector3 center, Material mat, float radius, float duration, bool gravity){
		var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.renderer.material = mat;
		sphere.transform.position = center;
		sphere.transform.localScale = new Vector3(radius, radius, radius);
		if(gravity){ 
			sphere.AddComponent<GravityField>();
			Destroy (sphere.collider);
		}else{
			sphere.layer = LayerMask.NameToLayer("Character Bullet");
		}
		Destroy (sphere, duration);
		Destroy(gameObject);
	}
}
