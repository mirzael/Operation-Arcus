using UnityEngine;

public class Hiscores : Singleton<Hiscores> {
	private const int MAX_SCORES = 10;
	public static int latestScore;
	
	public void Awake() {
        string[] names = new string[MAX_SCORES];
        int[] scores = new int[MAX_SCORES];
        
        for (int i = 1; i <= MAX_SCORES; i++) {
            if (PlayerPrefs.HasKey("Score" + i)) {
                names[i - 1] = PlayerPrefs.GetString("Player" + i);
                scores[i - 1] = PlayerPrefs.GetInt("Score" + i);
            } else {
                names[i - 1] = "Player" + i;
                scores[i - 1] = 0;
            }
        }
        
		PlayerPrefs.DeleteAll ();
		for (int i = 1; i <= MAX_SCORES; i++) {
            int score = scores[i - 1];
			PlayerPrefs.SetString("Player" + i, names[i - 1]);
            PlayerPrefs.SetInt("Score" + i, score);
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