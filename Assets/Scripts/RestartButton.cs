using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {
	//GameObject p;

	void Start(){
	//	p = GameObject.FindGameObjectWithTag ("Player").GetComponent<> ();
	}
	public void RestartScene(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
	}

}
