using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleReward : MonoBehaviour {

	Animator animator;

	void Start() {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void doReward()
	{
		animator.Play ("Cai");
		StartCoroutine (Destroier ());
	}

	public IEnumerator Destroier() {
		yield return new WaitForSeconds(1);
		Destroy (this.gameObject);
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player")
		doReward ();
	}


}
