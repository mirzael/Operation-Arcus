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

		//Send damage to the ships around the ship that was hit
		foreach(RaycastHit hit in Physics.SphereCastAll(transform.position-transform.up*(ColorPower.Instance.powerRed * radiusPerPoint + baseExplosionRadius)*1.1f, (ColorPower.Instance.powerRed * radiusPerPoint + baseExplosionRadius)*1.2f, transform.up)){
			Debug.Log("Hitting: " + hit.collider.gameObject.name);
			hit.collider.gameObject.BroadcastMessage("OnHit", new WeaponDamage { tag = tag, damage = this.damage, hitLocation = col.contacts[0].point }, SendMessageOptions.DontRequireReceiver);
		}

		Destroy (gameObject);
	}
}
