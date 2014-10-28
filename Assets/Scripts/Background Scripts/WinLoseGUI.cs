using UnityEngine;

public class WinLoseGUI : MonoBehaviour {

	public bool win = false;
	public bool displaying = false;

	public void Start() {
		
	}

	public void OnGUI() {
		GUIStyle guiStyle = new GUIStyle(GUI.skin.label);
		guiStyle.normal.textColor = Color.white;
		guiStyle.alignment = TextAnchor.UpperCenter;
		Rect textArea = new Rect(0, 0, Screen.width, Screen.height);
		if (win) {
			GUI.Label(textArea, "You Won! Press R to play again", guiStyle);
		} else {
			GUI.Label(textArea, "You Lost! Press R to play again", guiStyle);
		}
		displaying = true;
	}

	public void Update() {
		if (!displaying) {
			return;
		}
		
		if (Input.GetKeyDown(KeyCode.R)) {
			Debug.Log("Restarting game");
			Application.LoadLevel(Application.loadedLevel);
		}
	}
}
