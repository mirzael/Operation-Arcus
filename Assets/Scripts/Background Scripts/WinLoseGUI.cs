using UnityEngine;

public class WinLoseGUI : MonoBehaviour {

	public bool win = false;
	public bool displaying = false;
	
	private string message;
	
	public void Start() {
		if (win) {
			if (Spawner.level < Spawner.MAX_LEVELS) {
				message = "You the battle! Press R to continue";
			} else {
				message = "You won the war! Press R to continue";
			}
		} else {
			message = "You lost! Press R to play again, or ESC to quit";
		}
	}

	public void OnGUI() {
		GUIStyle guiStyle = new GUIStyle(GUI.skin.label);
		guiStyle.normal.textColor = Color.white;
		guiStyle.alignment = TextAnchor.UpperCenter;
		Rect textArea = new Rect(0, 0, Screen.width, Screen.height);
		GUI.Label(textArea, message, guiStyle);
		displaying = true;
	}

	public void Update() {
		if (!displaying) {
			return;
		}
		
		if (Input.GetKeyDown(KeyCode.R)) {
			Debug.Log("Restarting game");
			if (win) {
				Spawner.level++;
				if (Spawner.level > Spawner.MAX_LEVELS) {
					Application.LoadLevel("Credits");
					return;
				}
			} else {
				Spawner.level = 1;
			}
			Application.LoadLevel(Application.loadedLevel);
		} else if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel("MainMenu");
		}
	}
}
