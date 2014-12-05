using UnityEngine;

public class EndGame : MonoBehaviour {

	public void Start(){
		//Screen.SetResolution (450, 800, true);
		}
	public void OnMouseDown() {
		Application.LoadLevel("MainMenu");
	}
}