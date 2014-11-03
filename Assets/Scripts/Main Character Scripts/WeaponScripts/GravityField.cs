using UnityEngine;
using System.Collections;

public class GravityField : MonoBehaviour {
	public float GRAVITY_FIELD = 100f;
	public float GRAVITY_FORCE = 350f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		foreach (Collider collider in Physics.OverlapSphere(transform.position, GRAVITY_FIELD)) {
			if(collider.gameObject.layer == LayerMask.NameToLayer("Enemy")){
				// calculate direction from target to me
				Vector3 forceDirection = transform.position - collider.transform.position;
				
				// apply force on target towards me
				collider.rigidbody.AddForce(forceDirection.normalized * GRAVITY_FORCE * Time.fixedDeltaTime);
			}
		}
	}
}
