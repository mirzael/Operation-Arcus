using UnityEngine;
using System.Collections;

public class ColorBarDrawer : MonoBehaviour {

	GUIStyle style;
	public Vector2 pos = new Vector2(0,0);
	public Vector2 size  = new Vector2(60,20);
	public Texture2D barEmpty;
	public Texture2D redBarFull;
	public Texture2D blueBarFull;
	public Texture2D yellowBarFull;
	MainCharacterDriver driver;

	void Awake(){
		style = new GUIStyle();
		style.normal.textColor = Color.white;
		style.stretchHeight = true;
		style.stretchWidth = true;
		driver = (MainCharacterDriver)GetComponentInChildren (typeof(MainCharacterDriver));
	}

	void OnGUI()
	{
		DrawBar (pos, driver.powerRed/100, redBarFull);
		DrawBar (new Vector2 (pos.x, pos.y + size.y), driver.powerBlue / 100, blueBarFull);
		DrawBar (new Vector2 (pos.x, pos.y + size.y * 2), driver.powerYellow / 100, yellowBarFull);
		if (driver.invulnCounter > 0) {
			GUI.Label(new Rect(100,100, size.x, size.y), "INVULNERABLE", style);
		}
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
