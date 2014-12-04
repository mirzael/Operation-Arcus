using UnityEngine;
using System.Collections;

public class EnableDisable : MonoBehaviour {

	public float sphereRadius;
	public float empDuration;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){			
		int layerMask = 1 << 8;
		//Add the disabler script - enemy can't move or shoot
		foreach(Collider collider in Physics.OverlapSphere(transform.position, sphereRadius, layerMask)){
			if(collider.gameObject.GetComponent<Disabler>() == null){
				var dis = collider.gameObject.AddComponent<Disabler>();
				dis.disabledTime = empDuration;
			}
		}
	}
}
