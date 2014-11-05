using UnityEngine;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Threading;
using MainCharacter;

public class MainCharacterDriver : MonoBehaviour {
	GameObject[] colorPieces;
	float currentCooldown = 0;
	int rainbowCooldown = 2;

	public float timeToWin = 15f;
	public bool gameOver = false;

	/*These are the Forms of the ship
	 *The forms comprise of
	 *	- Projectile Cooldown
	 *	- Projectile itself
	 *	- Projectile Speed
	 *	- Color of ship
	 *	- Speed of ship
	 */
	public RotatingList<Form> forms = new RotatingList<Form>();


	const float POWER_MAX = 100.0f;
	const float POWER_INC = 5.0f;
	const float TRANSFORM_AMOUNT = 50f;
	const int PROJECTILE_DISTANCE = 2;
	const int GREEN_DEGREES_PER_SEC = 720;
	public static float powerRed = 0.0f;
	public static float powerBlue = 0.0f;
	public static float powerYellow = 0.0f;

	//This is the current form the ship is using
	Form currentForm;

	//The base forms
	[SerializeField]
	public Form redForm;
	//Red Weapon
	public float redExplosionRadius;
	public float redRadiusPerPoint;
	[SerializeField]
	public Form blueForm;
	[SerializeField]
	public Form yellowForm;

	//These are the special ship forms
	[SerializeField]
	public Form orangeForm;
	//Orange Weapon
	public float orangeRotationSpeed;
	public float orangeExplosionRadius;
	public float orangeGravityRadius;
	public float orangeGravityForce;
	[SerializeField]
	public Form purpleForm;
	//Purple weapon
	public GameObject purpleMirv;
	public float purpleTimeBeforeExplosion;
	[SerializeField]
	public Form greenForm;
	//Green Weapon
	public float greenEmpRadius;
	public float greenEmpDuration;
	[SerializeField]
	public Form rainbowForm;

	//Used for returning to the form we were in before switching to secondary
	Form previousForm;


	// Use this for initialization
	void Start () {
		colorPieces = GameObject.FindGameObjectsWithTag ("ArcusColor");
		bool formSet = false;
		if (powerRed != 0.0f || powerBlue != 0.0f || powerYellow != 0.0f) {
			formSet = true;
		} else {
						powerRed = 0.0f;
						powerBlue = 0.0f;
						powerYellow = 0.0f;
						
				}

		redForm.shipColor = ShipColor.RED;
		blueForm.shipColor = ShipColor.BLUE;
		yellowForm.shipColor = ShipColor.YELLOW;
		greenForm.shipColor = ShipColor.GREEN;
		orangeForm.shipColor = ShipColor.ORANGE;
		purpleForm.shipColor = ShipColor.PURPLE;
		rainbowForm.shipColor = ShipColor.RAINBOW;

		forms.Add (redForm);
		forms.Add (blueForm);
		forms.Add (yellowForm);
		previousForm = forms [0];
		//Set the current form to the first form
		currentForm = forms [0];
		switchForm (currentForm);
	}
	
	// Update is called once per frame
	void Update () {
		if (gameOver) return;
		timeToWin -= Time.deltaTime;
		if (timeToWin <= 0.0f) {
			WinLoseGUI gui = GameObject.Find("Main Camera").AddComponent<WinLoseGUI>();
			gui.win = true;
			gameOver = true;
			Application.Quit();
			return;
		}
		
		//Get where to move given user input
		float hspeed = Input.GetAxisRaw("Horizontal") * -(Time.deltaTime);
		float vspeed = Input.GetAxisRaw ("Vertical") * Time.deltaTime;

		var toMoveVector = Vector3.right * hspeed * currentForm.formSpeed + Vector3.back * vspeed * currentForm.formSpeed;
		transform.Translate(toMoveVector);

		//change the cooldown of the main weapon, as one frame has passed
		currentCooldown -= Time.deltaTime;

		//FIRE!!!
		if (Input.GetKey(KeyCode.Space) && currentCooldown <= 0) {
			currentCooldown = currentForm.getCooldown();
			Fire();
		}
		if (currentForm.shipColor == ShipColor.RAINBOW) 
		{
			rainbowCooldown = rainbowCooldown - 1;
			if (rainbowCooldown <= 0)
			{
				rainbowCooldown = 10;
				powerBlue = powerBlue - 1;
				powerYellow = powerYellow - 1;
				powerRed = powerRed - 1;
			}
			if (powerBlue == 0)
				switchForm (previousForm);
			return;
		}
		//Switch to Previous Form
		if (Input.GetKeyDown (KeyCode.Q)) {
			switchForm (forms.Previous ());
			previousForm = currentForm;
		//Switch to Next Form
		} else if (Input.GetKeyDown (KeyCode.E)) {
			switchForm (forms.Next ());
			previousForm = currentForm;
		//Switch to ORANGE Form
		} else if (Input.GetKeyDown (KeyCode.Alpha1) && powerRed >= TRANSFORM_AMOUNT && powerYellow >= TRANSFORM_AMOUNT) {
			powerRed -= TRANSFORM_AMOUNT; powerYellow -= TRANSFORM_AMOUNT;
			switchForm (orangeForm);
		//Switch to PURPLE FORM
		} else if (Input.GetKeyDown (KeyCode.Alpha2) && powerRed >= TRANSFORM_AMOUNT && powerBlue >= TRANSFORM_AMOUNT) {
			powerRed -= TRANSFORM_AMOUNT; powerBlue -= TRANSFORM_AMOUNT;
			switchForm (purpleForm);
		//Switch to GREEN FORM
		} else if (Input.GetKeyDown (KeyCode.Alpha3) && powerBlue >= TRANSFORM_AMOUNT && powerYellow >= TRANSFORM_AMOUNT) {
			powerBlue -= TRANSFORM_AMOUNT; powerYellow -= TRANSFORM_AMOUNT;
			switchForm(greenForm);
		} else if(Input.GetKeyDown (KeyCode.PageDown)){
			powerRed = powerYellow = powerBlue = 100;
			forms[0].setSpeed(forms[0].getSpeed() + powerRed);
			forms[1].setCooldown(forms[1].getCooldown() - 0.00015f * powerBlue);
		}
	}

