using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreditsScroll : MonoBehaviour
{
    public Scrollbar scrollBar;

    public float timeToMove = 8f;
    private bool userControlled = false;
    private float timeSoFar;

    protected void Start()
    {
        timeSoFar = 0f;
    }

    public void UserClick()
    {
        Debug.Log("pointer down");
        userControlled = true;
    }

    protected void  Update()
    {
        timeSoFar += Time.deltaTime;
        if(!userControlled)
        {
            float t = Mathf.Clamp(timeSoFar,1, timeToMove);
            float val = 1 - (t / timeToMove);
            scrollBar.value = val;
            Canvas.ForceUpdateCanvases();
        }
    }
}

