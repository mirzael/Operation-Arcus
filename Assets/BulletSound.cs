using UnityEngine;
using System.Collections;



public class BulletSound : MonoBehaviour {

	public AudioClip sound;

	// Use this for initialization
	void Initialize () {
		audio.PlayOneShot (sound);
	}

}
