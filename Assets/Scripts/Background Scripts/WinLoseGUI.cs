using UnityEngine;

public class WinLoseGUI : MonoBehaviour {

	public bool win = false;
	public bool displaying = false;
	
	private string message;
	
	public void Start() {
		if (win) {
			if (GameObject.Find("WaveSpawner").GetComponent<Spawner>().level < Spawner.MAX_LEVELS) {
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
			var spawner = GameObject.Find("WaveSpawner").GetComponent<Spawner>();
			if (win) {
				spawner.level++;
				if (spawner.level > Spawner.MAX_LEVELS) {
					Application.LoadLevel("Credits");
					return;
				}
				var driver = GameObject.Find("Arcus v1").GetComponent<MainCharacterDriver>();
				driver.gameOver = false;
				driver.timeToWinCounter = driver.timeToWin;
				
				spawner.Start();
				Destroy(this);
			} else {
				Application.LoadLevel(Application.loadedLevel);
			}
		} else if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel("MainMenu");
		}
	}
}
