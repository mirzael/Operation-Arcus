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

	public float invulnTime;
	public float invulnCounter = 0;
	public float prevAlpha = 1;
	public int lives;
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

	//Arcus Animator
	Animator anim;

	const float POWER_MAX = 100.0f;
	const float POWER_INC = 5.0f;
	const float TRANSFORM_AMOUNT = 50f;
	const int PROJECTILE_DISTANCE = 2;
	const int GREEN_DEGREES_PER_SEC = 720;
	const float ALPHA_PER_SEC = 0.1f;
	public float powerRed = 0.0f;
	public float powerBlue = 0.0f;
	public float powerYellow = 0.0f;

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
	//Yellow Weapon
	public float yellowPointsPerBullet;

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
	int purpleBarrel = 1;
	[SerializeField]
	public Form greenForm;
	//Green Weapon
	public float greenEmpRadius;
	public float greenEmpDuration;
	public float greenSinAmplitude;
	[SerializeField]
	public Form rainbowForm;

	//Used for returning to the form we were in before switching to secondary
	Form previousForm;

	public float shipXMin = -10.25f;
	public float shipXMax = 10.25f;
	public float shipYMin = -12.0f;
	public float shipYMax = 19.0f;

	public UIDriver uiDriver;

	public static string arcusName = "";

	// Use this for initialization
	void Start () {
		arcusName = gameObject.name;
		
		anim = GetComponent<Animator> ();
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
		redForm.animationNum = 1;
		redForm.originalCooldown = redForm.cooldown;
		redForm.originalSpeed = redForm.projectileSpeed;
		blueForm.shipColor = ShipColor.BLUE;
		blueForm.animationNum = 2;
		yellowForm.shipColor = ShipColor.YELLOW;
		yellowForm.animationNum = 3;
		greenForm.shipColor = ShipColor.GREEN;
		greenForm.animationNum = 0;
		orangeForm.shipColor = ShipColor.ORANGE;
		orangeForm.animationNum = 5;
		purpleForm.shipColor = ShipColor.PURPLE;
		purpleForm.animationNum = 4;
		rainbowForm.shipColor = ShipColor.RAINBOW;
		rainbowForm.animationNum = 0;

		forms.Add (redForm);
		forms.Add (blueForm);
		forms.Add (yellowForm);
		previousForm = forms [0];
		//Set the current form to the first form
		currentForm = forms [0];
		switchForm (currentForm);
		
		uiDriver = GameObject.Find("UI Camera").GetComponent<UIDriver>();
	}
	
	// Update is called once per frame
	void Update () {
		if (gameOver) return;
		invulnCounter -= Time.deltaTime;

		if (invulnCounter > 0) {
			foreach (Renderer obj in GetComponentsInChildren<Renderer>()) {
				obj.enabled = ! obj.enabled;
			}
		} else {
			foreach (Renderer obj in GetComponentsInChildren<Renderer>()) {
				obj.enabled = true;
			}
		}
		
		//Get where to move given user input
		float hspeed = Input.GetAxisRaw("Horizontal") * -(Time.deltaTime);
		float vspeed = Input.GetAxisRaw ("Vertical") * Time.deltaTime;

		var toMoveVector = Vector3.right * hspeed * currentForm.formSpeed + Vector3.back * vspeed * currentForm.formSpeed;
		Vector3 orig = transform.position;
		transform.Translate(toMoveVector);
		
		float posX = transform.position.x - transform.parent.position.x;
		float posY = transform.position.y - transform.parent.position.y;
		if (posX > shipXMax || posX < shipXMin || posY > shipYMax || posY < shipYMin) {
			transform.position = orig;
		}

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
				
				uiDriver.UpdateBars();
			}
			if (powerBlue == 0)
				switchForm (previousForm);
			return;
		}
		//Switch to Previous Form
		if (Input.GetKeyDown (KeyCode.Q)) {
			switchForm (forms.Previous ());
			previousForm = currentForm;
			uiDriver.RotateRight();
		//Switch to Next Form
		} else if (Input.GetKeyDown (KeyCode.E)) {
			switchForm (forms.Next ());
			previousForm = currentForm;
			uiDriver.RotateLeft();
		//Switch to ORANGE Form
		} else if (Input.GetKeyDown (KeyCode.Alpha1) && powerRed >= TRANSFORM_AMOUNT && powerYellow >= TRANSFORM_AMOUNT) {
			powerRed -= TRANSFORM_AMOUNT; powerYellow -= TRANSFORM_AMOUNT;
			switchForm (orangeForm);
			uiDriver.UpdateBars();
		//Switch to PURPLE FORM
		} else if (Input.GetKeyDown (KeyCode.Alpha2) && powerRed >= TRANSFORM_AMOUNT && powerBlue >= TRANSFORM_AMOUNT) {
			powerRed -= TRANSFORM_AMOUNT; powerBlue -= TRANSFORM_AMOUNT;
			switchForm (purpleForm);
			uiDriver.UpdateBars();
		//Switch to GREEN FORM
		} else if (Input.GetKeyDown (KeyCode.Alpha3) && powerBlue >= TRANSFORM_AMOUNT && powerYellow >= TRANSFORM_AMOUNT) {
			powerBlue -= TRANSFORM_AMOUNT; powerYellow -= TRANSFORM_AMOUNT;
			switchForm(greenForm);
			uiDriver.UpdateBars();
		} else if(Input.GetKeyDown (KeyCode.PageDown)){
			powerRed = powerYellow = powerBlue = 100;
			forms[0].setSpeed(forms[0].getSpeed() + powerRed / 30);
			forms[1].setCooldown(forms[1].getCooldown() - 0.15f);
			uiDriver.UpdateBars();
		}
	}

	void OnCollisionEnter(Collision col){
		if (currentForm.projectile.tag != col.gameObject.tag || col.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
			if(invulnCounter <= 0){
				invulnCounter = currentForm.shipColor == ShipColor.RAINBOW ? 0 : invulnTime;
				if(currentForm.shipColor == ShipColor.PURPLE){
					redForm.resetSpeed();
					redForm.setSpeed(redForm.getSpeed() + powerRed/30);
					blueForm.resetCooldown();
					blueForm.setCooldown(blueForm.getCooldown() - 0.00015f * powerBlue);
					switchForm(previousForm);
				}else if(currentForm.shipColor == ShipColor.ORANGE){
					switchForm(previousForm);
					redForm.resetSpeed();
					redForm.setSpeed(redForm.getSpeed() + powerRed/30);
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
					lives--;
					if(lives < 0){
						if (gameOver) return;
						Destroy (gameObject);
						powerRed = 0.0f;
						powerBlue = 0.0f;
						powerYellow = 0.0f;
						Debug.Log("MISSION FAILED");
						
						uiDriver.ShowLoseScreen();
						gameOver = true;
					}
				}
			}
		} else {
			if (col.gameObject.tag == "Red") {
				if (powerRed < POWER_MAX) {
					powerRed += POWER_INC;
					redForm.setSpeed(redForm.getSpeed() + powerRed/30);
					if (powerRed > POWER_MAX) {
						powerRed = POWER_MAX;
					}
					uiDriver.UpdateBars();
				}
				Debug.Log("Absorbed red bullet, Red Power at " + powerRed);
			} else if (col.gameObject.tag == "Blue") {
				if (powerBlue < POWER_MAX) {
					powerBlue += POWER_INC;
					currentForm.setCooldown(currentForm.getCooldown() - 0.00015f * powerBlue);
					if (powerBlue > POWER_MAX) {
						powerBlue = POWER_MAX;
					}
					uiDriver.UpdateBars();
				}
				Debug.Log("Absorbed blue bullet, Blue Power at " + powerBlue);
			} else if (col.gameObject.tag == "Yellow") {
				if (powerYellow < POWER_MAX) {
					powerYellow += POWER_INC;
					if (powerYellow > POWER_MAX) {
						powerYellow = POWER_MAX;
					}
					uiDriver.UpdateBars();
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
			projectile.AddComponent<BlueWeapon>().damage = currentForm.damage;
			break;
		case ShipColor.RED:
			projectile = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE + Vector3.left/2, currentForm.projectile.transform.rotation);
			projectile.rigidbody.velocity = Vector3.up * currentForm.getSpeed();
			var rWep = projectile.AddComponent<RedWeapon>();
			rWep.baseExplosionRadius = redExplosionRadius;
			rWep.radiusPerPoint = redRadiusPerPoint;
			rWep.driver = this;

			projectile = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE + Vector3.right/2, currentForm.projectile.transform.rotation);
			projectile.rigidbody.velocity = Vector3.up * currentForm.getSpeed();
			rWep = projectile.AddComponent<RedWeapon>();
			rWep.baseExplosionRadius = redExplosionRadius;
			rWep.radiusPerPoint = redRadiusPerPoint;
			rWep.damage = currentForm.damage;
			rWep.driver = this;

			break;
		case ShipColor.YELLOW:
			int numProjectiles = 3 + (int)(powerYellow / yellowPointsPerBullet);
			int projectileSpreadAngle = 30;
			int angleBetweenProjectiles = (projectileSpreadAngle / (numProjectiles - 1));
			float radToDeg =  Mathf.PI / 180;
			GameObject[] blast = new GameObject[numProjectiles];
			for(int i = 0; i < numProjectiles; i++){
				float trajectoryDegree = 90 + (projectileSpreadAngle / 2 - angleBetweenProjectiles * i);
				float currentAngularVelocity = Mathf.Cos(trajectoryDegree * radToDeg);
				blast[i] = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
				blast[i].rigidbody.velocity = transform.TransformDirection(Vector3.back * currentForm.getSpeed() + Vector3.right * currentAngularVelocity * currentForm.getSpeed());
				blast[i].AddComponent<YellowWeapon>().damage = currentForm.damage;
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
			oWep.damage = currentForm.damage;
			oBlast[1] = (GameObject)Instantiate(currentForm.projectile, transform.position + (Vector3.up + Vector3.right) * PROJECTILE_DISTANCE, currentForm.projectile.transform.rotation);
			oWep = oBlast[1].gameObject.AddComponent<OrangeWeapon>();
			oWep.moveSpeed = currentForm.projectileSpeed;
			oWep.rotationSpeed = orangeRotationSpeed;
			oWep.explosionRadius = orangeExplosionRadius;
			oWep.gravityRadius = orangeGravityRadius;
			oWep.gravityForce = orangeGravityForce;
			oWep.damage = currentForm.damage;
			break;
		case ShipColor.PURPLE:
			purpleBarrel *= -1;
			projectile = (GameObject)Instantiate(currentForm.projectile, transform.position + Vector3.up * PROJECTILE_DISTANCE + Vector3.left * PROJECTILE_DISTANCE / 2.5f * purpleBarrel, currentForm.projectile.transform.rotation);
			var moveScript = projectile.AddComponent<MoveProjectile>();
			moveScript.projectileSpeed = currentForm.projectileSpeed;
			var mirvStuff = projectile.AddComponent<PurpleWeapon>();
			mirvStuff.mirvBullet = purpleMirv;
			mirvStuff.bulletSpeed = currentForm.projectileSpeed;
			mirvStuff.timeBeforeExplosion = purpleTimeBeforeExplosion;
			mirvStuff.damage = currentForm.damage;
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
			gWep.damage = currentForm.damage;
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
				rainboom[i].AddComponent<RainbowWeapon>().damage = currentForm.damage;
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
		anim.SetInteger ("TransformVar", currentForm.animationNum);
	}

	void sinBullet(GreenWeapon weapon, bool isNegative){
		weapon.amplitude = isNegative ? -greenSinAmplitude : greenSinAmplitude;
		weapon.ySpeed = currentForm.getSpeed();
		weapon.degreesPerSec = GREEN_DEGREES_PER_SEC;
		weapon.sphereRadius = greenEmpRadius;
		weapon.empDuration = greenEmpDuration;
		weapon.damage = currentForm.damage;
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
