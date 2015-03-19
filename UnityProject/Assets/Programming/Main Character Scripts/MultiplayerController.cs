using UnityEngine;
using System.Collections.Generic;

public class MultiplayerController : Singleton<MultiplayerController> {
	public GameObject singePlayer;
	public GameObject multiPlayer;

    public bool isMultiplayer = true;
    private static bool? globalIsMultiplayer = null;
	
    public static void SetMultiplayer(bool isMultiplayer)
    {
        //this.isMultiplayer = isMultiplayer;
        globalIsMultiplayer = isMultiplayer;
    }

    public static bool GetIsMultiplayer()
    {
        return globalIsMultiplayer ?? true;
    }

	public void Awake() {
        if(globalIsMultiplayer!=null)
        {
            isMultiplayer = (bool) globalIsMultiplayer;
        }
        else
        {
            globalIsMultiplayer = isMultiplayer;
        }
	}

    public void Start()
    {
        singePlayer.SetActive(!isMultiplayer);
        multiPlayer.SetActive(isMultiplayer);
    }
}