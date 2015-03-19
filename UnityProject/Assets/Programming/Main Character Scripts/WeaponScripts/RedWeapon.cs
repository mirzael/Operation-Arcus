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
		var radius = ColorPower.Instance.powerRed * radiusPerPoint + baseExplosionRadius;
		foreach(Collider collider in Physics.OverlapSphere(col.gameObject.transform.position, radius)){
			collider.gameObject.BroadcastMessage("OnHit", new WeaponDamage { tag = tag, damage = this.damage, hitLocation = col.contacts[0].point }, SendMessageOptions.DontRequireReceiver);
		}

		Destroy (gameObject);
	}
}
