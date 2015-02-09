using UnityEngine;

public class RestartAction : MenuAction
{
    public override void TakeAction()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}