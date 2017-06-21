using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour {

	public Rigidbody FireMagic;
	public Transform BulletEmitter;
	
	// Update is called once per frame
	void Update () {
		//Input.GetMouseButton -> Tiro continuo
		//Input.GetMouseButtonDonw -> Tiro um a um
		if (Input.GetMouseButtonDown (0)) {
			Rigidbody c = Rigidbody.Instantiate (FireMagic, BulletEmitter.position, BulletEmitter.rotation) as Rigidbody;
			c.AddForce (BulletEmitter.forward * 1250);
		}
	}
}
