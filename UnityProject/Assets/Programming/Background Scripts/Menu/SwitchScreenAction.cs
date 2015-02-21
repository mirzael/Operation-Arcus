using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class SwitchScreenAction : MenuAction
{
    public GameObject toHide;
    public GameObject toShow;

    public Transform earthModel;
    public bool makeSmaller;
    private float scaleAmount = 2.5f;

    public override void TakeAction()
    {
        toShow.SetActive(true);
        toHide.SetActive(false);

        if(earthModel!=null)
        {
            float toScale;
            if(makeSmaller)
            {
                toScale = 1f / scaleAmount;
            }
            else
            {
                toScale = scaleAmount;
            }
            earthModel.localScale *= toScale;
        }
    }
}
