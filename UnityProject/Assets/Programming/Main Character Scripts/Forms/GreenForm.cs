using UnityEngine;

public class GreenForm : SecondaryForm {
	public float empRadius;
	public float empDuration;
	public float sinAmplitude;
	public GameObject empBlastPrefab;
	public float prefabSize;
	private const int DEGREES_PER_SEC = 720;
	
	public void Start() {
		timeActiveOrig = 0.5f;
		GreenWeapon gWep = projectile.GetComponent<GreenWeapon>();
		gWep.isStraight = true;
		gWep.ySpeed = getSpeed();
		gWep.sphereRadius = empRadius;
		gWep.empDuration = empDuration;
		gWep.damage = damage;
		empBlastPrefab = (GameObject)Resources.Load ("Prefabs/GreenExplosion", typeof(GameObject));
	}
	
	public override void Fire() {
		/*
		GameObject[] gProj = new GameObject[3];
		gProj[0] = (GameObject)Instantiate(projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, projectile.transform.rotation);
		gProj[1] = (GameObject)Instantiate(projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, projectile.transform.rotation);
		gProj[2] = (GameObject)Instantiate(projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, projectile.transform.rotation);
		sinBullet(gProj[0].GetComponent<GreenWeapon>(), false);
		sinBullet(gProj[1].GetComponent<GreenWeapon>(), true);
		*/
	}
	
	public override void Activate() {
		isActive = true;
		timeActive = timeActiveOrig;

		//Create the green Sphere
		var sphere = (GameObject)Instantiate(empBlastPrefab, transform.position, empBlastPrefab.transform.rotation);
		//sphere.renderer.material = empBlast;
		sphere.transform.position = transform.position;
		//sphere.transform.localEulerAngles = new Vector3(0, 110.0788f, 0);
		sphere.transform.localScale = new Vector3(prefabSize, prefabSize, prefabSize);
		var dis = sphere.AddComponent<EnableDisable>();
		dis.empDuration = empDuration;
		dis.sphereRadius = empRadius;
		Destroy(sphere.collider);
		Destroy (sphere, 0.5f);
	}
	
	public void Update() {
		if (!isActive) return;
		timeActive -= Time.deltaTime;
		if (timeActive <= 0.0f) {
			isActive = false;
		}
	}
	
	public override bool TakeHit(Collision col) {
		Destroy(col.gameObject);
		return false;
	}
	
	private void sinBullet(GreenWeapon weapon, bool isNegative) {
		weapon.isStraight = false;
		weapon.amplitude = isNegative ? -sinAmplitude : sinAmplitude;
		weapon.degreesPerSec = DEGREES_PER_SEC;
	}
}