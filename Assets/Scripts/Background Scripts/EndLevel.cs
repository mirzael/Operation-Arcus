using UnityEngine;

public class EndLevel : MonoBehaviour {
	public void Update() {
		var enemies = GameObject.FindObjectsOfType(typeof(GameObject));
		foreach (GameObject go in enemies) {
			if (go.layer == LayerMask.NameToLayer("Enemy")) {
				return;
			}
		}
		
		var driver = GameObject.Find(MainCharacterDriver.arcusName).GetComponent<MainCharacterDriver>();
		driver.gameOver = true;
		driver.uiDriver.ShowWinScreen();
		
		Destroy(this);
	}
}