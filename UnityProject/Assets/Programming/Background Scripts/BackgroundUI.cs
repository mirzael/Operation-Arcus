using UnityEngine;
using System.Collections;
using MainCharacter;

public class BackgroundUI : MonoBehaviour {
	public Material successScreen;
	private GameObject winScreen, loseScreen;
	private bool showingWinLose;
	private bool win;
	
	public AudioClip loseSound;
	public AudioClip winSound;
	
	PointMaster points;
	public Material background;

	public void Start(){
		winScreen = GameObject.Find("WinScreen");
		loseScreen = GameObject.Find("LoseScreen");
		winScreen.SetActive(false);
		loseScreen.SetActive(false);
		showingWinLose = false;
		win = false;
		
		GameObject.Find("Background").renderer.material = background;
		
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

				if(MultiplayerController.isMultiplayer){
					MultiplayerCoordinator.Instance.NewLevel();
				}else{
					var driver = GameObject.Find(MainCharacterDriver.arcusName).GetComponent<MainCharacterDriver>();
					driver.gameOver = false;
				}
				
				spawner.NextLevel();
			} else {
				Application.LoadLevel(Application.loadedLevel);
				
			}
		} else if (Input.GetKeyDown(KeyCode.Escape)) {
			PointMaster.points = 0;
			ColorPower.Instance.powerRed = ColorPower.Instance.powerBlue = ColorPower.Instance.powerYellow = 0;
			var driver = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterDriver>();
			driver.ResetForm();
			Application.LoadLevel("MainMenu");
		}
	}

	public void ShowWinScreen() {
		
		points.enabled = false;
		var spawner = GameObject.Find ("WaveSpawner").GetComponent<Spawner> ();
		winScreen.SetActive(true);
		
		winScreen.renderer.material = successScreen;
		if (spawner.lastLevel) {
			
			audio.PlayOneShot (winSound);
		}
		showingWinLose = true;
		win = true;
		
	}
	
	public void ShowLoseScreen() {
		
		points.enabled = false;
		Destroy (GameObject.FindGameObjectWithTag ("SoundBox"));
		audio.volume = 0.1f;
		audio.PlayOneShot (loseSound);
		loseScreen.SetActive(true);
		showingWinLose = true;
		win = false;
	}
}
