using UnityEngine;

public class PurpleForm : SecondaryForm {
    //Commented out because it's not being used.
	//private int barrel = 1;
	public float timeBeforeExplosion;
	public float explosionSize;
	public GameObject mirv;
	
	public void Start() {
		timeActiveOrig = 0.5f;
		/*MoveProjectile moveScript = projectile.GetComponent<MoveProjectile>();
		moveScript.projectileSpeed = projectileSpeed;
		
		PurpleWeapon mirvStuff = projectile.GetComponent<PurpleWeapon>();
		mirvStuff.mirvBullet = mirv;
		mirvStuff.bulletSpeed = projectileSpeed;
		mirvStuff.timeBeforeExplosion = timeBeforeExplosion;
		mirvStuff.damage = damage;
		mirvStuff.explosionSize = explosionSize;*/
	}
	
	public override void Fire() {
		/*
		barrel *= -1;
		projectile = (GameObject)Instantiate(projectile,
				transform.position + Vector3.up * PROJECTILE_DISTANCE + Vector3.left * PROJECTILE_DISTANCE / 2.5f * barrel,
				projectile.transform.rotation);
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