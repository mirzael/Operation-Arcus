using UnityEngine;
using System.Collections;
using MainCharacter;

public class MultiplayerCoordinator {

	static MultiplayerCoordinator _instance;

	public static MultiplayerCoordinator Instance{get{
			if(_instance == null){
				_instance = new MultiplayerCoordinator();
			}
			return _instance;
		}
	}
	
	public MultiplayerCharacterDriver OArcusDriver{ private get; set; }
	public MultiplayerCharacterDriver DarcusDriver{ private get; set; }
	public BackgroundUI backgroundUI;

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
		if (ColorPower.Instance.powerBlue >= CharacterDriver.TRANSFORM_AMOUNT && ColorPower.Instance.powerYellow >= CharacterDriver.TRANSFORM_AMOUNT) {
			ColorPower.Instance.powerBlue -= CharacterDriver.TRANSFORM_AMOUNT;
			ColorPower.Instance.powerYellow -= CharacterDriver.TRANSFORM_AMOUNT;
			
			if (GameObject.Find("oArcus") != null) {
				OArcusDriver.PressGreen();
			}
			if (GameObject.Find("dArcus") != null) {
				DarcusDriver.PressGreen();
			}
		}
	}

	public void UseOffensiveOrange(){
		if (ColorPower.Instance.powerRed >= CharacterDriver.TRANSFORM_AMOUNT && ColorPower.Instance.powerYellow >= CharacterDriver.TRANSFORM_AMOUNT) {
			ColorPower.Instance.powerRed -= CharacterDriver.TRANSFORM_AMOUNT;
			ColorPower.Instance.powerYellow -= CharacterDriver.TRANSFORM_AMOUNT;
			
			if (GameObject.Find("oArcus") != null) {
				OArcusDriver.PressOrange();
			}
			if (GameObject.Find("dArcus") != null) {
				DarcusDriver.PressOrange();
			}
		}
	}

	public void UseOffensivePurple(){
		if (ColorPower.Instance.powerRed >= CharacterDriver.TRANSFORM_AMOUNT && ColorPower.Instance.powerBlue >= CharacterDriver.TRANSFORM_AMOUNT) {
			ColorPower.Instance.powerRed -= CharacterDriver.TRANSFORM_AMOUNT;
			ColorPower.Instance.powerBlue -= CharacterDriver.TRANSFORM_AMOUNT;
			
			if (GameObject.Find("oArcus") != null) {
				OArcusDriver.PressPurple();
			}
			if (GameObject.Find("dArcus") != null) {
				DarcusDriver.PressPurple();
			}
		}
	}

	public void UseDefensiveGreen(){
		if (ColorPower.Instance.powerBlue >= CharacterDriver.TRANSFORM_AMOUNT && ColorPower.Instance.powerYellow >= CharacterDriver.TRANSFORM_AMOUNT) {
			ColorPower.Instance.powerBlue -= CharacterDriver.TRANSFORM_AMOUNT;
			ColorPower.Instance.powerYellow -= CharacterDriver.TRANSFORM_AMOUNT;
			
			if (GameObject.Find("oArcus") != null) {
				OArcusDriver.PressDefensiveGreen();
			}
			if (GameObject.Find("dArcus") != null) {
				DarcusDriver.PressDefensiveGreen();
			}
		}
	}
	
	public void UseDefensiveOrange(){
		if (ColorPower.Instance.powerRed >= CharacterDriver.TRANSFORM_AMOUNT && ColorPower.Instance.powerYellow >= CharacterDriver.TRANSFORM_AMOUNT) {
			ColorPower.Instance.powerRed -= CharacterDriver.TRANSFORM_AMOUNT;
			ColorPower.Instance.powerYellow -= CharacterDriver.TRANSFORM_AMOUNT;
			
			if (GameObject.Find("oArcus") != null) {
				OArcusDriver.PressDefensiveOrange();
			}
			if (GameObject.Find("dArcus") != null) {
				DarcusDriver.PressDefensiveOrange();
			}
		}
	}
	
	public void UseDefensivePurple(){
		if (ColorPower.Instance.powerRed >= CharacterDriver.TRANSFORM_AMOUNT && ColorPower.Instance.powerBlue >= CharacterDriver.TRANSFORM_AMOUNT) {
			ColorPower.Instance.powerRed -= CharacterDriver.TRANSFORM_AMOUNT;
			ColorPower.Instance.powerBlue -= CharacterDriver.TRANSFORM_AMOUNT;
			
			if (GameObject.Find("oArcus") != null) {
				OArcusDriver.PressDefensivePurple();
			}
			if (GameObject.Find("dArcus") != null) {
				DarcusDriver.PressDefensivePurple();
			}
		}
	}
}
