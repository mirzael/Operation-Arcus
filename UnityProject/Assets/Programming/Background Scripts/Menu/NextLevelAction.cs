using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class NextLevelAction : MenuAction
{
    public override void TakeAction()
    {
        Application.LoadLevel(Application.loadedLevel + 1);
    }
}
