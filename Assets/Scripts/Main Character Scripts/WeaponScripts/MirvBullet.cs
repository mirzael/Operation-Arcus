using UnityEngine;
using System.Collections;

public class MirvBullet : MonoBehaviour {
	
	public float timeBeforeExplosion;
	public float bulletSpeed;
	public float explosionSize;
	GameObject explosionSphere;
	GameObject explosion;
	const float SPHERE_DURATION = 0.5f;

	// Use this for initialization
	void Start () {
		explosionSphere = (GameObject)Resources.Load ("Prefabs/PurpleExplosion", typeof(GameObject));	
		explosion = (GameObject)Resources.Load("Prefabs/ExplosionPurple", typeof(GameObject));
	}
	
	// Update is called once per frame
	void Update () {
		timeBeforeExplosion -= Time.deltaTime;
		if (timeBeforeExplosion >= 0) {
			transform.position += transform.up * bulletSpeed / 100;
		} else {
			var sphere = (GameObject)Instantiate(explosionSphere, transform.position, explosionSphere.transform.rotation);
			sphere.transform.localScale = new Vector3(explosionSize,explosionSize,explosionSize);
			sphere.tag = "Purple";
			sphere.layer = LayerMask.NameToLayer("Character Bullet");
			Instantiate(explosion, transform.position, transform.rotation);
			Destroy (sphere, SPHERE_DURATION);
			Destroy(gameObject);
		}
	}
}