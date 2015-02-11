using UnityEngine;
using System.Collections.Generic;

public class MultiplayerController : MonoBehaviour {
	public GameObject arcus;
	public GameObject oArcus;
	public GameObject dArcus;
	public static bool isMultiplayer = true;
	
	public void Start() {
		if (isMultiplayer) {
			oArcus.SetActive(true);
			dArcus.SetActive(true);
			arcus.SetActive(false);
		} else {
			arcus.SetActive(true);
			oArcus.SetActive(false);
			dArcus.SetActive(false);
		}
	}
}