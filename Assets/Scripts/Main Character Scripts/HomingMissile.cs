using UnityEngine;
using System.Collections;

public class HomingMissile : MonoBehaviour {
	Transform shape;
	Transform target;
	Transform myTransform;

	float moveSpeed = 25f;
	float rotationSpeed = 50f;

	// Use this for initialization
	void Start () {
		myTransform = transform;
		findTarget ();
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null && transform.position.x < target.position.x) {
			myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
				Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed*Time.deltaTime);
			
			//move towards the player
			myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;

		} else {
			findTarget();
		}
	}

	void findTarget(){
		RaycastHit hit;
		int layermask = 1 << 8;
		
		//RenderVolume (transform.position + Vector3.up, transform.position - Vector3.up, 2f, -transform.up, 100);
		if (Physics.CapsuleCast (transform.position + Vector3.up * 0.5f, transform.position - Vector3.up * 0.5f, 10f, Vector3.right, out hit, Mathf.Infinity, layermask)) {
			Debug.Log ("I HIT AN ENEMY");
			target = hit.transform;
		} else {
			Destroy (gameObject);
		}
	}

	void RenderVolume(Vector3 p1, Vector3 p2, float radius, Vector3 dir, float distance){
		if (!shape){ // if shape doesn't exist yet, create it
			shape = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
			Destroy(shape.collider); // no collider, please!
			shape.renderer.material = (Material)Resources.Load("Materials/Invis", typeof(Material)); // assign the selected material to it
		}
		Vector3 scale; // calculate desired scale
		float diam = 2 * radius; // calculate capsule diameter
		scale.x = diam; // width = capsule diameter
		scale.y = Vector3.Distance(p2, p1) + diam; // capsule height
		scale.z = distance + diam; // volume length
		shape.localScale = scale; // set the rectangular volume size
		// set volume position and rotation
		shape.position = (p1 + p2 + dir.normalized * distance) / 2;
		shape.rotation = Quaternion.LookRotation(dir, p2 - p1);
		shape.renderer.enabled = true; // show it
	}
}
