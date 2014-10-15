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
			Vector3 center = col.contacts[0].point;
			var collisions = Physics.OverlapSphere(center, 2.5f);
			for(int i = 0; i < collisions.Length; i++){
				if(collisions[i].gameObject.layer == LayerMask.NameToLayer("Enemy")){
					var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
					sphere.renderer.material = (Material) Resources.Load("Materials/AoeBlasts/RedBlast", typeof(Material));
					sphere.layer = LayerMask.NameToLayer("Character Bullet");
					sphere.transform.position = collisions[i].transform.position;
					sphere.transform.localScale = new Vector3(2.5f,2.5f,2.5f);
					Destroy (sphere, 0.5f);
					Destroy(collisions[i].gameObject);
				}
			}
		}
		Destroy (col.gameObject);
		Destroy (gameObject);
	}
}
