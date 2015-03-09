using UnityEngine;
using System.Collections;

public class LevelChanger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.F1)){
			LevelLoader.LoadLevel("Level1");
		}else if(Input.GetKeyDown(KeyCode.F2)){
			LevelLoader.LoadLevel("Level2");
		}else if(Input.GetKeyDown(KeyCode.F3)){
			LevelLoader.LoadLevel("Level3");
		}else if(Input.GetKeyDown(KeyCode.F4)){
			LevelLoader.LoadLevel("Level4");
		}else if(Input.GetKeyDown(KeyCode.F5)){
			LevelLoader.LoadLevel("Level5");
		}else if(Input.GetKeyDown(KeyCode.F6)){
			LevelLoader.LoadLevel("Level6");
		}
	}
}
