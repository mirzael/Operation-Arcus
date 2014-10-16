using UnityEngine;
using System.Collections;

public class Disabler : MonoBehaviour {

	float disabledTime = 1f;
	Vector3 initVelocity;
	Shooter shooting;

	// Use this for initialization
	void Start () {
		rigidbody.isKinematic = true;
		initVelocity = rigidbody.velocity;
		shooting = GetComponent<Shooter> ();
		shooting.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		disabledTime -= Time.deltaTime;
		if (disabledTime <= 0) {
			rigidbody.isKinematic = false;
			rigidbody.velocity = initVelocity;
			shooting.enabled = true;
			Destroy (this);
		}
	}
}
