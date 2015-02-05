using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class MenuAction : MonoBehaviour
{
    public bool isInGUI = false;

    protected void OnMouseDown()
    {
        if (!isInGUI)
        {
            TakeAction();
        }
    }

    public virtual void TakeAction()
    {

    }
}
