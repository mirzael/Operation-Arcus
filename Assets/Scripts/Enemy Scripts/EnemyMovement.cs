using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	public int pattern = 0;
	public bool stops = false;
	public Vector3 dir;
	public Vector3 newDir;
	public float speed;
	public float timeBeforeStopOnScreen;
	public float timeStayOnScreen;
	private bool onScreen;
	bool blank;

	
	public void Start() {
		blank = true;
		onScreen = false;
		speed = 10;
		Vector3 pos = transform.position;
		
		switch (pattern) {
		case 1: // diagonal left
			timeBeforeStopOnScreen = 1.5f;
			timeStayOnScreen = 3.0f;
			dir = Vector3.down * speed + Vector3.left * speed;
			break;
		case 2: // diagonal right
			timeBeforeStopOnScreen = 1.5f;
			timeStayOnScreen = 3.0f;
			dir = Vector3.down * speed + Vector3.right * speed;
			break;
		case 3: // straight down
			timeBeforeStopOnScreen = 1.5f;
			timeStayOnScreen = 3.0f;
			dir = Vector3.down * speed;
			break;
		case 4: // left-to-right
			timeBeforeStopOnScreen = 1.5f;
			timeStayOnScreen = 3.0f;
			dir = Vector3.right * speed;
			break;
		case 5: // right-to-left
			timeBeforeStopOnScreen = 1.5f;
			timeStayOnScreen = 3.0f;
			dir = Vector3.left * speed;
			break;
		case 6: // diagonal up-left
			timeBeforeStopOnScreen = 1.5f;
			timeStayOnScreen = 3.0f;
			dir = Vector3.up * speed + Vector3.left * speed;
			break;
		case 7: // diagonal up-right
			timeBeforeStopOnScreen = 1.5f;
			timeStayOnScreen = 3.0f;
			dir = Vector3.up * speed + Vector3.right * speed;
			break;
		case 8: // straight down
			timeBeforeStopOnScreen = 1.5f;
			timeStayOnScreen = 3.0f;
			dir = Vector3.down * speed + Vector3.right * speed;
			newDir = Vector3.up * speed + Vector3.right * speed;
			blank = false;
			break;
		default:
			timeBeforeStopOnScreen = 1.5f;
			timeStayOnScreen = 3.0f;
			dir = Vector3.down * speed;
			break;
		}
		transform.rigidbody.velocity = dir;
	}
	
	public void Update() {
		if (stops && timeBeforeStopOnScreen > 0.0f) {
			timeBeforeStopOnScreen -= Time.deltaTime;
			if (timeBeforeStopOnScreen <= 0.0f) {
				transform.rigidbody.velocity = new Vector3(0, 0, 0);
			}
		} else if (stops && timeStayOnScreen > 0.0f) {
			timeStayOnScreen -= Time.deltaTime;
			if (timeStayOnScreen <= 0.0f) {
				if (blank == true)
					transform.rigidbody.velocity = dir;
				else
					transform.rigidbody.velocity = newDir;
			}
		}
	}
	
	public void OnBecameVisible() {
		onScreen = true;
	}
}