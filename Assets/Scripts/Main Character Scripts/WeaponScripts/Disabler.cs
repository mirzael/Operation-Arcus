using UnityEngine;
using System.Collections;

public class Disabler : MonoBehaviour {

	public float disabledTime = 1f;
	Vector3 initVelocity;
	Shooter shooting;
	EnemyMovement movement;

	// Use this for initialization
	void Start () {
		rigidbody.isKinematic = true;
		initVelocity = rigidbody.velocity;
		movement = GetComponent<EnemyMovement> ();
		shooting = GetComponent<Shooter> ();
		movement.enabled = false;
		shooting.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		disabledTime -= Time.deltaTime;
		if (disabledTime <= 0) {
			rigidbody.isKinematic = false;
			rigidbody.velocity = initVelocity;
			movement.enabled = true;
			shooting.enabled = true;
			Destroy (this);
		}
	}
}
