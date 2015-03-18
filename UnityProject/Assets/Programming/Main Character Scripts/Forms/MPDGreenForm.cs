using UnityEngine;

public class MPDGreenForm : SecondaryForm {
	CharacterDriver driver;
	
	public void Start() {
		timeActiveOrig = 3f;
		driver = gameObject.GetComponent<CharacterDriver> ();
	}
	
	public override void Fire() {
	}
	
	public override void Activate() {
		isActive = true;
		timeActive = timeActiveOrig;
	}
	
	public void Update() {
		if (!isActive) return;
		timeActive -= Time.deltaTime;
		//50 Health / 3 seconds = 16 & 2/3
		driver.health += Time.deltaTime * (16 + 2.0f / 3);
		//If driver health > 100, set it to 100. Else, keep it as it is.
		driver.health = driver.health > 100 ? 100 : driver.health;
		if (timeActive <= 0.0f) {
			driver.health = Mathf.Floor(driver.health);
			isActive = false;
		}
	}
	
	public override bool TakeHit(Collision col) {
		Destroy(col.gameObject);
		return false;
	}

}