	void OnCollisionEnter(Collision col){
		if (currentForm.projectile.tag != col.gameObject.tag || col.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
			if(currentForm.shipColor == ShipColor.PURPLE){
				redForm.resetSpeed();
				redForm.setSpeed(redForm.getSpeed() + powerRed);
				blueForm.resetCooldown();
				blueForm.setCooldown(blueForm.getCooldown() - 0.00015f * powerBlue);
				switchForm(previousForm);
			}else if(currentForm.shipColor == ShipColor.ORANGE){
				switchForm(previousForm);
				redForm.resetSpeed();
				redForm.setSpeed(redForm.getSpeed() + powerRed);
			}else if(currentForm.shipColor == ShipColor.GREEN){
				switchForm(previousForm);
				blueForm.resetCooldown();
				blueForm.setCooldown(blueForm.getCooldown() - 0.00015f * powerBlue);
			}else if (currentForm.shipColor == ShipColor.RAINBOW)
				{
					Destroy (col.gameObject);
					return;
				}
			 else{
				if (gameOver) return;
				Destroy (gameObject);
				powerRed = 0.0f;
				powerBlue = 0.0f;
				powerYellow = 0.0f;
				Debug.Log("MISSION FAILED");
				
				WinLoseGUI gui = GameObject.Find("Main Camera").AddComponent<WinLoseGUI>();
				gui.win = false;
				gameOver = true;
				Application.Quit();
			}
		} else {
			if (col.gameObject.tag == "Red") {
				if (powerRed < POWER_MAX) {
					powerRed += POWER_INC;
					currentForm.setSpeed(currentForm.getSpeed() + powerRed);
					if (powerRed > POWER_MAX) {
						powerRed = POWER_MAX;
					}
				}
				Debug.Log("Absorbed red bullet, Red Power at " + powerRed);
			} else if (col.gameObject.tag == "Blue") {
				if (powerBlue < POWER_MAX) {
					powerBlue += POWER_INC;
					currentForm.setCooldown(currentForm.getCooldown() - 0.00015f * powerBlue);
					if (powerBlue > POWER_MAX) {
						powerBlue = POWER_MAX;
					}
				}
				Debug.Log("Absorbed blue bullet, Blue Power at " + powerBlue);
			} else if (col.gameObject.tag == "Yellow") {
				if (powerYellow < POWER_MAX) {
					powerYellow += POWER_INC;
					if (powerYellow > POWER_MAX) {
						powerYellow = POWER_MAX;
					}
				}
				Debug.Log("Absorbed yellow bullet, Yellow Power at " + powerYellow);
			}
		}
		Destroy (col.gameObject);
		if (powerYellow == POWER_MAX && powerBlue == POWER_MAX && powerRed == POWER_MAX) 
		{
			previousForm = currentForm;
			switchForm (rainbowForm);
		}
	}

