using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapinguariBehavior : MonoBehaviour {
	public GameObject player; 
    NavMeshAgent nav;
	//Rigidbody rb;

    private Animator animator;
    private float moveHorizontal, moveVertical;
    private string isRunning="isRunning";
	private Vector3 previousPosition;

	public AudioClip roar;
	AudioSource mapinguariAudio;
	// Use this for initialization
	void Start () {
        nav = GetComponent<NavMeshAgent>();
		mapinguariAudio = GetComponent<AudioSource> ();
        //player = GameObject.FindGameObjectWithTag("Player");

        animator = GetComponent<Animator>();
		//rb = GetComponent<Rigidbody> ();
		previousPosition = transform.position;

	}
		
    void Update()
    {
		
		//Debug.Log (player);
        AnimationController();
		//Debug.Log(Vector3.zero);
		//player = GameObject.FindGameObjectWithTag("Player");
		if (GetComponent<FieldOfVision> ()) {
			List<Transform> target = GetComponent<FieldOfVision> () .visibleTargets;
			//Debug.Log ("Target " + target);

			if (target.Count == 0) {
				//nav.Stop ();
				animator.SetBool (isRunning, false);
			} else {
				if (!animator.GetBool (isRunning)) {
					mapinguariAudio.PlayOneShot (roar);
				}
				nav.isStopped = false;
				nav.destination = target [0].transform.position;
				animator.SetBool(isRunning, true);
			}
		}
	}

//    void OnTriggerStay(Collider other)
//    {
//        if (other.CompareTag("Player")) {
//            
//            nav.SetDestination(player.transform.position);
//            nav.Resume();
//			animator.SetBool(isRunning, true);
//
//        }
//    }
//    void OnTriggerExit(Collider other)
//    {
//        if (other.CompareTag("Player"))
//        {
//            nav.Stop();
//			animator.SetBool(isRunning, false);
//        }
//    }

    void AnimationController()
    {
		if (previousPosition == transform.position)
        {
            
			//Debug.Log("False");
        } else
        {
			
			//Debug.Log("True");
        }
		previousPosition = transform.position;
    }
}

