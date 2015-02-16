using UnityEngine;

public class EndLevel : MonoBehaviour {
	private BackgroundUI ui;
    private bool animationPlaying = false;
    public float animTime = 2f;

	public void Start(){
		ui = Camera.main.GetComponent<BackgroundUI> ();
	}

	public void Update() {
		var enemies = GameObject.FindObjectsOfType(typeof(GameObject));
		foreach (GameObject go in enemies) {
			if (go.layer == LayerMask.NameToLayer("Enemy")) {
				return;
			}
		}

        PlayAnimation();
	}

    private void PlayAnimation()
    {
        if(animationPlaying)
        {
            return;
        }
        animationPlaying = true;
        if (MultiplayerController.isMultiplayer)
        {
            MultiplayerCoordinator.Instance.GameOver();
        }
        else
        {
            var driver = GameObject.Find(MainCharacterDriver.arcusName).GetComponent<MainCharacterDriver>();
            driver.WinLevel();
        }
        Invoke("ShowScreen",animTime);
    }

    private void ShowScreen()
    {
        ui.ShowWinScreen();
       // Component.Destroy(this);
    }
}