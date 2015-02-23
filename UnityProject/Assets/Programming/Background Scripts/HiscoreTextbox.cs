using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HiscoreTextbox : MonoBehaviour {
	private string username = "";
	public bool submitted = false;
    public bool inGUI = true;
    public GameObject thanksSubmitting;
	
	public void Awake() {
		if (!Hiscores.LatestScoreIsHiscore()) {
			submitted = true;
		}
	}
	
	/*public void OnGUI () {
		if (!submitted) {
			GUI.Label(new Rect(100, 50, 250, 25), "Hiscore! Type your name then press Enter");
			username = GUI.TextField(new Rect(100, 70, 250, 25), username, 40);
			
			if (Event.current.keyCode == KeyCode.Return && username.Length > 0) {
				Debug.Log("Saving score for " + username);
				Hiscores.SaveScore(username, Hiscores.latestScore);
				username = "";
				submitted = true;
			}
		}
	}*/

    public void OnValueChange(string name)
    {
        //Debug.LogError("name changed" + name);
        foreach(var arcus in GameObject.FindObjectsOfType<CharacterDriver>())
        {
            arcus.enabled = false;
        }
    }

    public void SubmitHiScore(string name)
    {
        if(!submitted)
        {
            Debug.Log(name + " submitting high score");
            Hiscores.SaveScore(name, Hiscores.latestScore);
            if(thanksSubmitting!=null)
            {
                thanksSubmitting.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }
}