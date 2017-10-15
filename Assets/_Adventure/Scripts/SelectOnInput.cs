using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectOnInput : MonoBehaviour {

    public EventSystem eventSystem;
    public GameObject selectedObject;
    Canvas enableOnMenuLoad;

    private bool buttonSelected;

	// Use this for initialization
	void Start () {
        //enableOnMenuLoad = GameObject.Find("Canvas").GetComponent<Canvas>();
        //enableOnMenuLoad.gameObject.SetActive(true);
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxisRaw("Vertical") != 0 && buttonSelected == false) {
            eventSystem.SetSelectedGameObject(selectedObject);
            buttonSelected = true;
        }
	}

    private void OnDisable()
    {
        buttonSelected = false;
    }
}
