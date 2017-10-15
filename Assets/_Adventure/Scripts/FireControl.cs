using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour {

	public Rigidbody FireMagic;
	public Transform BulletEmitter;
	public AudioClip fireClip;
	AudioSource playerAudio;

	void Start(){
		playerAudio = GetComponent<AudioSource> ();
	}
	// Update is called once per frame
	void Update () {
		//Input.GetMouseButton -> Tiro continuo
		//Input.GetMouseButtonDonw -> Tiro um a um
		if (Input.GetKeyDown(KeyCode.P)) {
			playerAudio.pitch = Random.Range (0.9f, 1.1f);
			playerAudio.PlayOneShot (fireClip);
			Rigidbody c = Rigidbody.Instantiate (FireMagic, BulletEmitter.position, BulletEmitter.rotation) as Rigidbody;
			c.AddForce (BulletEmitter.forward * 1250);
		}
	}
}
