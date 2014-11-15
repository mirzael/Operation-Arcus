using UnityEngine;

public class UIDriver : MonoBehaviour {
	public GameObject barCenter;
	public GameObject barLeft;
	public GameObject barRight;
	
	public GameObject powerCenter;
	public GameObject powerLeft;
	public GameObject powerRight;
	
	private int currentColor; // 1 = red; 2 = blue; 3 = yellow;
	
	private Vector3 origScaleCenter;
	private Vector3 origScaleLeft;
	private Vector3 origScaleRight;
	
	private GameObject winScreen, loseScreen;
	private bool showingWinLose;
	private bool win;
	
	public void Start() {
		winScreen = GameObject.Find("WinScreen");
		loseScreen = GameObject.Find("LoseScreen");
		winScreen.SetActive(false);
		loseScreen.SetActive(false);
		showingWinLose = false;
		win = false;
		
		currentColor = 1;
		
		origScaleCenter = powerCenter.transform.localScale;
		origScaleLeft = powerLeft.transform.localScale;
		origScaleRight = powerRight.transform.localScale;
		
		ShiftAndScale(powerCenter, origScaleCenter, new Vector3(0, 1, 1));
		ShiftAndScale(powerLeft, origScaleLeft, new Vector3(0, 1, 1));
		ShiftAndScale(powerRight, origScaleRight, new Vector3(0, 1, 1));
	}
	
	public void Update() {
		if (!showingWinLose) {
			return;
		}
		
		if (Input.GetKeyDown(KeyCode.R)) {
			Debug.Log("Restarting game");
			var spawner = GameObject.Find("WaveSpawner").GetComponent<Spawner>();
			if (win) {
				spawner.level++;
				if (spawner.level > Spawner.MAX_LEVELS) {
					Application.LoadLevel("Credits");
					return;
				}
				winScreen.SetActive(false);
				
				var driver = GameObject.Find(MainCharacterDriver.arcusName).GetComponent<MainCharacterDriver>();
				driver.gameOver = false;
				spawner.Start();
			} else {
				Application.LoadLevel(Application.loadedLevel);
			}
		} else if (Input.GetKeyDown(KeyCode.Escape)) {
			Application.LoadLevel("MainMenu");
		}
	}
	
	public void RotateLeft() {
		var tmp = barCenter.renderer.material;
		barCenter.renderer.material = barRight.renderer.material;
		barRight.renderer.material = barLeft.renderer.material;
		barLeft.renderer.material = tmp;
		
		tmp = powerCenter.renderer.material;
		powerCenter.renderer.material = powerRight.renderer.material;
		powerRight.renderer.material = powerLeft.renderer.material;
		powerLeft.renderer.material = tmp;
		
		currentColor++;
		if (currentColor > 3) {
			currentColor = 1;
		}
		
		UpdateBars();
	}
	
	public void RotateRight() {
		var tmp = barCenter.renderer.material;
		barCenter.renderer.material = barLeft.renderer.material;
		barLeft.renderer.material = barRight.renderer.material;
		barRight.renderer.material = tmp;
		
		tmp = powerCenter.renderer.material;
		powerCenter.renderer.material = powerLeft.renderer.material;
		powerLeft.renderer.material = powerRight.renderer.material;
		powerRight.renderer.material = tmp;
		
		currentColor--;
		if (currentColor < 1) {
			currentColor = 3;
		}
		
		UpdateBars();
	}
	
	public void UpdateBars() {
		var driver = GameObject.Find(MainCharacterDriver.arcusName).GetComponent<MainCharacterDriver>();
		
		if (currentColor == 1) {
			ShiftAndScale(powerLeft, origScaleLeft, new Vector3(driver.powerYellow / 100, 1, 1));
			ShiftAndScale(powerCenter, origScaleCenter, new Vector3(driver.powerRed / 100, 1, 1));
			ShiftAndScale(powerRight, origScaleRight, new Vector3(driver.powerBlue / 100, 1, 1));
		} else if (currentColor == 2) {
			ShiftAndScale(powerLeft, origScaleLeft, new Vector3(driver.powerRed / 100, 1, 1));
			ShiftAndScale(powerCenter, origScaleCenter, new Vector3(driver.powerBlue / 100, 1, 1));
			ShiftAndScale(powerRight, origScaleRight, new Vector3(driver.powerYellow / 100, 1, 1));
		} else if (currentColor == 3) {
			ShiftAndScale(powerLeft, origScaleLeft, new Vector3(driver.powerBlue / 100, 1, 1));
			ShiftAndScale(powerCenter, origScaleCenter, new Vector3(driver.powerYellow / 100, 1, 1));
			ShiftAndScale(powerRight, origScaleRight, new Vector3(driver.powerRed / 100, 1, 1));
		} else {
			ShiftAndScale(powerLeft, origScaleLeft, new Vector3(driver.powerYellow / 100, 1, 1));
			ShiftAndScale(powerCenter, origScaleCenter, new Vector3(driver.powerRed / 100, 1, 1));
			ShiftAndScale(powerRight, origScaleRight, new Vector3(driver.powerBlue / 100, 1, 1));
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
	
	public void ShowWinScreen() {
		winScreen.SetActive(true);
		showingWinLose = true;
		win = true;
	}
	
	public void ShowLoseScreen() {
		loseScreen.SetActive(true);
		showingWinLose = true;
		win = false;
	}
}
