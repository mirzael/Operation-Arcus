using UnityEngine;

public class UIDriver : MonoBehaviour {
	public GameObject barCenter;
	public GameObject barLeft;
	public GameObject barRight;
	
	public GameObject powerCenter;
	public GameObject powerLeft;
	public GameObject powerRight;
	
	public GameObject secondaryCenter;
	public GameObject secondaryLeft;
	public GameObject secondaryRight;
	
	private int currentColor; // 1 = red; 2 = blue; 3 = yellow;
	
	public Material[] successScreens;
	public Material[] backgrounds;
	
	public Material darkOrange, brightOrange;
	public Material darkPurple, brightPurple;
	public Material darkGreen, brightGreen;
	
	private Vector3 origScaleCenter;
	private Vector3 origScaleLeft;
	private Vector3 origScaleRight;
	
	private GameObject winScreen, loseScreen;
	private bool showingWinLose;
	private bool win;

	public AudioClip loseSound;
	public AudioClip winSound;
	public GameObject introSound;

	PointMaster points;
	private Camera myCamera;

	public GameObject primaryRing;
	public GameObject secondaryRing1;
	public GameObject secondaryRing2;
	
	public void Awake() {
		winScreen = GameObject.Find("WinScreen");
		loseScreen = GameObject.Find("LoseScreen");
		winScreen.SetActive(false);
		loseScreen.SetActive(false);
		showingWinLose = false;
		win = false;
		
		myCamera = transform.Find("Camera").camera;
		
		GameObject.Find("Background").renderer.material = backgrounds[GameObject.Find("WaveSpawner").GetComponent<Spawner>().level - 1];
		
		currentColor = 1;
		
		origScaleCenter = powerCenter.transform.localScale;
		origScaleLeft = powerLeft.transform.localScale;
		origScaleRight = powerRight.transform.localScale;
		
		ShiftAndScale(powerCenter, origScaleCenter, new Vector3(0, 1, 1));
		ShiftAndScale(powerLeft, origScaleLeft, new Vector3(0, 1, 1));
		ShiftAndScale(powerRight, origScaleRight, new Vector3(0, 1, 1));

		
		points = Camera.main.gameObject.GetComponent<PointMaster> ();
	}
	
	public void Update() {
		if (!showingWinLose) {
			return;
		}
		
		if (Input.GetKeyDown(KeyCode.R)) {
			points.enabled = true;
			showingWinLose = false;
			
			var spawner = GameObject.Find("WaveSpawner").GetComponent<Spawner>();
			if (win) {
				spawner.level++;
				if (spawner.lastLevel) {
					PointMaster.points = 0.0f;
					MainCharacterDriver.powerRed = MainCharacterDriver.powerBlue = MainCharacterDriver.powerYellow = 0;
					GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterDriver>().ResetForm();
					Application.LoadLevel("Credits");
					return;
				}
				
				GameObject.Find("Background").renderer.material = backgrounds[spawner.level - 1];
				winScreen.SetActive(false);
				myCamera.enabled = true;

				var driver = GameObject.Find(MainCharacterDriver.arcusName).GetComponent<MainCharacterDriver>();
				driver.gameOver = false;
				introSound.audio.Play ();
				spawner.NextLevel();
			} else {
				Application.LoadLevel(Application.loadedLevel);
				introSound.audio.Play ();
			}
		} else if (Input.GetKeyDown(KeyCode.Escape)) {
			PointMaster.points = 0;
			MainCharacterDriver.powerRed = MainCharacterDriver.powerBlue = MainCharacterDriver.powerYellow = 0;
			var driver = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterDriver>();
			driver.ResetForm();
			Application.LoadLevel("MainMenu");
		}
	}

	

