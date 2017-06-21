using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using SaveGameFree;

public class SceneTransition : MonoBehaviour {
    public string destiny;
    public Vector3 destinyPosition;
    // Use this for initialization
    private void Start()
    {
		FadeManager.Instance.Fade (false, 1f);
		//Debug.Log ("SceneTrasnsiotion run");
    }

	void OnDestroy() {
		SaveGameState save = GetComponent<SaveGameState> ();
		save.gameState.sceneIndex = destiny;
		if(save != null) save.SaveScene ();
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) {
			FadeManager.Instance.Fade (true, 1f);
            //other.transform.position = destinyPosition;
			StartCoroutine(LoadScene(other));
            //other.transform.position = destinyPosition;
            
        }

    }

	IEnumerator LoadScene(Collider player){
		yield return new WaitForSeconds (1f);
		player.transform.position = destinyPosition;
		SceneManager.LoadScene (destiny);
	}
}
