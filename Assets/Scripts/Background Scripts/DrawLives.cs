using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DrawLives : MonoBehaviour {
	GUIStyle liveStyle = new GUIStyle();
	public Vector2 guiTextPos = new Vector2 (0, 80);
	public Vector2 size  = new Vector2(100, 100);
	public GameObject innerHealthBar;

	public GameObject arcusModel;
	MainCharacterDriver driver;

	private Vector3 origHealthBar;

	// Use this for initialization
	void Start () {
		driver = (MainCharacterDriver)GetComponentInChildren (typeof(MainCharacterDriver));
		origHealthBar = innerHealthBar.transform.localScale;
	}

	// Update is called once per frame
	void OnGUI () {
		liveStyle.normal.textColor = Color.white;
	}

	void Update(){
		ShiftAndScale(innerHealthBar, origHealthBar, new Vector3(driver.health/100f,1,1));
	}

	private void ShiftAndScale(GameObject powerBar, Vector3 origScale, Vector3 newScaleRatio) {
		Vector3 curScale = powerBar.transform.localScale;
		Vector3 newScale = new Vector3(origScale.x * newScaleRatio.x, origScale.y * newScaleRatio.y, origScale.z * newScaleRatio.z);
		
		if (newScale.x == curScale.x && newScale.y == curScale.y && newScale.z == curScale.z) {
			return;
		}
		
		float newX = powerBar.transform.position.x - powerBar.renderer.bounds.extents.x;
		float curY = powerBar.transform.position.y;
		float curZ = powerBar.transform.position.z;
		powerBar.transform.position = new Vector3(newX, curY, curZ);
		powerBar.transform.localScale = newScale;
		newX = powerBar.transform.position.x + powerBar.renderer.bounds.extents.x;
		powerBar.transform.position = new Vector3(newX, curY, curZ);
	}
}
