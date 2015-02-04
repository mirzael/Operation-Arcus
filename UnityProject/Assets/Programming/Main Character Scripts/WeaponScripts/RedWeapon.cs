using UnityEngine;
using MainCharacter;
using System.Collections;

public class RedWeapon : MonoBehaviour {
	public float baseExplosionRadius;
	public float radiusPerPoint;
	public float damage;
	GameObject redBlast;
	GameObject explosion;

	// Use this for initialization
	void Start () {
		redBlast = (GameObject)Resources.Load ("Prefabs/RedExplosion", typeof(GameObject));
		explosion = (GameObject)Resources.Load("Prefabs/ExplosionRedMissile", typeof(GameObject));
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision col){
		var exp = (GameObject)Instantiate(explosion, transform.position, transform.rotation);
		exp.particleEmitter.minSize = (ColorPower.Instance.powerRed * radiusPerPoint + baseExplosionRadius) * 0.8f;
		exp.particleEmitter.maxSize = ColorPower.Instance.powerRed * radiusPerPoint + baseExplosionRadius;
		CreateAoe (col.contacts[0].point, ColorPower.Instance.powerRed * radiusPerPoint + baseExplosionRadius, 1f, false);
		if (gameObject.renderer.material != redBlast) {
			col.gameObject.BroadcastMessage ("OnHit", new WeaponDamage{tag=tag, damage=damage+ColorPower.Instance.powerRed/10, hitLocation = col.contacts[0].point});
		}
	}

	void CreateAoe(Vector3 center, float radius, float duration, bool gravity){

		var sphere = (GameObject)Instantiate (redBlast, transform.position, redBlast.transform.rotation);
		sphere.transform.localScale = new Vector3(radius, radius, radius);
		sphere.layer = LayerMask.NameToLayer("Character Bullet");
		sphere.tag = "Red";
		sphere.AddComponent<RedWeapon> ().damage = damage;
		Destroy (sphere, duration);
		Destroy(gameObject);
	}
}
