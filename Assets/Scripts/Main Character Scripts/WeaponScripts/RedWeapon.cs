using UnityEngine;
using MainCharacter;
using System.Collections;

public class RedWeapon : MonoBehaviour {
	public float baseExplosionRadius;
	public float radiusPerPoint;
	public float damage;
	public MainCharacterDriver driver;

	Material redBlast;
	GameObject explosion;

	// Use this for initialization
	void Start () {
		redBlast = (Material)Resources.Load ("Materials/AoeBlasts/RedBlast", typeof(Material));
		explosion = (GameObject)Resources.Load("Prefabs/ExplosionRedMissile", typeof(GameObject));
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision col){
		Instantiate(explosion, transform.position, transform.rotation);
		CreateAoe (col.contacts [0].point, redBlast, driver.powerRed*radiusPerPoint+baseExplosionRadius, 0.5f, false);
		col.gameObject.BroadcastMessage ("OnHit", new WeaponDamage{tag=tag, damage=damage});
	}

	void CreateAoe(Vector3 center, Material mat, float radius, float duration, bool gravity){
		var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.renderer.material = mat;
		sphere.transform.position = center;
		sphere.transform.localScale = new Vector3(radius, radius, radius);
		sphere.layer = LayerMask.NameToLayer("Character Bullet");
		sphere.AddComponent<RedWeapon> ().damage = damage;
		Destroy (sphere, duration);
		Destroy(gameObject);
	}
}
