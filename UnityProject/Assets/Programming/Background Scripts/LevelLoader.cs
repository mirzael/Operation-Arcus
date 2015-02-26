using UnityEngine;

public class LevelLoader: MonoBehaviour{
	public static void LoadNextLevel(){
		foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>()) {
			Destroy(go);
		}
		Application.LoadLevel (Application.loadedLevel+1);
	}

	public static void RestartLevel(){
		foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>()) {
			Destroy(go);
		}
		Application.LoadLevel (Application.loadedLevel);
	}

	public static void LoadLevel(string sceneName){
		foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>()) {
			Destroy(go);
		}
		Application.LoadLevel (sceneName);
	}

    public static bool IsLastLevel()
    {
        return (Application.loadedLevel == Application.levelCount - 1);
    }
}