	public void RotateToBlue(){
		if (currentColor == 1) {
			var tmp = barRight.renderer.material;
			barRight.renderer.material = barLeft.renderer.material;
			barLeft.renderer.material = barCenter.renderer.material;
			barCenter.renderer.material = tmp;

			tmp = powerRight.renderer.material;
			powerRight.renderer.material = powerLeft.renderer.material;
			powerLeft.renderer.material = powerCenter.renderer.material;
			powerCenter.renderer.material = tmp;

			var rTmp = secondaryRing2.renderer.material;
			secondaryRing2.renderer.material = secondaryRing1.renderer.material;
			secondaryRing1.renderer.material = primaryRing.renderer.material;
			primaryRing.renderer.material = tmp;

		} else if (currentColor == 3) {
			var tmp = barLeft.renderer.material;
			barLeft.renderer.material = barRight.renderer.material;
			barRight.renderer.material = barCenter.renderer.material;
			barCenter.renderer.material = tmp;

			tmp = powerLeft.renderer.material;
			powerLeft.renderer.material = powerRight.renderer.material;
			powerRight.renderer.material = powerCenter.renderer.material;
			powerCenter.renderer.material = tmp;

			var rTmp = secondaryRing1.renderer.material;
			secondaryRing1.renderer.material = secondaryRing2.renderer.material;
			secondaryRing2.renderer.material = primaryRing.renderer.material;
			primaryRing.renderer.material = rTmp;
		}

		currentColor = 2;
		UpdateBars ();
	}

	public void RotateToRed(){
		if (currentColor == 2) {
			var tmp = barLeft.renderer.material;
			barLeft.renderer.material = barRight.renderer.material;
			barRight.renderer.material = barCenter.renderer.material;
			barCenter.renderer.material = tmp;

			tmp = powerLeft.renderer.material;
			powerLeft.renderer.material = powerRight.renderer.material;
			powerRight.renderer.material = powerCenter.renderer.material;
			powerCenter.renderer.material = tmp;

			var rTmp = secondaryRing1.renderer.material;
			secondaryRing1.renderer.material = secondaryRing2.renderer.material;
			secondaryRing2.renderer.material = primaryRing.renderer.material;
			primaryRing.renderer.material = rTmp;

		} else if (currentColor == 3) {
			var tmp = barRight.renderer.material;
			barRight.renderer.material = barLeft.renderer.material;
			barLeft.renderer.material = barCenter.renderer.material;
			barCenter.renderer.material = tmp;

			tmp = powerRight.renderer.material;
			powerRight.renderer.material = powerLeft.renderer.material;
			powerLeft.renderer.material = powerCenter.renderer.material;
			powerCenter.renderer.material = tmp;

			var rTmp = secondaryRing2.renderer.material;
			secondaryRing2.renderer.material = secondaryRing1.renderer.material;
			secondaryRing1.renderer.material = primaryRing.renderer.material;
			primaryRing.renderer.material = rTmp;
		}

		currentColor = 1;
		UpdateBars ();
	}

	public void RotateToYellow(){
		if (currentColor == 1) {
			var tmp = barLeft.renderer.material;
			barLeft.renderer.material = barRight.renderer.material;
			barRight.renderer.material = barCenter.renderer.material;
			barCenter.renderer.material = tmp;

			tmp = powerLeft.renderer.material;
			powerLeft.renderer.material = powerRight.renderer.material;
			powerRight.renderer.material = powerCenter.renderer.material;
			powerCenter.renderer.material = tmp;

			var rTmp = secondaryRing1.renderer.material;
			secondaryRing1.renderer.material = secondaryRing2.renderer.material;
			secondaryRing2.renderer.material = primaryRing.renderer.material;
			primaryRing.renderer.material = rTmp;

		} else if (currentColor == 2) {
			var tmp = barRight.renderer.material;
			barRight.renderer.material = barLeft.renderer.material;
			barLeft.renderer.material = barCenter.renderer.material;
			barCenter.renderer.material = tmp;

			tmp = powerRight.renderer.material;
			powerRight.renderer.material = powerLeft.renderer.material;
			powerLeft.renderer.material = powerCenter.renderer.material;
			powerCenter.renderer.material = tmp;

			var rTmp = secondaryRing2.renderer.material;
			secondaryRing2.renderer.material = secondaryRing1.renderer.material;
			secondaryRing1.renderer.material = primaryRing.renderer.material;
			primaryRing.renderer.material = rTmp;
		}

		currentColor = 3;
		UpdateBars ();
	}

