using UnityEngine;

public class MPDPurpleForm : PurpleForm {
	private DarcusDriver driver;
	public GameObject oArcus;
	private int numFrames;
	private const int FRAMES_TO_DOCK = 10;
	
	new public void Start() {
		timeActiveOrig = 4.0f;
		driver = gameObject.GetComponent<DarcusDriver>();
	}
	
	public override void Activate() {
		isActive = true;
		timeActive = timeActiveOrig;
		gameObject.GetComponent<SphereCollider>().radius *= 3;
		numFrames = 0;
		driver.canMove = false;
	}
	
	new public void Update() {
		if (!isActive) return;
		timeActive -= Time.deltaTime;
		
		if (numFrames < FRAMES_TO_DOCK) {
			numFrames++;
			Vector3 diff = (oArcus.transform.position + Vector3.up * 5) - transform.position;
			transform.position += (diff * (1.0f / FRAMES_TO_DOCK));
		} else {
			transform.position = oArcus.transform.position + Vector3.up * 5;
		}
		
		if (timeActive <= 0.0f) {
			isActive = false;
			gameObject.GetComponent<SphereCollider>().radius /= 3;
			driver.canMove = true;
		}
	}
	
	public void OnCollisionEnter(Collision col) {
		TakeHit(col);
	}
}