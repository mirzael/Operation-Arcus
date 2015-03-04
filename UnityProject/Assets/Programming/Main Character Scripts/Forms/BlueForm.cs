using UnityEngine;

public class BlueForm : PrimaryForm {
	public float cooldownOffsetPerPower = 0.00015f;
	
	public void Start() {
		projectile.GetComponent<BlueWeapon>().damage = damage;
	}
	
	public override void Fire() {
		GameObject newProj = (GameObject)Instantiate(projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, projectile.transform.rotation);
		newProj.rigidbody.velocity = Vector3.up * getSpeed();
	}
	
	public override bool TakeHit(Collision col) {
		string tag = col.gameObject.tag;
		Destroy(col.gameObject);
		
		if (col.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
			return true;
		}
		
		if (tag == "Blue") {
			setPower(power + POWER_INC);
			//Debug.Log("Absorbed blue bullet, Blue Power at " + power);
			return false;
		}
		return true;
	}
	
	public override void setPower(float amount) {
		base.setPower(amount);
		setCooldown(originalCooldown - cooldownOffsetPerPower * power);
	}
}
