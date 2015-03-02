using UnityEngine;

public class RestartAction : MenuAction
{
    public override void TakeAction()
    {
		LevelLoader.RestartLevel ();
    }
}