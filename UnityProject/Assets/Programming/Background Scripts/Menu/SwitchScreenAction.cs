using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SwitchScreenAction : MenuAction
{
    public GameObject toHide;
    public GameObject toShow;

    public override void TakeAction()
    {
        toShow.SetActive(true);
        toHide.SetActive(false);
    }
}
