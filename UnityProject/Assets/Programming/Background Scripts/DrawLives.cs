﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLives : MonoBehaviour {
	GUIStyle liveStyle = new GUIStyle();
	public Vector2 guiTextPos = new Vector2 (0, 80);
	public Vector2 size  = new Vector2(100, 100);
	public GameObject innerHealthBar;

	public GameObject arcusModel;
	public Renderer[] healthPortions;

	// Use this for initialization
	void Start () {
		healthPortions = innerHealthBar.GetComponentsInChildren<Renderer> ();
	}

	// Update is called once per frame
	void OnGUI () {
		liveStyle.normal.textColor = Color.white;
	}

	void Update(){
		if (MainCharacterDriver.health > 0) {
			UpdateHealth ();
		}
	}

	private void UpdateHealth(){
		int invisIndex = 10-MainCharacterDriver.health / 10;

		for (int i = healthPortions.Length-1; i >= invisIndex; i--) {
			var tmp = healthPortions[i].material.color;
			tmp.a = 0.8f;
			healthPortions[i].material.color = tmp;
		}
		for (int i = 0; i < invisIndex; i++) {
			var tmp = healthPortions[i].material.color;
			tmp.a = 0f;
			healthPortions[i].material.color = tmp;
		}

	}

	private void ShiftAndScale(GameObject powerBar, Vector3 origScale, Vector3 newScaleRatio) {
		Vector3 curScale = powerBar.transform.localScale;
		Vector3 newScale = new Vector3(origScale.x * newScaleRatio.x, origScale.y * newScaleRatio.y, origScale.z * newScaleRatio.z);
		
		if (newScale.x == curScale.x && newScale.y == curScale.y && newScale.z == curScale.z) {
			return;
		}
		
		float newX = powerBar.transform.position.x - powerBar.renderer.bounds.extents.x;
		float curY = powerBar.transform.position.y;
		float curZ = powerBar.transform.position.z;
		powerBar.transform.position = new Vector3(newX, curY, curZ);
		powerBar.transform.localScale = newScale;
		newX = powerBar.transform.position.x + powerBar.renderer.bounds.extents.x;
		powerBar.transform.position = new Vector3(newX, curY, curZ);
	}
}