	/* CurrentColor = 1
	 * 	Left = Secondary 1 = Yellow
	 * 	Center = Red
	 *	Right = Secondary 2 = Blue 
	 * CurrentColor = 2
	 * 	Left = Secondary 1 = Red
	 *  Center = Blue
	 *  Right = Secondary 2 = Yellow
	 * CurrentColor = 3
	 *  Left = Secondary 1 = Blue
	 *  Center = Yellow
	 *  Right = Secondary 2 = Red 
	 */

	public void UpdateBars() {
	
		Debug.Log ("Updating Colors: RED: " + MainCharacterDriver.powerRed + " BLUE: " + MainCharacterDriver.powerBlue + " YELLOW: " + MainCharacterDriver.powerYellow);

		if (currentColor == 1) {
			ShiftAndScale(powerLeft, origScaleLeft, new Vector3(MainCharacterDriver.powerYellow / 100, 1, 1));
			ShiftAndScale(powerCenter, origScaleCenter, new Vector3(MainCharacterDriver.powerRed / 100, 1, 1));
			ShiftAndScale(powerRight, origScaleRight, new Vector3(MainCharacterDriver.powerBlue / 100, 1, 1));
		} else if (currentColor == 2) {
			ShiftAndScale(powerLeft, origScaleLeft, new Vector3(MainCharacterDriver.powerRed / 100, 1, 1));
			ShiftAndScale(powerCenter, origScaleCenter, new Vector3(MainCharacterDriver.powerBlue / 100, 1, 1));
			ShiftAndScale(powerRight, origScaleRight, new Vector3(MainCharacterDriver.powerYellow / 100, 1, 1));
		} else if (currentColor == 3) {
			ShiftAndScale(powerLeft, origScaleLeft, new Vector3(MainCharacterDriver.powerBlue / 100, 1, 1));
			ShiftAndScale(powerCenter, origScaleCenter, new Vector3(MainCharacterDriver.powerYellow / 100, 1, 1));
			ShiftAndScale(powerRight, origScaleRight, new Vector3(MainCharacterDriver.powerRed / 100, 1, 1));
		} else {
			ShiftAndScale(powerLeft, origScaleLeft, new Vector3(MainCharacterDriver.powerYellow / 100, 1, 1));
			ShiftAndScale(powerCenter, origScaleCenter, new Vector3(MainCharacterDriver.powerRed / 100, 1, 1));
			ShiftAndScale(powerRight, origScaleRight, new Vector3(MainCharacterDriver.powerBlue / 100, 1, 1));
		}
		
		float transformAmount = MainCharacterDriver.TRANSFORM_AMOUNT;
		if (MainCharacterDriver.powerRed >= transformAmount && MainCharacterDriver.powerBlue >= transformAmount) {
			secondaryCenter.renderer.material = brightPurple;
		} else {
			secondaryCenter.renderer.material = darkPurple;
		}
		if (MainCharacterDriver.powerRed >= transformAmount && MainCharacterDriver.powerYellow >= transformAmount) {
			secondaryLeft.renderer.material = brightOrange;
		} else {
			secondaryLeft.renderer.material = darkOrange;
		}
		if (MainCharacterDriver.powerYellow >= transformAmount && MainCharacterDriver.powerBlue >= transformAmount) {
			secondaryRight.renderer.material = brightGreen;
		} else {
			secondaryRight.renderer.material = darkGreen;
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
		introSound.audio.Stop();
		points.enabled = false;
		int level = GameObject.Find("WaveSpawner").GetComponent<Spawner>().level;
		winScreen.SetActive(true);
		myCamera.enabled = false;
		
		if (level <= Spawner.MAX_LEVELS) {
			winScreen.renderer.material = successScreens[level - 1];
			if (level == successScreens.Length)
			{
				introSound.audio.Stop();
				audio.PlayOneShot (winSound);
			}
		}
		showingWinLose = true;
		win = true;

	}
	
	public void ShowLoseScreen() {
		introSound.audio.Stop ();
		points.enabled = false;
		Destroy (GameObject.FindGameObjectWithTag ("SoundBox"));
		audio.volume = 0.1f;
		audio.PlayOneShot (loseSound);
		loseScreen.SetActive(true);
		myCamera.enabled = false;
		showingWinLose = true;
		win = false;
	}
}
