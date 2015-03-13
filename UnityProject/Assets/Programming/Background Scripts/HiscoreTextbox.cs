using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HiscoreTextbox : MonoBehaviour {
	private string username = "";
	public bool submitted = false;
    public bool inGUI = true;
    public GameObject thanksSubmitting;
	
    public void SubmitHiScore()
    {
        SubmitHiScore("");
    }

    public void SubmitHiScore(string nameBS)
    {
        string name = gameObject.GetComponent<InputField>().text;
        if(!submitted)
        {
            //Debug.Log(name + " submitting high score");
            Hiscores.SaveScore(name, Hiscores.latestScore);
            if(thanksSubmitting!=null)
            {
                thanksSubmitting.SetActive(true);
                gameObject.SetActive(false);
            }
            submitted = true;
        }
    }
}