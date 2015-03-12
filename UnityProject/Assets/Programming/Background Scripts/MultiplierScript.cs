using UnityEngine;
using System.Collections;

public class MultiplierScript : MonoBehaviour {
	public float enemyHealthMultiplier;
	public float enemyCooldownMultiplier;
	public float playerDamageMultipler;

	public void Start(){
		if (MultiplayerController.Instance.isMultiplayer) {
			enemyHealthMultiplier *= 2;
		}
	}
}
