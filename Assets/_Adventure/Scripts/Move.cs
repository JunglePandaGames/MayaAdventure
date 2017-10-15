// Converted from UnityScript to C# at http://www.M2H.nl/files/js_to_c.php - by Mike Hergaarden
// Do test the code! You usually need to change a few small bits.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {
    //UnityEngine.SceneManagement.SceneManager ls;

    //[Header("Movement")]
    private float speed;
    private float maxSpeed;
    private float minSpeed;
    protected float velocidadeCima;
    public int MaxLife;
    private int life;
    private float initialPositionX;
    private float initialPositionZ;
    private float ROTATE_SCALE = 45;
    private string ITEM_TAG = "Item";
    private string INIMIGOS_TAG = "Inimigo";
    private string SPEED = "speed"; 
    private float turnSpeed = 200f;
    // force is how forcefully we will push the player away from the enemy.
    private float force = 750;


    //[Header("Components")]
    public Animator animator;
    public Rigidbody rigidBody;
	AudioSource playerAudio;

    //[Header("Animation")]
    public bool isRunning = false;
    public bool isWalking = false;
    public bool isJumping = false;
    public bool isOnGround = true;
    private bool isMoveKeyPressed = false;
    private bool isRunningKeyPressed = false;
    private bool isJumpKeyPressed = false;
    private bool isDead = false;
    string isDying = "isDying";
    string isPickingObject = "isPickingObject";
    string jump = "Jump";
    string damage = "Damage";
	public AudioClip hurt;

    private bool paused = false; //variable for detect state game pause/unpause

    void Start()
    {
        minSpeed = 0.0999F;
        maxSpeed = 0.2F;
        life = MaxLife;
        DontDestroyOnLoad(transform.gameObject);
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody>();
		playerAudio = GetComponent<AudioSource> ();
        initialPositionX = transform.position.x;
        initialPositionZ = transform.position.z;
    }


    void FixedUpdate()
    {
        //velocidadeFrente = 5 * Time.deltaTime;
        //speed = 0.09999F;

        if (!isDead)
        {
            //input
            // MovementsController() é responsavel pelo movimentação da personagem, se o jogo estiver pausado a movimentação é desativada
            MovementsController();

			//Controla quando o som de passos deve ser reproduzido.
			if(isWalking && !playerAudio.isPlaying){
				playerAudio.pitch = 1f;
				playerAudio.Play();
			}
			if(isRunning && !playerAudio.isPlaying){
				playerAudio.pitch = 1.5f;
				playerAudio.Play();
			}

        }

        //animation / input
        AnimationController();
        //UI update set animation
        UpdateAnimetionParameters();
        UpdateHUD();

        /*if(Input.GetKeyDown(KeyCode.Escape))
        {
            // this line checks the state of the "paused" variable and then changes it to the other state
            paused = paused ? false:true; 
            // And here we're just changing the timecale    
        }
        if(paused)
            Time.timeScale = 0;
        else{
            MovementsController();
            Time.timeScale = 1;
        }*/


    }

    void MovementsController()
    {
        //Debug.Log ("isOnGround: "+ isOnGround);

        isMoveKeyPressed = false;
        isJumpKeyPressed = false;

        //JUMP Key
        if (Input.GetKey("space"))
        {
            isJumpKeyPressed = true;
        }


        //mover para frente
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            transform.eulerAngles = new Vector3(0, ROTATE_SCALE * 0, 0);
            isMoveKeyPressed = true;
        }

        //mover para trás
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            transform.eulerAngles = new Vector3(0, ROTATE_SCALE * 4, 0);
            isMoveKeyPressed = true;
        }

        //mover para esquerda
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            transform.eulerAngles = new Vector3(0, ROTATE_SCALE * 6, 0);
            isMoveKeyPressed = true;
        }

        //mover para direita
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector3(0, ROTATE_SCALE * 2, 0);
            isMoveKeyPressed = true;
        }

        //mover para frente e esquerda
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            transform.eulerAngles = new Vector3(0, ROTATE_SCALE * 7, 0);
            isMoveKeyPressed = true;

        }
        //mover para frente e direita
        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            transform.eulerAngles = new Vector3(0, ROTATE_SCALE * 1, 0);
            isMoveKeyPressed = true;
        }


        //mover para trás e direita
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)))
        {
            transform.eulerAngles = new Vector3(0, ROTATE_SCALE * 3, 0);
            isMoveKeyPressed = true;
        }

        //mover para trás e esquerda
        if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)))
        {
            transform.eulerAngles = new Vector3(0, ROTATE_SCALE * 5, 0);
            isMoveKeyPressed = true;
        }

        //checar o SHIFT para corrida, senão ela apenas andará
        if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift))
        {
            isRunningKeyPressed = true;
        }
        else
        {
            isRunningKeyPressed = false;
        }

        if (isOnGround)
        {
            if (transform.position.y < 5)
            {
                if (isJumpKeyPressed)
                {
                    Debug.Log("Maya altura:" + transform.position.y);
                    animator.SetTrigger(jump);
                    //rigidBody.AddForce(transform.TransformDirection(new Vector3(0,1,1))*velocidadeCima, ForceMode.Impulse);
                    rigidBody.velocity = transform.TransformDirection(new Vector3(0, 1, 1)) * (3);
                }
                if (isMoveKeyPressed)
                {
                    if (isRunningKeyPressed)
                    {
                        transform.Translate(0, 0, speed);
                        Debug.Log("velocidadeFrente Correndo: " + speed);
                    }
                    else
                    {
                        transform.Translate(0, 0, speed);
                        Debug.Log("velocidadeFrente Andando: " + speed);
                    }
                }
            }
        }

    }
    void AnimationController()
    {
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        //movimento lateral e/ou movimento vertical (frente e trás)
        if (!isDead)
        {
            if (!isMoveKeyPressed && !isJumping)
            {
                isWalking = false;
                isRunning = false;
                animator.Play("Parado");
            }
            else
            {
                if (isRunningKeyPressed)
                {
                    if(speed < maxSpeed)
                    {
                        speed += 0.0111F;
                    }
                    isRunning = true;
                    isWalking = false;
                    animator.SetFloat(SPEED,speed);
                }
                else if (isMoveKeyPressed)
                {
                    isWalking = true;
                    isRunning = false;
                    speed = minSpeed;
                    animator.SetFloat(SPEED, 0);

                }
                //else
                {
                }
            }
        }

    }

    void UpdateAnimetionParameters()
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
        //animator.SetBool("isDead", isDead);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == ITEM_TAG)
        {
            // Destroy(col.gameObject);
            animator.SetTrigger(isPickingObject);
        }
        if (col.gameObject.tag == INIMIGOS_TAG)
        {
            ReflectCollision(col);
			playerAudio.pitch = 0.9f;
			playerAudio.PlayOneShot(hurt);
            life--;
            Debug.Log("Life: " + life);
            if (!isDead && life <= 0)
            {
                isDead = true;
                animator.SetBool("isDead", isDead);
                Debug.Log("Morrendo");
                LoadGameOver();
            }
            else
            {
                animator.Play("TomandoDano");
                animator.SetBool(damage, true);
                StartCoroutine(waitUI());
                animator.SetBool(damage, false);
            }
        }
        if (col.gameObject.layer == 8 && !isOnGround)
        {
            // Destroy(col.gameObject);
            Debug.Log("no chão");
            isOnGround = true;
            isJumping = false;
        }

    }

    void OnCollisionExit(Collision col)
    {
        if (col.gameObject.layer == 8 && isOnGround && isJumpKeyPressed)
        {
            Debug.Log("pulou");
            isOnGround = false;
            isJumping = true;
        }
    }

    void multiDirectionsMovement()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        transform.Translate(direction * 2 * Time.deltaTime);
        //transform.LookAt(direction);

    }

    void LoadGameOver()
    {
        animator.Play("Morrendo");
        Debug.Log("Game Over");
        StartCoroutine(waitUI());
        Cursor.visible = true;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }

    void ReflectCollision(Collision col)
    {
        //rigidBody.AddForce(transform.TransformDirection(Vector3.forward)*-10, ForceMode.Impulse);
        //animator.SetTrigger(damage);

        Debug.Log(damage);
        Vector3 dir;
        // Calculate Angle Between the collision point and the player
        dir = transform.position - col.transform.position;
        // We then get the opposite (-Vector3) and normalize it
        dir.Normalize();
        // And finally we add force in the direction of dir and multiply it by force. 
        // This will push back the player
        Debug.Log("Força:" + dir * force);
        rigidBody.AddForce(dir * force);
    }

    IEnumerator waitUI()
    {
        yield return new WaitForSeconds(1.2f);
    }

    void UpdateHUD()
    {
        FadeManager.Instance.updateLifeValue(life);
        //FadeManager.Instance.fadeImage;
    }
}