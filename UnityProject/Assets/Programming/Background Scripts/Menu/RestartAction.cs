using UnityEngine;

public class RestartAction : MenuAction
{
    public override void TakeAction()
    {
		foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>()) {
			Destroy(go);
		}
		
        Application.LoadLevel(Application.loadedLevel);
    }
}