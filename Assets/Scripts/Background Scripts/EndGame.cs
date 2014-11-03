using UnityEngine;

public class EndGame : MonoBehaviour {
	public void Update() {
		if (Input.GetKeyDown(KeyCode.Return)) {
			Application.LoadLevel("MainMenu");
		}
	}
}