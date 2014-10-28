using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	public int pattern = 0;
	public Vector3 dir;
	public float speed;
	public float timeBeforeStopOnScreen;
	public float timeStayOnScreen;
	private bool onScreen;
	
	public void Start() {
		onScreen = false;
		speed = 10;
		Vector3 pos = transform.position;
		
		switch (pattern) {
		case 1: // diagonal left
			timeBeforeStopOnScreen = 0.5f;
			timeStayOnScreen = 3.0f;
			dir = Vector3.down * speed + Vector3.left * speed;
			break;
		case 2: // diagonal right
			timeBeforeStopOnScreen = 0.5f;
			timeStayOnScreen = 3.0f;
			dir = Vector3.down * speed + Vector3.right * speed;
			break;
		case 3: // straight down
			timeBeforeStopOnScreen = 0.5f;
			timeStayOnScreen = 3.0f;
			dir = Vector3.down * speed;
			break;
		default:
			timeBeforeStopOnScreen = 0.5f;
			timeStayOnScreen = 3.0f;
			dir = Vector3.down * speed;
			break;
		}
		transform.rigidbody.velocity = dir;
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