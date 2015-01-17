using UnityEngine;
using System.Collections;

public class MoveProjectile : MonoBehaviour {
	public float projectileSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Time.timeScale != 0) transform.position += Vector3.up * projectileSpeed/100;
	}

}
