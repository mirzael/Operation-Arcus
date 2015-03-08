using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BackMusicStopper : MonoBehaviour
{
    protected void Start()
    {
        BackgroundUI.Instance.AddGameEndEvent(delegate()
        {
            audio.Stop();
        });
    }
}

