using UnityEngine;

public class ScrollBackground : MonoBehaviour {
	public float numSeconds = 0;
	private float distPerSecond;
	private const float startPos = 88.0f;
	private const float endPos = -71.0f;
	
	public void Start() {
		distPerSecond = (endPos - startPos) / numSeconds;
		transform.position = new Vector3(transform.position.x, startPos, transform.position.z);
	}
	
	public void Update() {
		transform.position += new Vector3(0, distPerSecond * Time.deltaTime, 0);
		if (transform.position.y <= endPos) {
			Destroy(this);
		}
	}
}
