using System;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	public Vector3 dir;
	public float speed;
	public float timeBeforeStopOnScreen;
	public float timeStayOnScreen;
	private bool onScreen;
	
	public void Start() {
		onScreen = false;
		speed = 10;
		timeBeforeStopOnScreen = 0.5f;
		timeStayOnScreen = 3.0f;
		dir = Vector3.down * speed;
		transform.rigidbody.velocity = dir; // Vector3.down * 10 + Vector3.left * Random.Range(-10f, 10f);
	}
	
	public void Update() {
		if (onScreen && timeBeforeStopOnScreen > 0.0f) {
			timeBeforeStopOnScreen -= Time.deltaTime;
			if (timeBeforeStopOnScreen <= 0.0f) {
				transform.rigidbody.velocity = new Vector3(0, 0, 0);
			}
		} else if (onScreen && timeStayOnScreen > 0.0f) {
			timeStayOnScreen -= Time.deltaTime;
			if (timeStayOnScreen <= 0.0f) {
				transform.rigidbody.velocity = dir;
			}
		}
	}
	
	public void OnBecameVisible() {
		onScreen = true;
	}
}