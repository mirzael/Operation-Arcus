using UnityEngine;

public class EndGame : MenuAction {

    protected void Start()
    {
        Debug.Log("end game go go");
    }

	public override void TakeAction() {
		Application.LoadLevel("MainMenu");
	}
}