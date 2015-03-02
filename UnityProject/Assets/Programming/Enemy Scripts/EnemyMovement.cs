using UnityEngine;

public class EnemyMovement : MonoBehaviour {
	public int pattern = 0;
	public bool stops = false;
	public Vector3 dir;
	public Vector3 newDir;
	public float speed;
	public float timeBeforeStopOnScreen;
	public float timeStayOnScreen;
	bool blank;

    private Vector3 fakeVelocity;

	
	public void Start() {
		blank = true;
		speed = 10;
		
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
		case 9: // straight down
			timeBeforeStopOnScreen = 1.5f;
			timeStayOnScreen = 3.0f;
			dir = Vector3.down * speed + Vector3.left * speed;
			newDir = Vector3.up * speed + Vector3.left * speed;
			blank = false;
			break;
		case 10: // Don't move
			timeBeforeStopOnScreen = 10000000000000000f;
			dir = new Vector3(0,0,0);
			break;
		default:
			timeBeforeStopOnScreen = 1.5f;
			timeStayOnScreen = 3.0f;
			dir = Vector3.down * speed;
			break;
		}
        SetVelocity(dir);
	}
	
	public void Update() {
		if (stops && timeBeforeStopOnScreen > 0.0f) {
			timeBeforeStopOnScreen -= Time.deltaTime;
			if (timeBeforeStopOnScreen <= 0.0f) {
                SetVelocity(Vector3.zero);
			}
		} else if (stops && timeStayOnScreen > 0.0f) {
			timeStayOnScreen -= Time.deltaTime;
			if (timeStayOnScreen <= 0.0f) {
				if (blank == true)
					SetVelocity(dir);
				else
					SetVelocity(newDir);
			}
		}

        //asteroids are kinematic
        if(rigidbody.isKinematic)
        {
            rigidbody.MovePosition(transform.position + fakeVelocity*Time.deltaTime);
        }
	}

    private void SetVelocity(Vector3 v)
    {
        //Check if kinematic for asteroids - we'll need to fake their movement
        if (rigidbody.isKinematic)
        {
            fakeVelocity = v;
        }
        else
        {
            rigidbody.velocity = v;
        }
    }

}