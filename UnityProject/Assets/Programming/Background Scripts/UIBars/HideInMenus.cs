using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class HideInMenus : MonoBehaviour
{
    protected void Start()
    {
        BackgroundUI.Instance.AddGameEndEvent(HideSelf);
    }

    public void HideSelf()
    {
        gameObject.SetActive(false);
    }

    protected void OnDestroy()
    {
        if(gameObject.activeSelf)
        {
            //if event has already triggered, don't worry about it
            BackgroundUI UI = BackgroundUI.Instance;
            if(UI!=null)
            {
                BackgroundUI.Instance.RemoveGameEndEvent(HideSelf);
            }
        }
        GameObject.Destroy(gameObject);
    }
}

