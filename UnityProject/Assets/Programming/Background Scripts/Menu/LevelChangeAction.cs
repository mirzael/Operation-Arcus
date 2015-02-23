using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class LevelChangeAction : MenuAction
{
    public string nextLevel;

    public override void TakeAction()
    {
        ChangeLevel();
    }

    public void ChangeLevel()
    {
		foreach (GameObject go in GameObject.FindObjectsOfType<GameObject>()) {
			Destroy(go);
		}
		
        Application.LoadLevel(nextLevel);
    }
}
