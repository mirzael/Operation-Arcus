﻿using UnityEngine;
using System.Collections;

public class GreenWeapon : MonoBehaviour {
	public bool isStraight = false;
	public float amplitude = 5f;
	public float sphereRadius;
	public float empDuration;
	public float degreesPerSec;
	public float degrees = 0;
	public float ySpeed;
	Vector3 centerPos;

	Material greenBlast;


	// Use this for initialization
	void Start () {
		greenBlast = (Material)Resources.Load ("Materials/AoeBlasts/GreenBlast", typeof(Material));
		centerPos = transform.position;
		if (isStraight)	degreesPerSec = 0;
	}
	
	// Update is called once per frame
	void Update () {
		centerPos += Vector3.up * ySpeed / 100;
		degrees = Mathf.Repeat(degrees + (Time.deltaTime * degreesPerSec), 360.0f);
		var radians = degrees * Mathf.Deg2Rad;

		// Offset by sin wave
		Vector3 offset = new Vector3(amplitude * Mathf.Sin(radians), 0.0f, 0.0f);
		transform.position = centerPos + offset;
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "Red"){
			//Create the green Sphere
			var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphere.renderer.material = greenBlast;
			sphere.transform.position = transform.position;
			sphere.transform.localScale = new Vector3(sphereRadius, sphereRadius, sphereRadius);
			var dis = sphere.AddComponent<EnableDisable>();
			dis.empDuration = empDuration;
			dis.sphereRadius = sphereRadius;
			Destroy(sphere.collider);
			Destroy (sphere, 0.5f);

		}
	}
}