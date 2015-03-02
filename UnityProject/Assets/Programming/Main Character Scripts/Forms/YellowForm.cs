using UnityEngine;

public class YellowForm : PrimaryForm {
	public float pointsPerBullet;
	
	public void Start() {
		projectile.GetComponent<YellowWeapon>().damage = damage;
	}
	
	public override void Fire() {
		int numProjectiles = 2 + (int)(power / pointsPerBullet);
		int projectileSpreadAngle = 20;
		int angleBetweenProjectiles = (projectileSpreadAngle / (numProjectiles - 1));
		float radToDeg =  Mathf.PI / 180;
		GameObject[] blast = new GameObject[numProjectiles];
		for(int i = 0; i < numProjectiles; i++) {
			float trajectoryDegree = 90 + (projectileSpreadAngle / 2 - angleBetweenProjectiles * i);
			float currentAngularVelocity = Mathf.Cos(trajectoryDegree * radToDeg);
			blast[i] = (GameObject)Instantiate(projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, projectile.transform.rotation);
			blast[i].rigidbody.velocity = transform.TransformDirection(Vector3.back * getSpeed() + Vector3.right * currentAngularVelocity * getSpeed());
		}
	}
	
	public override bool TakeHit(Collision col) {
		string tag = col.gameObject.tag;
		Destroy(col.gameObject);
		if (tag == "Yellow") {
			setPower(power + POWER_INC);
			//Debug.Log("Absorbed yellow bullet, Yellow Power at " + power);
			return false;
		}
		return true;
	}
}