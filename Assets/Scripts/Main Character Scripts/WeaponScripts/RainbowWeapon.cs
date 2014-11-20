using UnityEngine;
using MainCharacter;
using System.Collections;

public class RainbowWeapon : MonoBehaviour {
	public float damage;
	
	public void Start() {
		Color newColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), 1.0f);
		renderer.material.color = newColor;
	}
	
	void OnCollisionEnter(Collision col){
		col.gameObject.BroadcastMessage ("OnHit", new WeaponDamage{tag=tag, damage=damage});
		Destroy (gameObject);
	}

}
