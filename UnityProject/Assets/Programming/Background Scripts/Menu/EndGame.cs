using UnityEngine;

public class EndGame : MenuAction {

	public override void TakeAction() {
		LevelLoader.LoadLevel("MainMenu");
	}
}