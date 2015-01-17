using UnityEngine;
using MainCharacter;
using System.Collections;

public class BlueWeapon : MonoBehaviour {
	public float damage;
	void OnCollisionEnter(Collision col){
		col.gameObject.BroadcastMessage ("OnHit", new WeaponDamage{tag=tag, damage=damage, hitLocation = col.contacts[0].point});
		Destroy (gameObject);
	}

}
