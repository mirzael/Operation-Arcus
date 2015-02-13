using UnityEngine;

public class EndLevel : MonoBehaviour {
	private BackgroundUI ui;

	public void Start(){
		ui = Camera.main.GetComponent<BackgroundUI> ();
	}

	public void Update() {
		var enemies = GameObject.FindObjectsOfType(typeof(GameObject));
		foreach (GameObject go in enemies) {
			if (go.layer == LayerMask.NameToLayer("Enemy")) {
				return;
			}
		}
	
		if (MultiplayerController.isMultiplayer) {
			MultiplayerCoordinator.Instance.GameOver ();
		} else {
			var driver = GameObject.Find(MainCharacterDriver.arcusName).GetComponent<MainCharacterDriver>();
			driver.gameOver = true;
		}

		ui.ShowWinScreen ();

		Destroy(this);
	}
}