using UnityEngine;

public class GreenForm : Form {
	public float empRadius;
	public float empDuration;
	public float sinAmplitude;
	private const int DEGREES_PER_SEC = 720;
	
	public void Start() {
		GreenWeapon gWep = projectile.GetComponent<GreenWeapon>();
		gWep.isStraight = true;
		gWep.ySpeed = getSpeed();
		gWep.sphereRadius = empRadius;
		gWep.empDuration = empDuration;
		gWep.damage = damage;
	}
	
	public override void Fire() {
		GameObject[] gProj = new GameObject[3];
		gProj[0] = (GameObject)Instantiate(projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, projectile.transform.rotation);
		gProj[1] = (GameObject)Instantiate(projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, projectile.transform.rotation);
		gProj[2] = (GameObject)Instantiate(projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, projectile.transform.rotation);
		sinBullet(gProj[0].GetComponent<GreenWeapon>(), false);
		sinBullet(gProj[1].GetComponent<GreenWeapon>(), true);
	}
	
	public override bool TakeHit(Collision col) {
		return true;
	}
	
	private void sinBullet(GreenWeapon weapon, bool isNegative) {
		weapon.isStraight = false;
		weapon.amplitude = isNegative ? -sinAmplitude : sinAmplitude;
		weapon.degreesPerSec = DEGREES_PER_SEC;
	}
}