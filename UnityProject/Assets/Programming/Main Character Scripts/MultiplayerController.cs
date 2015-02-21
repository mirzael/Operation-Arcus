using UnityEngine;
using System.Collections.Generic;

public class MultiplayerController : Singleton<MultiplayerController> {
	public GameObject singePlayer;
	public GameObject multiPlayer;

    public bool isMultiplayer = true;
	
	public void Start() {
        singePlayer.SetActive(!isMultiplayer);
        multiPlayer.SetActive(isMultiplayer);
	}
}