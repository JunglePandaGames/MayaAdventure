#pragma strict

var ls:UnityEngine.SceneManagement.SceneManager;

//[Header("Movement")]
var velocidadeFrente : float;
var velocidadeCima : float;
var MaxLife : int;
private var life : int;
private var initialPositionX : float;
private var initialPositionZ : float;
private final var ROTATE_SCALE : float = 45;
private final var ITEM_TAG : String = "Item"; 
private final var INIMIGOS_TAG : String = "Inimigo"; 
private final var turnSpeed : float = 200f;
// force is how forcefully we will push the player away from the enemy.
private var force : float = 750;


//[Header("Components")]
var animator : Animator;
var rigidBody : Rigidbody;
var Hud : GameObject;

//[Header("Animation")]
var isRunning : boolean = false;
var isWalking : boolean = false;
var isJumping : boolean = false;
var isOnGround : boolean = true;
private var isMoveKeyPressed : boolean = false;
private var isRunningKeyPressed : boolean = false;
private var isJumpKeyPressed : boolean = false;
private var isDead : boolean = false;
final var isDying : String = "isDying";
final var isPickingObject : String = "isPickingObject";
final var jump : String = "Jump";
final var damage : String = "Damage";

private var paused: boolean = false; //variable for detect state game pause/unpause

function Start () {
    life = MaxLife;
    DontDestroyOnLoad(transform.gameObject);
    animator = GetComponent("Animator");
    rigidBody = GetComponent("Rigidbody");
    //Hud = GetGameObject("FadeImage");
    //Hud = GameObject.FindWithTag("FadeImage");
    initialPositionX = transform.position.x;
    initialPositionZ = transform.position.z;
}


function FixedUpdate () {
    velocidadeFrente = 5*Time.deltaTime;
    
    if(!isDead){
        //input
        // MovementsController() é responsavel pelo movimentação da personagem, se o jogo estiver pausado a movimentação é desativada
        MovementsController();
    }
    //animation / input
    AnimationController();
    //UI update set animation
    UpdateAnimetionParameters();
    
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

function MovementsController(){
    //Debug.Log ("isOnGround: "+ isOnGround);
    
    isMoveKeyPressed = false;
    isJumpKeyPressed = false;

    //JUMP Key
    if(Input.GetKey("space")){
        isJumpKeyPressed = true;
    }
    

    //mover para frente
    if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
        transform.eulerAngles  = new Vector3(0,ROTATE_SCALE*0,0);
        isMoveKeyPressed = true;
    }

    //mover para trás
    if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
        transform.eulerAngles  = new Vector3(0,ROTATE_SCALE*4,0);
        isMoveKeyPressed = true;
    }

    //mover para esquerda
    if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
        transform.eulerAngles  = new Vector3(0,ROTATE_SCALE*6,0);
        isMoveKeyPressed = true;
    }

    //mover para direita
    if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
        transform.eulerAngles  = new Vector3(0,ROTATE_SCALE*2,0);
        isMoveKeyPressed = true;
    }
    
    //mover para frente e esquerda
    if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
        && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))){
        transform.eulerAngles  = new Vector3(0,ROTATE_SCALE*7,0);
        isMoveKeyPressed = true;

        if(Input.GetKey("space")){
            isJumpKeyPressed = true;
        }
    }
    //mover para frente e direita
    if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
        && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))){
        transform.eulerAngles  = new Vector3(0,ROTATE_SCALE*1,0);
        isMoveKeyPressed = true;
    }


    //mover para trás e direita
    if((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
        && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))){
        transform.eulerAngles  = new Vector3(0,ROTATE_SCALE*3,0);
        isMoveKeyPressed = true;
    }

    //mover para trás e esquerda
    if((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
        && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))){
        transform.eulerAngles  = new Vector3(0,ROTATE_SCALE*5,0);
        isMoveKeyPressed = true;
    }

    //checar o SHIFT para corrida, senão ela irá andar apenas
    if(Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.LeftShift)){
        isRunningKeyPressed = true;
    } else {
        isRunningKeyPressed = false;
    }

    if(isOnGround){
        if(transform.position.y < 5){
            if(isJumpKeyPressed){
                Debug.Log ("Maya altura:"+transform.position.y);
                animator.SetTrigger(jump);
                //rigidBody.AddForce(transform.TransformDirection(new Vector3(0,1,1))*velocidadeCima, ForceMode.Impulse);
                rigidBody.velocity = transform.TransformDirection(new Vector3(0,1,1))*(velocidadeCima);
            }
            if(isMoveKeyPressed){
                if(isRunningKeyPressed){
                    transform.Translate(0,0,velocidadeFrente*2);
                } else {
                    transform.Translate(0,0,velocidadeFrente);
                }
            }
        }
    }

}
function AnimationController(){
    var moveHorizontal = Input.GetAxis("Horizontal");
    var moveVertical = Input.GetAxis("Vertical");
    
    //movimento lateral e/ou movimento vertical (frente e trás)
    if(!isDead){
        if(!isMoveKeyPressed && !isJumping){
            isWalking = false;
            isRunning = false;
            animator.Play("Parado");
        } else {
            if(isRunningKeyPressed){
                isRunning = true;
                isWalking = false;
            } else {
                isWalking = true;
                isRunning = false;
            }
        }
    }
    
}

