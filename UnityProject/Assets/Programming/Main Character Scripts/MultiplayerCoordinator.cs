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
		backgroundUI = Camera.main.GetComponent<BackgroundUI> ();
	}


	public void UpdateUI(){
		OArcusDriver.uiDriver.UpdateBars ();
		DarcusDriver.uiDriver.UpdateBars ();
	}

	public void GameOver(){
		if (OArcusDriver.health <= 0 && DarcusDriver.health <= 0) {
			OArcusDriver.gameOver = true;
			DarcusDriver.gameOver = true;
		}

		backgroundUI.ShowLoseScreen ();
	}

	public void UseOffensiveGreen(){
		if (ColorPower.Instance.powerBlue >= CharacterDriver.TRANSFORM_AMOUNT && ColorPower.Instance.powerYellow >= CharacterDriver.TRANSFORM_AMOUNT) {
			ColorPower.Instance.powerBlue -= CharacterDriver.TRANSFORM_AMOUNT;
			ColorPower.Instance.powerYellow -= CharacterDriver.TRANSFORM_AMOUNT;

			OArcusDriver.PressGreen();
			DarcusDriver.PressGreen();
		}
	}

	public void UseOffensiveOrange(){
		if (ColorPower.Instance.powerRed >= CharacterDriver.TRANSFORM_AMOUNT && ColorPower.Instance.powerYellow >= CharacterDriver.TRANSFORM_AMOUNT) {
			ColorPower.Instance.powerRed -= CharacterDriver.TRANSFORM_AMOUNT;
			ColorPower.Instance.powerYellow -= CharacterDriver.TRANSFORM_AMOUNT;
			
			OArcusDriver.PressOrange();
			DarcusDriver.PressOrange();
		}
	}

	public void UseOffensivePurple(){
		if (ColorPower.Instance.powerRed >= CharacterDriver.TRANSFORM_AMOUNT && ColorPower.Instance.powerBlue >= CharacterDriver.TRANSFORM_AMOUNT) {
			ColorPower.Instance.powerRed -= CharacterDriver.TRANSFORM_AMOUNT;
			ColorPower.Instance.powerBlue -= CharacterDriver.TRANSFORM_AMOUNT;
			
			OArcusDriver.PressPurple();
			DarcusDriver.PressPurple();
		}
	}

	public void UseDefensiveGreen(){
		if (ColorPower.Instance.powerBlue >= CharacterDriver.TRANSFORM_AMOUNT && ColorPower.Instance.powerYellow >= CharacterDriver.TRANSFORM_AMOUNT) {
			ColorPower.Instance.powerBlue -= CharacterDriver.TRANSFORM_AMOUNT;
			ColorPower.Instance.powerYellow -= CharacterDriver.TRANSFORM_AMOUNT;
			
			OArcusDriver.PressDefensiveGreen();
			DarcusDriver.PressDefensiveGreen();
		}
	}
	
	public void UseDefensiveOrange(){
		if (ColorPower.Instance.powerRed >= CharacterDriver.TRANSFORM_AMOUNT && ColorPower.Instance.powerYellow >= CharacterDriver.TRANSFORM_AMOUNT) {
			ColorPower.Instance.powerRed -= CharacterDriver.TRANSFORM_AMOUNT;
			ColorPower.Instance.powerYellow -= CharacterDriver.TRANSFORM_AMOUNT;
			
			OArcusDriver.PressDefensiveOrange();
			DarcusDriver.PressDefensiveOrange();
		}
	}
	
	public void UseDefensivePurple(){
		if (ColorPower.Instance.powerRed >= CharacterDriver.TRANSFORM_AMOUNT && ColorPower.Instance.powerBlue >= CharacterDriver.TRANSFORM_AMOUNT) {
			ColorPower.Instance.powerRed -= CharacterDriver.TRANSFORM_AMOUNT;
			ColorPower.Instance.powerBlue -= CharacterDriver.TRANSFORM_AMOUNT;
			
			OArcusDriver.PressDefensivePurple();
			DarcusDriver.PressDefensivePurple();
		}
	}
}
