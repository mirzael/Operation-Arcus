using UnityEngine;
using MainCharacter;


public class RedForm : PrimaryForm {
	public float explosionRadius;
	public float radiusPerPoint;
	
	public void Start() {
		RedWeapon rWep = projectile.GetComponent<RedWeapon>();
		rWep.baseExplosionRadius = explosionRadius;
		rWep.radiusPerPoint = radiusPerPoint;
		rWep.damage = damage;
	}
	
	public override void Fire() {
		GameObject newProj = (GameObject)Instantiate(projectile, gameObject.transform.position + Vector3.up * PROJECTILE_DISTANCE + Vector3.left / 2, projectile.transform.rotation);
		newProj.rigidbody.velocity = Vector3.up * getSpeed();
		newProj = (GameObject)Instantiate(projectile, gameObject.transform.position + Vector3.up * PROJECTILE_DISTANCE + Vector3.right / 2, projectile.transform.rotation);
		newProj.rigidbody.velocity = Vector3.up * getSpeed();
	}
	
	public override bool TakeHit(Collision col) {
		string tag = col.gameObject.tag;
		Destroy(col.gameObject);
		
		if (col.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
			return true;
		}
		
		if (tag == "Red") {
			setPower(ColorPower.Instance.powerRed + POWER_INC);
			//Debug.Log("Absorbed red bullet, Red Power at " + power);
			return false;
		}
		return true;
	}
	
	public override void setPower(float amount) {
		base.setPower(amount);
		setSpeed(originalSpeed + power / 30);
	}
}