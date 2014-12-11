using UnityEngine;
using MainCharacter;
using System.Collections;

public class YellowWeapon : MonoBehaviour {
	public float damage;
	void OnCollisionEnter(Collision col){
		col.gameObject.BroadcastMessage ("OnHit", new WeaponDamage{tag=tag, damage=damage, hitLocation = col.contacts[0].point});
		Destroy (gameObject);
	}

}
