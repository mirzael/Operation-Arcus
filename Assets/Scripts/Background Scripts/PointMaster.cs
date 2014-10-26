using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PointMaster : MonoBehaviour {
	public GUIStyle pointStyle;
	float points = 0;
	public Vector2 pos = new Vector2(0, 50);
	public Vector2 size  = new Vector2(100, 100);
	const float POINTS_PER_KILL = 1000;

	// Use this for initialization
	void Start () {
	}

	void OnGUI(){		
		GUI.Label (new Rect (pos.x, pos.y, size.x, size.y), "POINTS:\t\t\t" + points, pointStyle);
	}
	// Update is called once per frame
	void Update () {

	}

	public void Notify(DeathInfo shipDeath){
		switch (shipDeath.bulletTag) {
			case "Orange":
				if(shipDeath.shipTag == "Blue") points += POINTS_PER_KILL;
				break;
			case "Purple":
				if(shipDeath.shipTag == "Yellow") points += POINTS_PER_KILL;
				break;
			case "Green":
				if(shipDeath.shipTag == "Red") points += POINTS_PER_KILL;
				break;
		}
		points += POINTS_PER_KILL;
	}
}

public struct DeathInfo {
	public string bulletTag;
	public string shipTag;
}