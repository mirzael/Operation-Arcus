﻿using UnityEngine;
using System.Collections;

public class The_Destroyer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider collision){
		Destroy (collision.gameObject);
	}
}
