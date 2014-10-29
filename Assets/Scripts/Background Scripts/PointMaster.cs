using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PointMaster : MonoBehaviour {
	GUIStyle pointStyle;
	float points = 0;
	public Color orange;
	public Color purple;
	public Color yellow;
	public Vector2 pos = new Vector2(0, 50);
	public Vector2 size  = new Vector2(100, 100);
	const float POINTS_PER_KILL = 1000;
	List<PointInfo> displayArray = new List<PointInfo>();

	// Use this for initialization
	void Start () {
		pointStyle = new GUIStyle ();
		pointStyle.normal.textColor = Color.white;
	}

	void OnGUI(){		
		pointStyle.normal.textColor = Color.white;

		foreach (PointInfo info in displayArray) {
			pointStyle.normal.textColor = info.color;
		}
		GUI.Label (new Rect (pos.x, pos.y, size.x, size.y), "POINTS:\t\t\t" + points, pointStyle);

		/*
		foreach(PointInfo info in displayArray){
			Debug.Log("PRINTING AT " + info.positionToDisplay.x + ", " + info.positionToDisplay.y + "WITH COLOR: " + info.color.ToString());
			pointStyle.normal.textColor = info.color;
			GUI.Label(new Rect(info.positionToDisplay.x, info.positionToDisplay.y, size.x, size.y), "IM A BANANA", pointStyle);
		} */
	}
	// Update is called once per frame
	void Update () {
		for(int i = 0; i < displayArray.Count; i++){
			displayArray[i].length -= Time.deltaTime;
			if(displayArray[i].length <= 0){
				displayArray.RemoveAt(i);
				i--;
			}
		}
	}

	public void Notify(DeathInfo shipDeath){
		float deathPoints = POINTS_PER_KILL;
		PointInfo pointInfo = new PointInfo ();
		switch (shipDeath.bulletTag) {
			case "Red":
				switch(shipDeath.shipTag){
					case "Blue":
						pointInfo.color = purple;
						break;
					case "Yellow":
						pointInfo.color = orange;
						break;
					case "Red":
						pointInfo.color = Color.red;
						break;
				}
				break;
			case "Blue":
				switch(shipDeath.shipTag){
					case "Yellow":
						pointInfo.color = Color.green;
						break;
					case "Red":
						pointInfo.color = purple;
						break;
					case "Blue":
						pointInfo.color = Color.blue;
						break;
				} 
				break;		
			case "Yellow":
				switch(shipDeath.shipTag){
					case "Red":
						pointInfo.color = orange;
						break;
					case "Blue":
						pointInfo.color = Color.green;
						break;
					case "Yellow":
						pointInfo.color = yellow;
						break;
				}
				break;
			case "Orange":
				if(shipDeath.shipTag == "Blue"){
					deathPoints += POINTS_PER_KILL;
					pointInfo.color = Color.white;
				}else{
					pointInfo.color = orange;
				}
				break;
			case "Purple":
				if(shipDeath.shipTag == "Yellow"){
					deathPoints += POINTS_PER_KILL;
					pointInfo.color = Color.white;
				}else{
					pointInfo.color = purple;
				}
				break;
			case "Green":
				if(shipDeath.shipTag == "Red"){
					deathPoints += POINTS_PER_KILL;
					pointInfo.color = Color.white;
				}else{
					pointInfo.color = Color.green;
				}
				break;
		}
		points += deathPoints;
		pointInfo.pointValue = deathPoints;
		pointInfo.length = 10;
		var where2Display = Camera.main.WorldToScreenPoint(shipDeath.shipPosition);
		pointInfo.positionToDisplay = where2Display;
		displayArray.Add (pointInfo);
	}
}

public struct DeathInfo {
	public string bulletTag;
	public string shipTag;
	public Vector3 shipPosition;
}

public class PointInfo{
	public Color color;
	public float pointValue;
	public float length;
	public Vector3 positionToDisplay;
}