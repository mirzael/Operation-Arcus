using UnityEngine;
using MainCharacter;
using System.Collections;

public class GreenWeapon : MonoBehaviour {
	public bool isStraight = false;
	public float amplitude = 2.5f;
	public float sphereRadius;
	public float empDuration;
	public float degreesPerSec;
	public float degrees = 0;
	public float ySpeed;
	public float damage;
	Vector3 centerPos;

	GameObject greenBlast;

	// Use this for initialization
	void Start () {
		greenBlast = (GameObject)Resources.Load ("Prefabs/GreenExplosion", typeof(GameObject));
		centerPos = transform.position;
		if (isStraight)	degreesPerSec = 0;
	}
	
	// Update is called once per frame
	void Update () {
		centerPos += Vector3.up * ySpeed / 100;
		degrees = Mathf.Repeat(degrees + (Time.deltaTime * degreesPerSec), 360.0f);
		var radians = degrees * Mathf.Deg2Rad;

		// Offset by sin wave
		Vector3 offset = new Vector3(amplitude * Mathf.Sin(radians), 0.0f, 0.0f);
		transform.position = centerPos + offset;
	}

	void OnCollisionEnter(Collision col){
		if(col.gameObject.tag == "Red"){
			//Create the green Sphere
			var sphere = (GameObject)Instantiate(greenBlast, transform.position, greenBlast.transform.rotation);
			//sphere.renderer.material = empBlast;
			sphere.transform.position = transform.position;
			//sphere.transform.localEulerAngles = new Vector3(0, 110.0788f, 0);
			sphere.transform.localScale = new Vector3(sphereRadius/10, sphereRadius/10, sphereRadius/10);
			var dis = sphere.AddComponent<EnableDisable>();
			dis.empDuration = empDuration;
			dis.sphereRadius = sphereRadius;
			Destroy(sphere.collider);
			Destroy (sphere, 0.5f);
		}
        col.gameObject.BroadcastMessage("OnHit", new WeaponDamage { tag = tag, damage = damage, hitLocation = col.contacts[0].point }, SendMessageOptions.DontRequireReceiver);
		Destroy (gameObject);
	}
}