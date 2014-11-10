using UnityEngine;

public class EndLevel : MonoBehaviour {
	public void Update() {
		var enemies = GameObject.FindObjectsOfType(typeof(GameObject));
		foreach (GameObject go in enemies) {
			if (go.layer == 8) {
				return;
			}
		}
		WinLoseGUI gui = GameObject.Find("Main Camera").AddComponent<WinLoseGUI>();
		gui.win = true;
		GameObject.Find("Arcus v1").GetComponent<MainCharacterDriver>().gameOver = true;
		
		Destroy(this);
	}
}