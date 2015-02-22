using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class KillSelf : MonoBehaviour
{
    public bool removeChildren = true;
    public float timeToKill = 1.0f;
    protected void Awake()
    {
        Invoke("KillMe", timeToKill);
    }

    public void KillMe()
    {
        if (removeChildren)
        {
            foreach (Transform child in transform.GetComponentsInChildren<Transform>(true))
            {
                if (child.parent == transform)
                {
                    child.parent = null;
                }
            }
        }
        GameObject.Destroy(gameObject);
    }
}

