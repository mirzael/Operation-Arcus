using UnityEngine;
using MainCharacter;
using System.Collections;

public class OrangeWeapon : MonoBehaviour {
	Transform shape;
	Transform target;

	public float moveSpeed;
	public float rotationSpeed;
	public float explosionRadius;
	public float gravityRadius;
	public float gravityForce;
	public float damage;
	public Material blackHoleMat;

	Material orangeBlast;
	Material wellBlast;
	Sprite blackHole;

	// Use this for initialization
	void Start () {
		orangeBlast = (Material)Resources.Load ("Materials/AoeBlasts/OrangeBlast", typeof(Material));
		wellBlast = (Material)Resources.Load ("Materials/Black-Hole-v01", typeof(Material));
		blackHole = (Sprite)Resources.Load ("Textures/Black-Hole-v01", typeof(Sprite));
		findTarget ();
	}
	
	// Update is called once per frame
	void Update () {
		if (target != null && transform.position.y < target.position.y + 1) {

			//find the vector pointing from our position to the target
			var _direction = (target.position - transform.position).normalized;

			//create the rotation we need to be in to look at the target
			var _lookRotation = Quaternion.LookRotation (_direction);

			//rotate us over time according to speed until we are in the required rotation
			transform.rotation = Quaternion.Slerp (transform.rotation, _lookRotation, Time.deltaTime * rotationSpeed);
		}
			//move towards the player
			transform.position += transform.forward * moveSpeed * Time.deltaTime;

	}

	void findTarget(){
		RaycastHit hit;
		int layermask = 1 << 8;
		
		//RenderVolume (transform.position + Vector3.left * 0.5f, transform.position + Vector3.right * 0.5f, 10f, Vector3.up, 100);
		if (Physics.CapsuleCast (transform.position + Vector3.left * 0.5f, transform.position + Vector3.right * 0.5f, 10f, Vector3.up, out hit, Mathf.Infinity, layermask)) {
			target = hit.transform;
		} else {
			Destroy (gameObject);
		}
	}

	void RenderVolume(Vector3 p1, Vector3 p2, float radius, Vector3 dir, float distance){
		if (!shape){ // if shape doesn't exist yet, create it
			shape = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
			Destroy(shape.collider); // no collider, please!
			shape.renderer.material = (Material)Resources.Load("Materials/Invis", typeof(Material)); // assign the selected material to it
		}
		Vector3 scale; // calculate desired scale
		float diam = 2 * radius; // calculate capsule diameter
		scale.x = diam; // width = capsule diameter
		scale.y = Vector3.Distance(p2, p1) + diam; // capsule height
		scale.z = distance + diam; // volume length
		shape.localScale = scale; // set the rectangular volume size
		// set volume position and rotation
		shape.position = (p1 + p2 + dir.normalized * distance) / 2;
		shape.rotation = Quaternion.LookRotation(dir, p2 - p1);
		shape.renderer.enabled = true; // show it
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.tag == "Blue") {
			CreateAoe (col.contacts [0].point, orangeBlast, gravityRadius, 1f, true);
		} else {
			CreateAoe (col.contacts [0].point, orangeBlast, explosionRadius, 0.5f, false);
		}
		col.gameObject.BroadcastMessage ("OnHit", new WeaponDamage{tag=tag, damage=damage});
	}

	void CreateAoe(Vector3 center, Material mat, float radius, float duration, bool gravity){
		var sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		sphere.renderer.material = mat;
		sphere.transform.position = center;
		sphere.transform.localScale = new Vector3(radius, radius, radius);
		if(gravity){ 
			var field = sphere.AddComponent<GravityField>();
			//sphere.renderer.material = wellBlast;
			sphere.transform.localEulerAngles = new Vector3(0, 110.0788f, 0);
			//sphere.renderer.material.color = new Color(255, 255, 255, 0.5f);
			/*SpriteRenderer aRender = sphere.AddComponent<SpriteRenderer> ();
			aRender.material = wellBlast;
			aRender.sprite = blackHole;
			aRender.material.color = new Color(255, 255, 255, 0.5f);*/
			field.GRAVITY_FIELD = gravityRadius;
			field.GRAVITY_FORCE = gravityForce;
			Destroy (sphere.collider);
		}else{
			sphere.layer = LayerMask.NameToLayer("Character Bullet");
		}
		Destroy (sphere, duration);
		Destroy(gameObject);
	}
}
