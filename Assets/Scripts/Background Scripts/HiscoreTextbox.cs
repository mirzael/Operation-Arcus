using UnityEngine;

public class HiscoreTextbox : MonoBehaviour {
	private string username = "";
	public bool submitted = false;
	
	public void Awake() {
		if (!Hiscores.LatestScoreIsHiscore()) {
			submitted = true;
		}
	}
	
	public void OnGUI () {
		if (!submitted) {
			GUI.Label(new Rect(350, 50, 250, 25), "Hiscore! Type your name then press Enter");
			username = GUI.TextField(new Rect(350, 70, 250, 25), username, 40);
			
			if (Event.current.keyCode == KeyCode.Return && username.Length > 0) {
				Debug.Log("Saving score for " + username);
				Hiscores.SaveScore(username, Hiscores.latestScore);
				username = "";
				submitted = true;
			}
		}
	}
}