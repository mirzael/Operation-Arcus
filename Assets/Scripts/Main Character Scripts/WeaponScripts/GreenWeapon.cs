using UnityEngine;
using System.Collections;

public class GreenWeapon : MonoBehaviour {

	public float amplitude = 5f;
	public float degreesPerSec;
	public float degrees = 0;
	public float ySpeed;
	Vector3 centerPos;


	// Use this for initialization
	void Start () {
		centerPos = transform.position;
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
}