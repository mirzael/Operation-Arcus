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
    public Transform smallerModel;
    public Transform largerModel;

    public bool makeSmaller;

    public override void TakeAction()
    {
        toShow.SetActive(true);
        toHide.SetActive(false);

        if(earthModel!=null)
        {
            if(makeSmaller)
            {
                earthModel.position = smallerModel.position;
                earthModel.localScale = smallerModel.localScale;
            }
            else
            {
                earthModel.position = largerModel.position;
                earthModel.localScale = largerModel.localScale;
            }
        }
    }
}
