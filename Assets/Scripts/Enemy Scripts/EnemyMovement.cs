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
		Vector3 pos = transform.position;
		
		int moveType = Random.Range(0, 3);
		switch (moveType) {
		case 0:
			transform.position = new Vector3(pos.x + Random.Range(-5f, 5f), pos.y, pos.z);
			timeBeforeStopOnScreen = 0.5f;
			timeStayOnScreen = 3.0f;
			dir = Vector3.down * speed + Vector3.left * speed;
			break;
		case 1:
			transform.position = new Vector3(pos.x + Random.Range(-5f, 5f), pos.y, pos.z);
			timeBeforeStopOnScreen = 0.5f;
			timeStayOnScreen = 3.0f;
			dir = Vector3.down * speed + Vector3.right * speed;
			break;
		case 2:
			transform.position = new Vector3(pos.x + Random.Range(-10f, 10f), pos.y, pos.z);
			timeBeforeStopOnScreen = 0.5f;
			timeStayOnScreen = 3.0f;
			dir = Vector3.down * speed;
			break;
		case 3:
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