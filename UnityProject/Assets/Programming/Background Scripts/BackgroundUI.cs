using UnityEngine;
using System.Collections;
using MainCharacter;
using System.Collections.Generic;
using System;

public class BackgroundUI : Singleton<BackgroundUI> {
	public GameObject winScreen, loseScreen;
	private bool showingWinLose;
	private bool win;
	
	public AudioClip loseSound;
	public AudioClip winSound;

    protected bool pause = false;
    public GameObject pauseScreen;

    //Let other gameobjects call actions when the game ends
    private List<Action> gameEndEvents = new List<Action>();
    public void AddGameEndEvent(Action action)
    {
        gameEndEvents.Add(action);
    }
    public void RemoveGameEndEvent(Action action)
    {
        gameEndEvents.Remove(action);
    }
	
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
        pauseScreen.SetActive(false);
		showingWinLose = false;
		win = false;
        pause = false;
		
		points = Camera.main.gameObject.GetComponent<PointMaster> ();
	}
	
	// Update is called once per frame
	public void Update () {

        if (!showingWinLose)
        {
            //main game, not menu yet
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                pause = !pause;

                pauseScreen.SetActive(pause);

                if (pause)
                {
                    Time.timeScale = 0;
                    return;
                }
                else
                {
                    Time.timeScale = 1;
                }
            }
        }
        else
        {
            //Game ended, in menus
            if (Input.GetKeyDown(KeyCode.R))
            {
                //ZH 3-13 Should be handled by simply chaging level and by menus
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                PointMaster.points = 0;
                ColorPower.Instance.powerRed = ColorPower.Instance.powerBlue = ColorPower.Instance.powerYellow = 0;
                var driver = GameObject.FindGameObjectWithTag("Player").GetComponent<MainCharacterDriver>();
                driver.ResetForm();
                LevelLoader.LoadLevel("MainMenu");
            }
        }		
	}

	public void ShowWinScreen() {

//		var spawner = GameObject.Find ("WaveSpawner").GetComponent<Spawner> ();
		winScreen.SetActive(true);
		
		if (LevelLoader.IsLastLevel()) {
			
			audio.PlayOneShot (winSound);
		}
        audio.PlayOneShot(winSound);
        EndGame();
		win = true;
	}
	
	public void ShowLoseScreen() {
		Destroy (GameObject.FindGameObjectWithTag ("SoundBox"));
		audio.PlayOneShot (loseSound);
		loseScreen.SetActive(true);
        EndGame();
		win = false;
	}

    public void EndGame()
    {
        showingWinLose = true;
        points.enabled = false;
        foreach(Action action in gameEndEvents)
        {
            action();
        }
    }
}
