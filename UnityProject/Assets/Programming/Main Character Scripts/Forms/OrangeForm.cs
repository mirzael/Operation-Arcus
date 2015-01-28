using UnityEngine;

public class OrangeForm : SecondaryForm {
	public float explosionRadius;
	public float gravityForce;
	public float gravityRadius;
	public float rotationSpeed;
	
	public void Start() {
		OrangeWeapon oWep = projectile.GetComponent<OrangeWeapon>();
		oWep.moveSpeed = projectileSpeed;
		oWep.rotationSpeed = rotationSpeed;
		oWep.explosionRadius = explosionRadius;
		oWep.gravityRadius = gravityRadius;
		oWep.gravityForce = gravityForce;
		oWep.damage = damage;
	}
	
	public override void Fire() {
		/*
		var oBlast = new GameObject[2];
		oBlast[0] = (GameObject)Instantiate(projectile, transform.position + (Vector3.up + Vector3.left) * PROJECTILE_DISTANCE, projectile.transform.rotation);
		oBlast[1] = (GameObject)Instantiate(projectile, transform.position + (Vector3.up + Vector3.right) * PROJECTILE_DISTANCE, projectile.transform.rotation);
		*/
	}
	
	public override void Activate() {
		isActive = true;
		timeActive = timeActiveOrig;
		gameObject.GetComponent<SphereCollider>().radius *= 3;
	}
	
	public void Update() {
		if (!isActive) return;
		timeActive -= Time.deltaTime;
		if (timeActive <= 0.0f) {
			isActive = false;
			gameObject.GetComponent<SphereCollider>().radius /= 3;
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
			Destroy(col.gameObject);
			return true;
		}
		
		return false;
	}
}