using UnityEngine;

public class RestartAction : MenuAction
{
    public override void TakeAction()
    {
		foreach (GameObject o in Object.FindObjectsOfType<GameObject>()) {
			Destroy(o);
		}
		
        Application.LoadLevel(Application.loadedLevel);
    }
}