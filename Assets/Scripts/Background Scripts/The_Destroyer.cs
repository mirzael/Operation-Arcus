using UnityEngine;
using System.Collections;

public class The_Destroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider collision){
		if (collision.gameObject.tag == "Green") {
			Destroy (collision.gameObject, 0.5f);
		} else {
			Destroy (collision.gameObject);
		}
	}
}
