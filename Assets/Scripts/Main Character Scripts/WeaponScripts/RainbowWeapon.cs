using UnityEngine;
using MainCharacter;
using System.Collections;

public class RainbowWeapon : MonoBehaviour {
	public float damage;

	void OnCollisionEnter(Collision col){
		col.gameObject.BroadcastMessage ("OnHit", new WeaponDamage{tag=tag, damage=damage});
		Destroy (gameObject);
	}

}
