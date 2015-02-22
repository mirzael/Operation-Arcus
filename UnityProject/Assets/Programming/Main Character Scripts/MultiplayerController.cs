using UnityEngine;
using System.Collections.Generic;

public class MultiplayerController : Singleton<MultiplayerController> {
	public GameObject singePlayer;
	public GameObject multiPlayer;

    public bool isMultiplayer = true;
    public static bool? globalIsMultiplayer = null;
	
	public void Start() {
        if(globalIsMultiplayer!=null)
        {
            isMultiplayer = (bool) globalIsMultiplayer;
        }
        singePlayer.SetActive(!isMultiplayer);
        multiPlayer.SetActive(isMultiplayer);
	}
}