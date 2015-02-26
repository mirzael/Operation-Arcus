using UnityEngine;

public class WinLoseGUI : MonoBehaviour {

	public bool win = false;
	public bool displaying = false;
	
	private string message;
	
	public void Start() {
		if (win) {
			if (!LevelLoader.IsLastLevel()) {
				message = "You won the battle! Press R to continue";
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
			var spawner = GameObject.Find("WaveSpawner").GetComponent<Spawner>();
			if (win) {
				spawner.level++;
				if (LevelLoader.IsLastLevel()) {
					LevelLoader.LoadLevel("Credits");
					return;
				}
				var driver = GameObject.Find(MainCharacterDriver.arcusName).GetComponent<MainCharacterDriver>();
				driver.gameOver = false;
				spawner.Start();
				Destroy(this);
			} else {
				LevelLoader.RestartLevel();
			}
		} else if (Input.GetKeyDown(KeyCode.Escape)) {
			LevelLoader.LoadLevel("MainMenu");
		}
	}
}
