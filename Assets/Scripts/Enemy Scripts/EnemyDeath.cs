using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyDeath : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter(Collision col){
		//AOE, BABY
		if (col.gameObject.tag == "Red") {
			CreateAoe (col.contacts[0].point, (Material) Resources.Load("Materials/AoeBlasts/RedBlast", typeof(Material)), 2.5f, 0.5f);
		}else if(col.gameObject.tag == "Orange"){
			if(gameObject.tag == "Blue"){
				CreateAoe (col.contacts[0].point, (Material) Resources.Load("Materials/AoeBlasts/OrangeBlast", typeof(Material)), 10f, 0.25f, true);
			}else{
				CreateAoe (col.contacts[0].point, (Material) Resources.Load("Materials/AoeBlasts/OrangeBlast", typeof(Material)), 4f, 0.5f);
			}
		}
		Destroy (col.gameObject);
		Destroy (gameObject);
	}

	void CreateAoe(Vector3 center, Material mat, float radius, float duration, bool gravity = false){
		var collisions = Physics.OverlapSphere(center, radius);
		for(int i = 0; i < collisions.Length; i++){
			if(collisions[i].gameObject.layer == LayerMask.NameToLayer("Enemy")){
				var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
				sphere.renderer.material = mat;
				sphere.transform.position = collisions[i].transform.position;
				sphere.transform.localScale = new Vector3(radius, radius, radius);
				if(gravity){ 
					sphere.AddComponent<GravityField>();
				}else{
					sphere.layer = LayerMask.NameToLayer("Character Bullet");
					Destroy(collisions[i].gameObject);
				}
				Destroy (sphere, duration);
			}
		}
	}
}
