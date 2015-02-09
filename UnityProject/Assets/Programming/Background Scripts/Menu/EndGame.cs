using UnityEngine;

public class EndGame : MenuAction {

	public override void TakeAction() {
		Application.LoadLevel("MainMenu");
	}
}