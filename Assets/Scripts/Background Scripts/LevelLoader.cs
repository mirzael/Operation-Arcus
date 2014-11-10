using UnityEngine;

public class LevelLoader : MonoBehaviour {

	public void Update() {
		if (Input.GetKeyDown (KeyCode.R)) {
			var enemies = GameObject.FindObjectsOfType(typeof(GameObject));
			foreach (GameObject go in enemies) {
				if (go.name.StartsWith("Enemy")) {
					Destroy(go);
				}
			}
			GameObject.Find("WaveSpawner").GetComponent<Spawner>().Start();
		}
	}
}