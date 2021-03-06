﻿using UnityEngine;
using System.Collections;
using MainCharacter;

public class ColorBarDrawer : MonoBehaviour {

	GUIStyle style;
	public Vector2 pos = new Vector2(0,0);
	public Vector2 size  = new Vector2(60,20);
	public Texture2D barEmpty;
	public Texture2D redBarFull;
	public Texture2D blueBarFull;
	public Texture2D yellowBarFull;

	void Awake(){
		style = new GUIStyle();
		style.normal.textColor = Color.white;
		style.stretchHeight = true;
		style.stretchWidth = true;
	}

	void OnGUI()
	{
		DrawBar (pos, ColorPower.Instance.powerRed/100, redBarFull);
		DrawBar (new Vector2 (pos.x, pos.y + size.y), ColorPower.Instance.powerBlue / 100, blueBarFull);
		DrawBar (new Vector2 (pos.x, pos.y + size.y * 2), ColorPower.Instance.powerYellow / 100, yellowBarFull);
	} 

	void DrawBar(Vector2 pos, float progress, Texture2D barFull){
		// draw the background:
		GUI.BeginGroup (new Rect (pos.x, pos.y, size.x, size.y));
		GUI.Box (new Rect (0,0, size.x, size.y),barEmpty, style);
		
		// draw the filled-in part:
		GUI.BeginGroup (new Rect (0, 0, size.x * progress, size.y));
		GUI.Box (new Rect (0,0, size.x, size.y),barFull, style);
		GUI.EndGroup ();
		
		GUI.EndGroup ();

	}

}
