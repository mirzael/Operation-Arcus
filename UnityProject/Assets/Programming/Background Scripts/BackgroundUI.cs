using UnityEngine;
using System.Collections;
using MainCharacter;

public class BackgroundUI : Singleton<BackgroundUI> {
	public Material successScreen;
	public GameObject winScreen, loseScreen;
	private bool showingWinLose;
	private bool win;
	
	public AudioClip loseSound;
	public AudioClip winSound;
	
	PointMaster points;
	public Material background;

	public void Start(){
        if(winScreen==null)
        {
            winScreen = GameObject.Find("WinScreen");
        }
        if(loseScreen==null)
        {
            loseScreen = GameObject.Find("LoseScreen");
        }
		winScreen.SetActive(false);
		loseScreen.SetActive(false);
		showingWinLose = false;
		win = false;
		
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
                    Application.LoadLevel("MainMenu");
					PointMaster.points = 0.0f;
					ColorPower.Instance.powerRed = ColorPower.Instance.powerBlue = ColorPower.Instance.powerYellow = 0;
					//GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterDriver>().ResetForm();
					return;
				}
				
				GameObject.Find("Background").renderer.material = background;
				winScreen.SetActive(false);

				if(MultiplayerController.Instance.isMultiplayer){
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
		
		if (spawner.lastLevel) {
			
			audio.PlayOneShot (winSound);
		}
		showingWinLose = true;
		win = true;
	}
	
	public void ShowLoseScreen() {
		points.enabled = false;
		Destroy (GameObject.FindGameObjectWithTag ("SoundBox"));
		audio.PlayOneShot (loseSound);
		loseScreen.SetActive(true);
		showingWinLose = true;
		win = false;
	}
}
