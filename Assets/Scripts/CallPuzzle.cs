using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallPuzzle : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        transform.GetChild(1).gameObject.SetActive(true);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
