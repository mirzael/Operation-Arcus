using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class DrawLives : MonoBehaviour {
	public GameObject innerHealthBar;
    public Slider healthSlider;

	public Renderer[] healthPortions;
	private CharacterDriver driver;

    public bool useGUI = true;

	// Use this for initialization
	void Start () {
		healthPortions = innerHealthBar.GetComponentsInChildren<Renderer> ();
		driver = gameObject.GetComponent<CharacterDriver> ();
	}

	void Update(){
		if (driver.health > 0) {
			UpdateHealth ();
		}
	}

	private void UpdateHealth(){
        if(useGUI)
        {
            healthSlider.value = driver.health;
        }
        else
        {
            float invisIndex = 10 - driver.health / 10;

            for (int i = healthPortions.Length - 1; i >= invisIndex; i--)
            {
                var tmp = healthPortions[i].material.color;
                tmp.a = 0.8f;
                healthPortions[i].material.color = tmp;
            }
            for (int i = 0; i < invisIndex; i++)
            {
                var tmp = healthPortions[i].material.color;
                tmp.a = 0f;
                healthPortions[i].material.color = tmp;
            }
        }
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
