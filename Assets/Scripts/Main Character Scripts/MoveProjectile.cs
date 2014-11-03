using UnityEngine;
using System.Collections;

public class MoveProjectile : MonoBehaviour {
	public float projectileSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += Vector3.up * projectileSpeed/100;
	}

}
