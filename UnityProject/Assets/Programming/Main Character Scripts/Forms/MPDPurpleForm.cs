using UnityEngine;
using Spectrum;

public class MPDPurpleForm : SecondaryForm {
	private KatherineMainDArcusDriver driver;
	public GameObject oArcus;
	private int numFrames;
	private const int FRAMES_TO_DOCK = 10;
	
	public void Start() {
		timeActiveOrig = 4.0f;
		driver = gameObject.GetComponent<KatherineMainDArcusDriver>();
	}
	
	public override void Activate() {
		isActive = true;
		timeActive = timeActiveOrig;
		gameObject.GetComponent<SphereCollider>().radius *= 3;
		numFrames = 0;
		driver.canMove = false;
	}

	public override void Fire(){}
	
	public void Update() {
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

	public override bool TakeHit(Collision col) {
		if (col.gameObject.layer == LayerMask.NameToLayer("Enemy Bullet")) {
			col.gameObject.rigidbody.velocity *= -2;
			// move the bullet away a bit
			col.gameObject.transform.position += col.gameObject.rigidbody.velocity;
			// make sure the bullet disappears at some point
			Destroy(col.gameObject, 3);
		} else if (col.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
			Destroy(col.gameObject.GetComponent<EnemyMovement>());
			col.gameObject.rigidbody.velocity *= -2;
			// move the enemy away a bit
			//col.gameObject.transform.position += col.gameObject.rigidbody.velocity;
			// make sure the enemy disappears at some point
			Destroy(col.gameObject, 3);
		}
		
		return false;
	}
}