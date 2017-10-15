using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionPortalControl : MonoBehaviour {

	OcclusionPortal OP;

	// Use this for initialization
	void Start () {
		OP = gameObject.GetComponent<OcclusionPortal> ();
	}

	void OnTriggerExit(Collider other){
		if(other.tag == "Player"){
			OP.open = true;
			Debug.Log ("Saiu");
		}
	}
	void OnTriggerEnter(Collider other){
		if(other.tag == "Player"){
			OP.open = false;
			Debug.Log ("Entrou");
		}
	}
}
