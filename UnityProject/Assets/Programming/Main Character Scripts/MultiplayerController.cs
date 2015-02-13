using UnityEngine;
using System.Collections.Generic;

public class MultiplayerController : MonoBehaviour {
	public GameObject singePlayer;
	public GameObject multiPlayer;

    public static bool isMultiplayer = true;
	
	public void Start() {
        singePlayer.SetActive(!isMultiplayer);
        multiPlayer.SetActive(isMultiplayer);
	}
}