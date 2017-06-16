using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MapinguariBehavior : MonoBehaviour {
    GameObject player; 
    NavMeshAgent nav;
	Rigidbody rb;

    private Animator animator;
    private float moveHorizontal, moveVertical;
    private string isRunning="isRunning";
	private Vector3 previousPosition;
	// Use this for initialization
	void Start () {
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");

        animator = GetComponent<Animator>();
		rb = GetComponent<Rigidbody> ();
		previousPosition = transform.position;
	}
	
    void Update()
    {
        AnimationController();
		//Debug.Log(Vector3.zero);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player")) {
            
            nav.SetDestination(player.transform.position);
            nav.Resume();
			animator.SetBool(isRunning, true);

        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nav.Stop();
			animator.SetBool(isRunning, false);
        }
    }

    void AnimationController()
    {
		
		if (previousPosition == transform.position)
        {
            
			Debug.Log("False");
        } else
        {
			
			Debug.Log("True");
        }
		previousPosition = transform.position;


    }
}

