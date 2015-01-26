using UnityEngine;

public class RainbowForm : PrimaryForm {
	
	public void Start() {
		projectile.GetComponent<RainbowWeapon>().damage = damage;
	}
	
	public override void Fire() {
		GameObject[] rainboom = new GameObject[15];
		int rainbowSpreadAngle = 50;
		int rainbowBetweenProjectiles = (rainbowSpreadAngle / (15 - 1));
		float rToD =  Mathf.PI / 180;
		Debug.Log (projectile.transform.rotation.x);
		for(int i = 0; i < rainboom.Length; i++){
			float trajectoryDegree = 90 + (rainbowSpreadAngle / 2 - rainbowBetweenProjectiles * i);
			float currentAngularVelocity = Mathf.Cos(trajectoryDegree * rToD);
			rainboom[i] = (GameObject)Instantiate(projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, projectile.transform.rotation);
			rainboom[i].rigidbody.velocity = transform.TransformDirection(Vector3.back * getSpeed() + Vector3.right * currentAngularVelocity * getSpeed());
		}
	}
	
	public override bool TakeHit(Collision col) {
		return false;
	}
}
