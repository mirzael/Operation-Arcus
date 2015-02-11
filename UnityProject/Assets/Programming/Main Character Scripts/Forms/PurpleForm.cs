using UnityEngine;

public class PurpleForm : SecondaryForm {
	private int barrel = 1;
	public float timeBeforeExplosion;
	public float explosionSize;
	public GameObject mirv;
	GameObject reflectBall;
	public float baseRadius;
	public float growValue;
	public float maxRadius;
	
	public void Start() {
		timeActiveOrig = 0.5f;
		baseRadius = 10f;
		growValue = 2f;
		maxRadius = 20f;
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
		reflectBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		reflectBall.transform.localScale = new Vector3(baseRadius, baseRadius, baseRadius);
		reflectBall.renderer.material.color = new Color(1, 0, 1, 0.1f);
		reflectBall.transform.position = transform.position;
		//gameObject.GetComponent<SphereCollider>().radius *= 3;
	}
	
	public void Update() {
		if (!isActive) return;
		timeActive -= Time.deltaTime;
		if (baseRadius + growValue < maxRadius)
			reflectBall.transform.localScale = new Vector3 (baseRadius + growValue, baseRadius + growValue, baseRadius + growValue);
		else
			reflectBall.transform.localScale = new Vector3 (maxRadius, maxRadius, maxRadius);
		growValue = growValue + 1f;
		foreach(Collider collider in Physics.OverlapSphere(reflectBall.transform.position, baseRadius + growValue)){
			if((collider.gameObject.layer == 10) /*|| (collider.gameObject.layer == 8)*/){
				Debug.Log("HIT OBJECT");
				Vector3 direction = (reflectBall.transform.position - collider.transform.position).normalized;
				Debug.Log ("VECTOR MADE");
				Rigidbody theRigid = collider.gameObject.transform;
				Transform temp = collider.gameObject.transform;
				while (temp.parent != null)
					temp = temp.parent;
				theRigid = temp.rigidbody;
				Debug.Log (theRigid.ToString());
				theRigid.velocity = (direction * -20);
				Debug.Log ("FORCE!!!!");
			}
		}
		if (timeActive <= 0.0f) {
			isActive = false;
			growValue = 1;
			Destroy (reflectBall);
			//gameObject.GetComponent<SphereCollider>().radius /= 3;
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