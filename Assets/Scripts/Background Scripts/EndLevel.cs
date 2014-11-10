using UnityEngine;

public class EndLevel : MonoBehaviour {
	public void Update() {
		var enemies = GameObject.FindObjectsOfType(typeof(GameObject));
		foreach (GameObject go in enemies) {
			if (go.name.StartsWith("Enemy") || go.name.StartsWith("R") || go.name.StartsWith("Y") || go.name.StartsWith("B")) {
				// only counts as enemy if it is the base object (UI has some parts with conflicting names)
				if (go.transform.parent == null) return;
			}
		}
		WinLoseGUI gui = GameObject.Find("Main Camera").AddComponent<WinLoseGUI>();
		gui.win = true;
		GameObject.Find("Arcus v1").GetComponent<MainCharacterDriver>().gameOver = true;
		
		Destroy(this);
	}
}