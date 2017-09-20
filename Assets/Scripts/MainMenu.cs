using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
		GameObject.Destroy(GameObject.FindGameObjectWithTag ("Player"));
		Cursor.visible = true;
			
	}

	// Update is called once per frame
	void Update () {
		
	}
}

