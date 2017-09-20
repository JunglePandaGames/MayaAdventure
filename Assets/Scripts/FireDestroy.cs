using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDestroy : MonoBehaviour {
	void Update(){
		GameObject.Destroy (gameObject, 2);
	}

	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Inimigo") {
			Debug.Log ("Bullete colidiu com BOX e Morreu");
			Destroy (gameObject);
			other.gameObject.GetComponent<Collider>().gameObject.SetActive (false);
		}
	}

//	void OnTriggerEnter(Collider other)
//	{
//		//if (enemy.GetComponent<Collider> ().GetType () == typeof(BoxCollider)) print ("Box");
//		//{
//		if (other.gameObject.tag == "Inimigo") {
//			Debug.Log ("Bullete colidiu com BOX e Morreu");
//			Destroy (gameObject);
//			//other.GetComponent<Collider>().gameObject.SetActive (false);
//			Destroy(enemy);
//		}
//		//}
//	}

}