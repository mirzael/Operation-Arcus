using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class WinArcusParent : MonoBehaviour
{
    protected void Awake()
    {
        ParentUnderArcus();
        //kill the main camera
        Transform camTransform = Camera.main.transform;
        AudioListener.Destroy(camTransform.GetComponent<AudioListener>());
        GUILayer.Destroy(camTransform.GetComponent<GUILayer>());
        Behaviour.Destroy(camTransform.GetComponent("FlareLayer"));
        Camera.Destroy(camTransform.GetComponent<Camera>());
        
        /*foreach(Transform child in camTransform.GetComponentsInChildren<Transform>(true))
        {
            if(child.parent==camTransform)
            {
                child.parent = null;
            }
        }
        GameObject.Destroy(camTransform.gameObject);*/
    }

    public void ParentUnderArcus()
    {
        Transform arcus = GameObject.FindObjectOfType<UIDriver>().transform;
        transform.position = arcus.position;
        transform.parent = arcus;
    }
}

