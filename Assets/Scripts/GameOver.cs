using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Destroy(GameObject.FindGameObjectWithTag("Player"));
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
