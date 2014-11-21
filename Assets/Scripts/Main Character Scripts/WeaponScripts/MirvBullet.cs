using UnityEngine;
using System.Collections;

public class MirvBullet : MonoBehaviour {
	
	public float timeBeforeExplosion;
	public float bulletSpeed;
	public float explosionSize;
	Material explosionMat;
	GameObject explosion;
	const float SPHERE_DURATION = 0.5f;

	// Use this for initialization
	void Start () {
		explosionMat = (Material)Resources.Load ("Materials/AoeBlasts/PurpleBlast", typeof(Material));	
		explosion = (GameObject)Resources.Load("Prefabs/ExplosionPurple", typeof(GameObject));
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
			sphere.transform.localScale = new Vector3(explosionSize,explosionSize,explosionSize);
			sphere.tag = "Purple";
			sphere.layer = LayerMask.NameToLayer("Character Bullet");
			Instantiate(explosion, transform.position, transform.rotation);
			Destroy (sphere, SPHERE_DURATION);
			Destroy(gameObject);
		}
	}
}