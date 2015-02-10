using UnityEngine;
using MainCharacter;

public class UIDriver : MonoBehaviour {
	public GameObject barCenter;
	public GameObject barLeft;
	public GameObject barRight;
	
	public GameObject powerCenter;
	public GameObject powerLeft;
	public GameObject powerRight;
	
	private ShipColor currentColor; // 1 = red; 2 = blue; 3 = yellow;
    //public enum ShipColor{BLUE, RED, YELLOW, ORANGE, GREEN, PURPLE, RAINBOW};
	
	public Material successScreen;
	public Material background;

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

	public Color Orange;
	public Color Purple;
	public Color Green;

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
		
		GameObject.Find("Background").renderer.material = background;
		
		currentColor = ShipColor.RED;
		
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
					ColorPower.Instance.powerRed = ColorPower.Instance.powerBlue = ColorPower.Instance.powerYellow = 0;
					GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterDriver>().ResetForm();
					Application.LoadLevel("Credits");
					return;
				}
				
				GameObject.Find("Background").renderer.material = background;
				winScreen.SetActive(false);

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
			ColorPower.Instance.powerRed = ColorPower.Instance.powerBlue = ColorPower.Instance.powerYellow = 0;
			var driver = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterDriver>();
			driver.ResetForm();
			Application.LoadLevel("MainMenu");
		}
	}

	

	public void RotateToBlue(){
		if (currentColor == ShipColor.RED) {
			var tmp = barRight.renderer.material;
			barRight.renderer.material = barLeft.renderer.material;
			barLeft.renderer.material = barCenter.renderer.material;
			barCenter.renderer.material = tmp;

			tmp = powerRight.renderer.material;
			powerRight.renderer.material = powerLeft.renderer.material;
			powerLeft.renderer.material = powerCenter.renderer.material;
			powerCenter.renderer.material = tmp;

		} else if (currentColor == ShipColor.YELLOW) {
			var tmp = barLeft.renderer.material;
			barLeft.renderer.material = barRight.renderer.material;
			barRight.renderer.material = barCenter.renderer.material;
			barCenter.renderer.material = tmp;

			tmp = powerLeft.renderer.material;
			powerLeft.renderer.material = powerRight.renderer.material;
			powerRight.renderer.material = powerCenter.renderer.material;
			powerCenter.renderer.material = tmp;
		}

		currentColor = ShipColor.BLUE;
		UpdateBars ();
	}

	public void RotateToRed(){
		if (currentColor == ShipColor.BLUE) {
			var tmp = barLeft.renderer.material;
			barLeft.renderer.material = barRight.renderer.material;
			barRight.renderer.material = barCenter.renderer.material;
			barCenter.renderer.material = tmp;

			tmp = powerLeft.renderer.material;
			powerLeft.renderer.material = powerRight.renderer.material;
			powerRight.renderer.material = powerCenter.renderer.material;
			powerCenter.renderer.material = tmp;

		} else if (currentColor == ShipColor.YELLOW) {
			var tmp = barRight.renderer.material;
			barRight.renderer.material = barLeft.renderer.material;
			barLeft.renderer.material = barCenter.renderer.material;
			barCenter.renderer.material = tmp;

			tmp = powerRight.renderer.material;
			powerRight.renderer.material = powerLeft.renderer.material;
			powerLeft.renderer.material = powerCenter.renderer.material;
			powerCenter.renderer.material = tmp;
		}

		currentColor = ShipColor.RED;
		UpdateBars ();
	}

	public void RotateToYellow(){
		if (currentColor == ShipColor.RED) {
			var tmp = barLeft.renderer.material;
			barLeft.renderer.material = barRight.renderer.material;
			barRight.renderer.material = barCenter.renderer.material;
			barCenter.renderer.material = tmp;

			tmp = powerLeft.renderer.material;
			powerLeft.renderer.material = powerRight.renderer.material;
			powerRight.renderer.material = powerCenter.renderer.material;
			powerCenter.renderer.material = tmp;
		} else if (currentColor == ShipColor.BLUE) {
			var tmp = barRight.renderer.material;
			barRight.renderer.material = barLeft.renderer.material;
			barLeft.renderer.material = barCenter.renderer.material;
			barCenter.renderer.material = tmp;

			tmp = powerRight.renderer.material;
			powerRight.renderer.material = powerLeft.renderer.material;
			powerLeft.renderer.material = powerCenter.renderer.material;
			powerCenter.renderer.material = tmp;

		}

		currentColor = ShipColor.YELLOW;
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

        BlindBar.Instance.UpdateColorBars();

		if (currentColor == ShipColor.RED) {
			ShiftAndScale(powerLeft, origScaleLeft, new Vector3(ColorPower.Instance.powerYellow / 100, 1, 1));
			ShiftAndScale(powerCenter, origScaleCenter, new Vector3(ColorPower.Instance.powerRed / 100, 1, 1));
			ShiftAndScale(powerRight, origScaleRight, new Vector3(ColorPower.Instance.powerBlue / 100, 1, 1));
		} else if (currentColor == ShipColor.BLUE) {
			ShiftAndScale(powerLeft, origScaleLeft, new Vector3(ColorPower.Instance.powerRed / 100, 1, 1));
			ShiftAndScale(powerCenter, origScaleCenter, new Vector3(ColorPower.Instance.powerBlue / 100, 1, 1));
			ShiftAndScale(powerRight, origScaleRight, new Vector3(ColorPower.Instance.powerYellow / 100, 1, 1));
		} else if (currentColor == ShipColor.YELLOW) {
			ShiftAndScale(powerLeft, origScaleLeft, new Vector3(ColorPower.Instance.powerBlue / 100, 1, 1));
			ShiftAndScale(powerCenter, origScaleCenter, new Vector3(ColorPower.Instance.powerYellow / 100, 1, 1));
			ShiftAndScale(powerRight, origScaleRight, new Vector3(ColorPower.Instance.powerRed / 100, 1, 1));
		} else {
			ShiftAndScale(powerLeft, origScaleLeft, new Vector3(ColorPower.Instance.powerYellow / 100, 1, 1));
			ShiftAndScale(powerCenter, origScaleCenter, new Vector3(ColorPower.Instance.powerRed / 100, 1, 1));
			ShiftAndScale(powerRight, origScaleRight, new Vector3(ColorPower.Instance.powerBlue / 100, 1, 1));
		}
		
		float transformAmount = MainCharacterDriver.TRANSFORM_AMOUNT;
		if (ColorPower.Instance.powerRed >= transformAmount && ColorPower.Instance.powerBlue >= transformAmount) {
			if (primaryRing.renderer.material.color.a == 0 || primaryRing.renderer.material.color == Purple) {
					primaryRing.renderer.material.color = Purple;
			} else if (secondaryRing1.renderer.material.color.a == 0 || primaryRing.renderer.material.color == Purple) {
					secondaryRing1.renderer.material.color = Purple;
			} else {
					secondaryRing2.renderer.material.color = Purple;
			}
		}else{
			if(primaryRing.renderer.material.color == Purple){
				var tmp = primaryRing.renderer.material.color;
				tmp.a = 0;
				primaryRing.renderer.material.color = tmp;
			} else if(secondaryRing1.renderer.material.color == Purple){
				var tmp = secondaryRing1.renderer.material.color;
				tmp.a = 0;
				secondaryRing1.renderer.material.color = tmp;
			} else if(secondaryRing2.renderer.material.color == Purple){
				var tmp = secondaryRing2.renderer.material.color;
				tmp.a = 0;
				secondaryRing2.renderer.material.color = tmp;
			}
		}
		if (ColorPower.Instance.powerRed >= transformAmount && ColorPower.Instance.powerYellow >= transformAmount) {
			if(primaryRing.renderer.material.color.a == 0 || primaryRing.renderer.material.color == Orange){
				primaryRing.renderer.material.color = Orange;
			} else if(secondaryRing1.renderer.material.color.a == 0 || primaryRing.renderer.material.color == Orange){
				secondaryRing1.renderer.material.color = Orange;
			} else {
				secondaryRing2.renderer.material.color = Orange;
			}
		}else{
			if(primaryRing.renderer.material.color == Orange){
				var tmp = primaryRing.renderer.material.color;
				tmp.a = 0;
				primaryRing.renderer.material.color = tmp;
			} else if(secondaryRing1.renderer.material.color == Orange){
				var tmp = secondaryRing1.renderer.material.color;
				tmp.a = 0;
				secondaryRing1.renderer.material.color = tmp;
			} else if(secondaryRing2.renderer.material.color == Orange){
				var tmp = secondaryRing2.renderer.material.color;
				tmp.a = 0;
				secondaryRing2.renderer.material.color = tmp;
			}
		}
		if (ColorPower.Instance.powerYellow >= transformAmount && ColorPower.Instance.powerBlue >= transformAmount) {
			if(primaryRing.renderer.material.color.a == 0 || primaryRing.renderer.material.color == Green){
				primaryRing.renderer.material.color = Green;
			} else if(secondaryRing1.renderer.material.color.a == 0 || primaryRing.renderer.material.color == Green){
				secondaryRing1.renderer.material.color = Green;
			} else {
				secondaryRing2.renderer.material.color = Green;
			}
		}else{
			if(primaryRing.renderer.material.color == Green){
				var tmp = primaryRing.renderer.material.color;
				tmp.a = 0;
				primaryRing.renderer.material.color = tmp;
			} else if(secondaryRing1.renderer.material.color == Green){
				var tmp = secondaryRing1.renderer.material.color;
				tmp.a = 0;
				secondaryRing1.renderer.material.color = tmp;
			} else if(secondaryRing2.renderer.material.color == Green){
				var tmp = secondaryRing2.renderer.material.color;
				tmp.a = 0;
				secondaryRing2.renderer.material.color = tmp;
			}
		}
	}
	
    /// <summary>
    /// Resize bar with relation to its original scale
    /// </summary>
    /// <param name="powerBar"></param>
    /// <param name="origScale"></param>
    /// <param name="newScaleRatio"></param>
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
		var spawner = GameObject.Find ("WaveSpawner").GetComponent<Spawner> ();
		winScreen.SetActive(true);

		winScreen.renderer.material = successScreen;
		if (spawner.lastLevel) {
			introSound.audio.Stop ();
			audio.PlayOneShot (winSound);
		}
		showingWinLose = true;
		win = true;

	}
	
	public void ShowLoseScreen() {
		if(introSound!=null)
		{
					introSound.audio.Stop ();
		}
		else
		{
			Debug.Log("please plug something in for intro sound for uidriver");
		}
		
		points.enabled = false;
		Destroy (GameObject.FindGameObjectWithTag ("SoundBox"));
		audio.volume = 0.1f;
		audio.PlayOneShot (loseSound);
		loseScreen.SetActive(true);
		showingWinLose = true;
		win = false;
	}
}
