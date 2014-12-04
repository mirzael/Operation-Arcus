using UnityEngine;

public class Hiscores : MonoBehaviour {
	private const int MAX_SCORES = 10;
	public static int latestScore;
	
	public void Awake() {
		for (int i = 1; i <= MAX_SCORES; i++) {
			if (!PlayerPrefs.HasKey("Score" + i)) {
				PlayerPrefs.SetInt("Score" + i, 0);
				PlayerPrefs.SetString("Player" + i, "Player" + i);
			}
			int score = PlayerPrefs.GetInt("Score" + i);
			if (score > 0) {
				string name = PlayerPrefs.GetString("Player" + i);
				GameObject.Find("Player" + i).GetComponent<TextMesh>().text = name + ": " + score;
			} else {
				GameObject.Find("Player" + i).GetComponent<TextMesh>().text = "";
			}
		}
	}
	
	public static void SaveScore(string name, int score) {
		for (int i = 1; i <= MAX_SCORES; i++) {
			int s = PlayerPrefs.GetInt("Score" + i);
			if (score >= s) {
				for (int j = MAX_SCORES; j > i; j--) {
					PlayerPrefs.SetInt("Score" + j, PlayerPrefs.GetInt("Score" + (j - 1)));
					PlayerPrefs.SetString("Player" + j, PlayerPrefs.GetString("Player" + (j - 1)));
				}
				PlayerPrefs.SetInt("Score" + i, score);
				PlayerPrefs.SetString("Player" + i, name);
				PlayerPrefs.Save();
				return;
			}
		}
	}
	
	public static bool LatestScoreIsHiscore() {
		return latestScore > 0 && latestScore >= PlayerPrefs.GetInt("Score" + MAX_SCORES);
	}
}