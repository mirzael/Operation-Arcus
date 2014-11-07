using UnityEngine;
using System.Collections;

public class RedWeapon : MonoBehaviour {
	public float baseExplosionRadius;
	public float radiusPerPoint;
	public MainCharacterDriver driver;

	Material redBlast;

	// Use this for initialization
	void Start () {
		redBlast = (Material)Resources.Load ("Materials/AoeBlasts/RedBlast", typeof(Material));
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter(Collision col){
		CreateAoe (col.contacts [0].point, redBlast, driver.powerRed*radiusPerPoint+baseExplosionRadius, 0.5f, false);
	}

	void CreateAoe(Vector3 center, Material mat, float radius, float duration, bool gravity){
		var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.renderer.material = mat;
		sphere.transform.position = center;
		sphere.transform.localScale = new Vector3(radius, radius, radius);
		sphere.layer = LayerMask.NameToLayer("Character Bullet");
		Destroy (sphere, duration);
		Destroy(gameObject);
	}
}
