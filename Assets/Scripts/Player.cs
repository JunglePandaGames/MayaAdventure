using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    Vector3 spawnPoint;
    Quaternion rotation;
	GameObject p;
	// Use this for initialization
	void Start () {
		p = GameObject.FindGameObjectWithTag ("Player");
		if (p==null) {
			GameObject go = Instantiate(Resources.Load("PersonagemAnimada"),spawnPoint,rotation) as GameObject;
		}
		p = GameObject.FindGameObjectWithTag ("Player");
		p.GetComponent<PlayerState> ().currentScene = p.scene.buildIndex;
        spawnPoint = GameObject.FindWithTag("SpawnPoint").transform.position;
        rotation = GameObject.FindWithTag("SpawnPoint").transform.rotation;
		FadeManager.Instance.Fade (false, 1f);
		Debug.Log ("SceneTrasnsiotion run");

    }
	void OnLevelWasLoad(){
		Debug.Log ("Teste");
	}
    // Update is called once per frame
    void Update () {
		
	}
}
