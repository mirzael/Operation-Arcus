using UnityEngine;

public class PlayButton : MonoBehaviour {
	private GameObject mainMenu;
	private GameObject hiscore;
	private GameObject instructions;
	private GameObject intro;
	
	public void Awake() {
		mainMenu = GameObject.Find("MainMenu");
		hiscore = GameObject.Find("Hiscores");
		instructions = GameObject.Find("Instructions");
		intro = GameObject.Find("Intro");
	}
	
	public void Start() {
		if (!transform.parent.gameObject.name.Equals("Hiscores")) {
			hiscore.SetActive(false);
		}
		if (!transform.parent.gameObject.name.Equals("Instructions")) {
			instructions.SetActive(false);
		}
		if (!transform.parent.gameObject.name.Equals("Intro")) {
			intro.SetActive(false);
		}
	}
	
	public void OnMouseDown() {
		if (gameObject.name.Equals("btnPlay")) {
			Spawner.level = 1;
			Application.LoadLevel("TechDemo");
		} else if (gameObject.name.Equals("btnIntro")) {
			Debug.Log("Displaying Intro");
			mainMenu.SetActive(false);
			intro.SetActive(true);
		} else if (gameObject.name.Equals("btnInstr")) {
			Debug.Log("Displaying Instructions");
			mainMenu.SetActive(false);
			instructions.SetActive(true);
		} else if (gameObject.name.Equals("btnHiscore")) {
			Debug.Log("Displaying Hiscores");
			mainMenu.SetActive(false);
			hiscore.SetActive(true);
		} else if (gameObject.name.Equals("btnCredits")) {
			Application.LoadLevel("Credits");
		} else if (gameObject.name.Equals("btnQuit")) {
			Application.Quit();
		} else if (gameObject.name.Equals("btnReturn")) {
			Application.LoadLevel("MainMenu");
		} else if (gameObject.name.Equals("btnBack")) {
			transform.parent.gameObject.SetActive(false);
			mainMenu.SetActive(true);
		}
	}
}