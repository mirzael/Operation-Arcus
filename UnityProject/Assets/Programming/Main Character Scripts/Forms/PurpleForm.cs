using UnityEngine;

public class PurpleForm : SecondaryForm {
	private int barrel = 1;
	public float timeBeforeExplosion;
	public float explosionSize;
	public GameObject mirv;
	
	public void Start() {
		MoveProjectile moveScript = projectile.GetComponent<MoveProjectile>();
		moveScript.projectileSpeed = projectileSpeed;
		
		PurpleWeapon mirvStuff = projectile.GetComponent<PurpleWeapon>();
		mirvStuff.mirvBullet = mirv;
		mirvStuff.bulletSpeed = projectileSpeed;
		mirvStuff.timeBeforeExplosion = timeBeforeExplosion;
		mirvStuff.damage = damage;
		mirvStuff.explosionSize = explosionSize;
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