	void Fire(){
		GameObject projectile;
		switch (currentForm.shipColor)
		{
		case ShipColor.BLUE:
			projectile = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
			projectile.rigidbody.velocity = Vector3.up * currentForm.getSpeed();
			break;
		case ShipColor.RED:
			projectile = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
			projectile.rigidbody.velocity = Vector3.up * currentForm.getSpeed();
			var rWep = projectile.AddComponent<RedWeapon>();
			rWep.baseExplosionRadius = redExplosionRadius;
			rWep.radiusPerPoint = redRadiusPerPoint;
			break;
		case ShipColor.YELLOW:
			int numProjectiles = 3 + (int)(powerYellow / POWER_INC);
			int projectileSpreadAngle = 30;
			int angleBetweenProjectiles = (projectileSpreadAngle / (numProjectiles - 1));
			float radToDeg =  Mathf.PI / 180;
			GameObject[] blast = new GameObject[numProjectiles];
			for(int i = 0; i < numProjectiles; i++){
				float trajectoryDegree = 90 + (projectileSpreadAngle / 2 - angleBetweenProjectiles * i);
				float currentAngularVelocity = Mathf.Cos(trajectoryDegree * radToDeg);
				blast[i] = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
				blast[i].rigidbody.velocity = transform.TransformDirection(Vector3.back * currentForm.getSpeed() + Vector3.right * currentAngularVelocity * currentForm.getSpeed());
			} 
			break;
		case ShipColor.ORANGE:
			var oBlast = new GameObject[2];
			oBlast[0] = (GameObject)Instantiate(currentForm.projectile, transform.position + (Vector3.up + Vector3.left) * PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
			var oWep = oBlast[0].gameObject.AddComponent<OrangeWeapon>();
			oWep.moveSpeed = currentForm.projectileSpeed;
			oWep.rotationSpeed = orangeRotationSpeed;
			oWep.explosionRadius = orangeExplosionRadius;
			oWep.gravityRadius = orangeGravityRadius;
			oWep.gravityForce = orangeGravityForce;
			oBlast[1] = (GameObject)Instantiate(currentForm.projectile, transform.position + (Vector3.up + Vector3.right) * PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
			oWep = oBlast[1].gameObject.AddComponent<OrangeWeapon>();
			oWep.moveSpeed = currentForm.projectileSpeed;
			oWep.rotationSpeed = orangeRotationSpeed;
			oWep.explosionRadius = orangeExplosionRadius;
			oWep.gravityRadius = orangeGravityRadius;
			oWep.gravityForce = orangeGravityForce;
			break;
		case ShipColor.PURPLE:
			projectile = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
			var moveScript = projectile.AddComponent<MoveProjectile>();
			moveScript.projectileSpeed = currentForm.projectileSpeed;
			var mirvStuff = projectile.AddComponent<PurpleWeapon>();
			mirvStuff.mirvBullet = purpleMirv;
			mirvStuff.bulletSpeed = currentForm.projectileSpeed;
			mirvStuff.timeBeforeExplosion = purpleTimeBeforeExplosion;
			break;
		case ShipColor.GREEN:
			var gProj = new GameObject[3];
			gProj[0] = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
			gProj[1] = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
			gProj[2] = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
			sinBullet(gProj[0].AddComponent<GreenWeapon>(), false);
			sinBullet(gProj[1].AddComponent<GreenWeapon>(), true);
			var gWep = gProj[2].AddComponent<GreenWeapon>();
			gWep.isStraight = true;
			gWep.ySpeed = currentForm.getSpeed();
			gWep.sphereRadius = greenEmpRadius;
			gWep.empDuration = greenEmpDuration;
			break;
		case ShipColor.RAINBOW:
			GameObject[] rainboom = new GameObject[15];
			int rainbowSpreadAngle = 50;
			int rainbowBetweenProjectiles = (rainbowSpreadAngle / (15 - 1));
			float rToD =  Mathf.PI / 180;
			Debug.Log (currentForm.projectile.transform.rotation.x);
			for(int i = 0; i < rainboom.Length; i++){
				float trajectoryDegree = 90 + (rainbowSpreadAngle / 2 - rainbowBetweenProjectiles * i);
				float currentAngularVelocity = Mathf.Cos(trajectoryDegree * rToD);
				rainboom[i] = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
				rainboom[i].rigidbody.velocity = transform.TransformDirection(Vector3.back * currentForm.getSpeed() + Vector3.right * currentAngularVelocity * currentForm.getSpeed());
			} 
			break;
		}
	}

	void switchForm(Form form){
		currentForm = form;
		for (int i = 0; i < colorPieces.Length; i++) 
		{
			colorPieces[i].renderer.material = currentForm.material;
		}
			currentCooldown = currentForm.getCooldown();
	}

	void sinBullet(GreenWeapon weapon, bool isNegative){
		weapon.amplitude = isNegative ? -weapon.amplitude : weapon.amplitude;
		weapon.ySpeed = currentForm.getSpeed();
		weapon.degreesPerSec = GREEN_DEGREES_PER_SEC;
		weapon.sphereRadius = greenEmpRadius;
		weapon.empDuration = greenEmpDuration;
	}

	public float[] getPowers()
	{
		float[] powerLevels = new float[4];
		powerLevels [0] = powerRed;
		powerLevels [1] = powerBlue;
		powerLevels [2] = powerYellow;
		switch (currentForm.shipColor) 
		{
		case ShipColor.RED:
			powerLevels[3] = 0f;
			break;
		case ShipColor.BLUE:
			powerLevels[3] = 1f;
			break;
		case ShipColor.YELLOW:
			powerLevels[3] = 2f;
			break;
		case ShipColor.ORANGE:
			powerLevels[3] = 3f;
			break;
		case ShipColor.PURPLE:
			powerLevels[3] = 4f;
			break;
		case ShipColor.GREEN:
			powerLevels[3] = 5f;
			break;
		case ShipColor.RAINBOW:
			powerLevels[3] = 6f;
			break;
		}
		return powerLevels;
	}


}
