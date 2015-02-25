using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class StartGameAction : MenuAction
{
    public bool isMultiplayer;

    public override void TakeAction()
    {
        MultiplayerController.globalIsMultiplayer = isMultiplayer;
		LevelLoader.LoadNextLevel ();
    }
}
