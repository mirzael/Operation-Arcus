using UnityEngine;

public class OrangeForm : SecondaryForm {
	public float explosionRadius;
	public float gravityForce;
	public float gravityRadius;
	public float rotationSpeed;
	public GameObject orangeProjectile;
	
	public void Start() {
		timeActiveOrig = 0.5f;
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
		/*GameObject reflectBall = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		reflectBall.transform.localScale = new Vector3(10, 10, 10);
		reflectBall.renderer.material.color = new Color(1, 0.5f, 0);
		reflectBall.transform.position = transform.position + Vector3.up * 7;
		Rigidbody rb = reflectBall.AddComponent<Rigidbody>();
		*/
		GameObject reflectBall = (GameObject)Instantiate(orangeProjectile);
		reflectBall.transform.position = transform.position + Vector3.up * 7;
		Rigidbody rb = reflectBall.GetComponent<Rigidbody>();
		rb.isKinematic = true;
		reflectBall.AddComponent<OrangeWeapon>();
		Destroy(reflectBall, 4);
	}
	
	public void Update() {
		if (!isActive) return;
		timeActive -= Time.deltaTime;
		if (timeActive <= 0.0f) {
			isActive = false;
		}
	}
	
	public override bool TakeHit(Collision col) {
		return false;
	}
}