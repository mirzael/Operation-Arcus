using UnityEngine;
using System.Collections;

public class MirvBullet : MonoBehaviour {
	
	public float timeBeforeExplosion;
	public float bulletSpeed;
	Material explosionMat;
	const float SPHERE_DURATION = 0.5f;
	const float SPHERE_RADIUS = 1f;

	// Use this for initialization
	void Start () {
		explosionMat = (Material)Resources.Load ("Materials/AoeBlasts/PurpleBlast", typeof(Material));	
	}
	
	// Update is called once per frame
	void Update () {
		timeBeforeExplosion -= Time.deltaTime;
		if (timeBeforeExplosion >= 0) {
			transform.position += transform.up * bulletSpeed / 100;
		} else {
			var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sphere.renderer.material = explosionMat;
			sphere.transform.position = transform.position;
			sphere.transform.localScale = new Vector3(SPHERE_RADIUS,SPHERE_RADIUS,SPHERE_RADIUS);
			sphere.tag = "Purple";
			sphere.layer = LayerMask.NameToLayer("Character Bullet");
			Destroy (sphere, SPHERE_DURATION);
			Destroy(gameObject);
		}
	}
}
