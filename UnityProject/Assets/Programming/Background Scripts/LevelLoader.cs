using UnityEngine;

public class LevelLoader: MonoBehaviour{
	public static void LoadNextLevel(){
		/*foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>()) {
			Destroy(go);
		}*/
		Application.LoadLevel (Application.loadedLevel+1);
        Time.timeScale = 1.0f;
	}

	public static void RestartLevel(){
		/*foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>()) {
			Destroy(go);
		}*/
		Application.LoadLevel (Application.loadedLevel);
        Time.timeScale = 1.0f;
	}

	public static void LoadLevel(string sceneName){
		/*foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>()) {
			Destroy(go);
		}*/
		Application.LoadLevel (sceneName);
        Time.timeScale = 1.0f;
	}

    public static bool IsLastLevel()
    {
        return (Application.loadedLevel == Application.levelCount - 1);
    }
}