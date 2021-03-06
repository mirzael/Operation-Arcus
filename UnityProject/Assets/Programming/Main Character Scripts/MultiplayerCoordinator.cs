﻿using UnityEngine;
using System.Collections;
using MainCharacter;

public class MultiplayerCoordinator {

	static MultiplayerCoordinator _instance;

	public static MultiplayerCoordinator Instance{get{
			if(_instance == null){
				_instance = new MultiplayerCoordinator();
			}
			backgroundUI = BackgroundUI.Instance;
			return _instance;
		}
	}
	
	public MultiplayerCharacterDriver OArcusDriver{ private get; set; }
	public MultiplayerCharacterDriver DarcusDriver{ private get; set; }
	public static BackgroundUI backgroundUI;

	public MultiplayerCoordinator(){
		backgroundUI = BackgroundUI.Instance;
	}

	public void UpdateUI(){
		if (GameObject.Find("oArcus") != null) {
			OArcusDriver.uiDriver.UpdateBars ();
		}
		if (GameObject.Find("dArcus") != null) {
			DarcusDriver.uiDriver.UpdateBars ();
		}
	}

	public void GameOver(){
		if (OArcusDriver.health <= 0 && DarcusDriver.health <= 0) {
            backgroundUI.ShowLoseScreen();
		}
    /*    else
        {
			if (GameObject.Find("oArcus") != null) {
				OArcusDriver.WinLevel();
			}
			if (GameObject.Find("dArcus") != null) {
				DarcusDriver.WinLevel();
			}
        } */
	}

	public void NewLevel(){
		OArcusDriver.gameOver = false;
		DarcusDriver.gameOver = false;
	}

	public void UseOffensiveGreen(){
			
			if (GameObject.Find("oArcus") != null) {
				OArcusDriver.ActivateGreen();
			}
			if (GameObject.Find("dArcus") != null) {
				DarcusDriver.ActivateGreen();
			}
	}

	public void UseOffensiveOrange(){			
			if (GameObject.Find("oArcus") != null) {
				OArcusDriver.ActivateOrange();
			}
			if (GameObject.Find("dArcus") != null) {
				DarcusDriver.ActivateOrange();
			}
	}

	public void UseOffensivePurple(){
	    if (GameObject.Find("oArcus") != null) {
	    	OArcusDriver.ActivatePurple();
	    }
	    if (GameObject.Find("dArcus") != null) {
	    	DarcusDriver.ActivatePurple();
	    }
	}

	public void UseDefensiveGreen(){			
	    if (GameObject.Find("oArcus") != null) {
	    	OArcusDriver.PressDefensiveGreen();
	    }
	    if (GameObject.Find("dArcus") != null) {
	    	DarcusDriver.PressDefensiveGreen();
	    }
	}
	
	public void UseDefensiveOrange(){
	    if (GameObject.Find("oArcus") != null) {
	    	OArcusDriver.PressDefensiveOrange();
	    }
	    if (GameObject.Find("dArcus") != null) {
	    	DarcusDriver.PressDefensiveOrange();
	    }
	}
	
	public void UseDefensivePurple(){			
	    if (GameObject.Find("oArcus") != null) {
	    	OArcusDriver.PressDefensivePurple();
	    }
	    if (GameObject.Find("dArcus") != null) {
	    	DarcusDriver.PressDefensivePurple();
	    }
	}
}
