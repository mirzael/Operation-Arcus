using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLives : MonoBehaviour {
	GUIStyle liveStyle = new GUIStyle();
	public Vector3 pos = new Vector3(-25, 13, 10);
	public Vector2 guiTextPos = new Vector2 (0, 80);
	public Vector2 size  = new Vector2(100, 100);
	public List<GameObject> lives = new List<GameObject>();
	public GameObject arcusModel;
	MainCharacterDriver driver;

	// Use this for initialization
	void Start () {		
		arcusModel.transform.localScale = new Vector3 (3f, 3f, 3f);
		driver = (MainCharacterDriver)GetComponentInChildren (typeof(MainCharacterDriver));
		for (int i = 0; i < driver.lives; i++) {
			var tempPos = pos;
			tempPos += new Vector3(i, 0, 0);
			lives.Add((GameObject)Instantiate (arcusModel, tempPos, arcusModel.transform.rotation));
		}
	}

	// Update is called once per frame
	void OnGUI () {
		liveStyle.normal.textColor = Color.white;
		GUI.Label (new Rect (guiTextPos.x, guiTextPos.y, size.x, size.y), "LIVES:", liveStyle);
	}

	void Update(){
		if (lives.Count == 0) {
			return;
		}
		for(int i = lives.Count; i > driver.lives; i--){
			Destroy (lives[i-1]);
			lives.RemoveAt(i-1);
		}
	}
}
