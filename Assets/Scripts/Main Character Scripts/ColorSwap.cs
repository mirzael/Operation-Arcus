using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ColorSwap : MonoBehaviour {

	public List<Material> mats;
	public GameObject swappedObj;
	int selectedIndex = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Q)) {
			selectedIndex = selectedIndex-1 <= 0 ? 0 : selectedIndex-1;
			swappedObj.renderer.material = mats [selectedIndex];
		} else if (Input.GetKeyDown(KeyCode.E)) {
			selectedIndex = selectedIndex+1 >= mats.Count ? mats.Count-1 : selectedIndex+1;
			swappedObj.renderer.material = mats[selectedIndex];
		}
	}
}
