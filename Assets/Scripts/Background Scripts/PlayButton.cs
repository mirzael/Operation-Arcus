using UnityEngine;

public class PlayButton : MonoBehaviour {
	private GameObject mainMenu;
	private GameObject hiscore;
	private GameObject intro1;
	private GameObject intro2;
	private GameObject intro3;
	private GameObject intro4;
	
	public void Awake() {
		mainMenu = GameObject.Find("MainMenu");
		if (mainMenu == null) {
			return;
		}
		hiscore = GameObject.Find("Hiscores");
		intro1 = GameObject.Find("Intro1");
		intro2 = GameObject.Find("Intro2");
		intro3 = GameObject.Find("Intro3");
		intro4 = GameObject.Find("Intro4");
	}
	
	public void Start() {
		if (!transform.parent.gameObject.name.Equals("Hiscores")) {
			hiscore.SetActive(false);
		}
		if (!transform.parent.gameObject.name.Equals("Intro1")) {
			intro1.SetActive(false);
		}
		if (!transform.parent.gameObject.name.Equals("Intro2")) {
			intro2.SetActive(false);
		}
		if (!transform.parent.gameObject.name.Equals("Intro3")) {
			intro3.SetActive(false);
		}
		if (!transform.parent.gameObject.name.Equals("Intro4")) {
			intro4.SetActive(false);
		}
	}
	
	public void OnMouseDown() {
		switch (gameObject.name) {
		case "btnPlay":
			Application.LoadLevel("TechDemo");
			break;
		case "btnIntro":
			Debug.Log("Displaying Intro");
			mainMenu.SetActive(false);
			intro1.SetActive(true);
			break;
		case "btnNext2":
			intro1.SetActive(false);
			intro2.SetActive(true);
			break;
		case "btnNext3":
			intro2.SetActive(false);
			intro3.SetActive(true);
			break;
		case "btnNext4":
			intro3.SetActive(false);
			intro4.SetActive(true);
			break;
		case "btnBack1":
			intro2.SetActive(false);
			intro1.SetActive(true);
			break;
		case "btnBack2":
			intro3.SetActive(false);
			intro2.SetActive(true);
			break;
		case "btnBack3":
			intro4.SetActive(false);
			intro3.SetActive(true);
			break;
		case "btnSkip":
			transform.parent.gameObject.SetActive(false);
			intro4.SetActive(true);
			break;
		case "btnHiscore":
			Debug.Log("Displaying Hiscores");
			mainMenu.SetActive(false);
			hiscore.SetActive(true);
			break;
		case "btnCredits":
			Application.LoadLevel("Credits");
			break;
		case "btnQuit":
			Application.Quit();
			break;
		case "btnReturn":
			Application.LoadLevel("MainMenu");
			break;
		case "btnBack":
			transform.parent.gameObject.SetActive(false);
			mainMenu.SetActive(true);
			break;
		}
	}
}