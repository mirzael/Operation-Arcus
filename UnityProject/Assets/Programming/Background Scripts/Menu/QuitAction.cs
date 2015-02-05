using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class QuitAction : MenuAction
{
    public override void TakeAction()
    {
        Application.Quit();
    }
}
