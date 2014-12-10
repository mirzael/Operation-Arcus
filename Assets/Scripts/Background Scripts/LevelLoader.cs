using UnityEngine;

public class LevelLoader : MonoBehaviour {

	public void Update() {
		if (Input.GetKeyDown (KeyCode.R)) {
			var enemies = GameObject.FindObjectsOfType(typeof(GameObject));
			foreach (GameObject go in enemies) {
				if (go.name.StartsWith("Enemy") || go.name.StartsWith("R") || go.name.StartsWith("Y") || go.name.StartsWith("B")) {
					Destroy(go);
				}
			}
			GameObject.Find("MainMusic").GetComponent<AudioSource>().audio.Play();
			GameObject.Find("WaveSpawner").GetComponent<Spawner>().Start();
		}
	}
}