function UpdateAnimetionParameters(){
    animator.SetBool("isWalking", isWalking);
    animator.SetBool("isRunning", isRunning);
    animator.SetBool("isJumping", isJumping);
    //animator.SetBool("isDead", isDead);
}

function OnCollisionEnter (col : Collision){
    if(col.gameObject.tag == ITEM_TAG){
        // Destroy(col.gameObject);
        animator.SetTrigger(isPickingObject);
    }
    if(col.gameObject.tag == INIMIGOS_TAG){
        ReflectCollision(col);
        
        life--;
        Debug.Log ("Life: " + life);
        if(!isDead && life<=0){
            isDead = true;
            animator.SetBool("isDead", isDead);
            Debug.Log ("Morrendo");
            LoadGameOver();
        } else {
            animator.Play("TomandoDano");
            animator.SetBool(damage, true);
            yield WaitForSeconds(1.2);
            animator.SetBool(damage, false);
        }
    }
    if(col.gameObject.layer == 8 && !isOnGround){
        // Destroy(col.gameObject);
        Debug.Log ("no chão");
        isOnGround = true;
        isJumping=false;
    }

}

function OnCollisionExit(col : Collision){
    if (col.gameObject.layer == 8 && isOnGround){
        Debug.Log ("pulou");
        isOnGround = false;
        isJumping=true;
    }
}

function multiDirectionsMovement(){
    var direction = Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    transform.Translate(direction * 2 * Time.deltaTime);
    //transform.LookAt(direction);
     
}

function LoadGameOver()
{
    animator.Play("Morrendo");
    Debug.Log ("Game Over");
    yield WaitForSeconds(1.2);
    Cursor.visible = true;
    ls.LoadScene("GameOver");
}

function ReflectCollision(col : Collision){
    //rigidBody.AddForce(transform.TransformDirection(Vector3.forward)*-10, ForceMode.Impulse);
    //animator.SetTrigger(damage);

    Debug.Log (damage);
    var dir : Vector3;
    // Calculate Angle Between the collision point and the player
    dir = transform.position - col.transform.position;
    // We then get the opposite (-Vector3) and normalize it
    dir.Normalize();
    // And finally we add force in the direction of dir and multiply it by force. 
    // This will push back the player
    Debug.Log ("Força:"+dir*force);
    rigidBody.AddForce(dir*force);
}

    function UpdateHUD(){
        switch(life){
            case 10:
                //Hud.FadeManager.Instance.fadeImage.sprite =  Resources.Load<Sprite>("LIfe_13");
                break;
            case 8:
                //Hud.FadeManager.Instance.fadeImage.sprite =  Resources.Load<Sprite>("LIfe_14");
                break;
            case 6:
                //Hud.FadeManager.Instance.fadeImage.sprite =  Resources.Load<Sprite>("LIfe_15");
                break;
            case 4:
                //Hud.FadeManager.Instance.fadeImage.sprite =  Resources.Load<Sprite>("LIfe_16");
                break;
            case 2:
                //Hud.FadeManager.Instance.fadeImage.sprite =  Resources.Load<Sprite>("LIfe_17");
                break;
        }
        //FadeManager.Instance.fadeImage;
    }