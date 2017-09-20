using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallPuzzle : MonoBehaviour {

	public bool trigger;

    void OnTriggerEnter(Collider other)
    {
		if(other.CompareTag("Player"))
        transform.GetChild(1).gameObject.SetActive(true);
        Cursor.visible = true;
    }

	void OnTriggerExit(Collider other)
	{
		if(other.CompareTag("Player"))
		transform.GetChild(1).gameObject.SetActive(false);
        Cursor.visible = false;
    }
		
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(trigger) transform.GetChild(1).gameObject.SetActive(true);
	}
}
