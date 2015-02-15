using UnityEngine;

public class MPDOrangeForm : SecondaryForm {
	MainCharacterDriver driver;
	SphereCollider col;
	
	public void Start() {
		timeActiveOrig = 4f;
		col = gameObject.GetComponent<SphereCollider> ();
	}
	
	public override void Fire() {
	}
	
	public override void Activate() {
		isActive = true;
		timeActive = timeActiveOrig;
		col.radius *= 3;
	}
	
	public void Update() {
		if (!isActive) return;
		timeActive -= Time.deltaTime;
		if (timeActive <= 0.0f) {
			isActive = false;
			col.radius /= 3;
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