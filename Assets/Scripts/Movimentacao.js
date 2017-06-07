#pragma strict

var ls:UnityEngine.SceneManagement.SceneManager;

//[Header("Movement")]
var velocidadeFrente : float;
var velocidadeCima : float;
var groundLimetedXRight : float = 0f;
var groundLimetedXLeft : float = 0f;
var groundLimetedZUp : float = 0f;
var groundLimetedZDown : float = 0f;
private var initialPositionX : float;
private var initialPositionZ : float;
private final var ROTATE_SCALE : float = 45;
private final var ITEM_TAG : String = "Item"; 
private final var INIMIGOS_TAG : String = "Inimigo"; 
private final var turnSpeed : float = 200f;


//[Header("Components")]
var animator : Animator;
var rigidBody : Rigidbody;

//[Header("Animation")]
var isRunning : boolean = false;
var isJumping : boolean = false;
var isOnGround : boolean = true;
private var isKeyPressed : boolean = false;
final var isDying : String = "isDying";
final var isPickingObject : String = "isPickingObject";
final var jump : String = "Jump"; 

function Start () {

    DontDestroyOnLoad(transform.gameObject);
    animator = GetComponent("Animator");
    rigidBody = GetComponent("Rigidbody");
    initialPositionX = transform.position.x;
    initialPositionZ = transform.position.z;
    
}


function Update () {
    velocidadeCima = 3;//*Time.deltaTime;
    velocidadeFrente = 5*Time.deltaTime;
    
    //input
    MovementsController();
    
    //Limitada o terreno do player
    //GroundLimeted();
    //animation / input
    AnimationController();
    //UI update set animation
    UpdateAnimetionParameters();
    
}
function GroundLimeted(){

    if (transform.position.x > (initialPositionX+groundLimetedXRight)) {
        transform.position = new Vector3 (initialPositionX+groundLimetedXRight, transform.position.y, transform.position.z);
    }
    if (transform.position.x < (initialPositionX-groundLimetedXLeft)) {
        transform.position = new Vector3 (initialPositionX-groundLimetedXLeft, transform.position.y, transform.position.z);
    }
    if (transform.position.z > (initialPositionZ+groundLimetedZUp)) {
        transform.position = new Vector3 (transform.position.x, transform.position.y, initialPositionZ+groundLimetedZUp);
    }
    if (transform.position.z < (initialPositionZ-groundLimetedZDown)) {
        transform.position = new Vector3 (transform.position.x, transform.position.y, initialPositionZ-groundLimetedZDown);
    }
}
function MovementsController(){
    //Debug.Log ("isOnGround: "+ isOnGround);
    isKeyPressed = false;

    //mover para frente
    if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)){
        transform.eulerAngles  = new Vector3(0,ROTATE_SCALE*0,0);
        isKeyPressed = true;
    }

    //mover para trás
    if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
        transform.eulerAngles  = new Vector3(0,ROTATE_SCALE*4,0);
        isKeyPressed = true;
    }

    //mover para esquerda
    if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
        transform.eulerAngles  = new Vector3(0,ROTATE_SCALE*6,0);
        isKeyPressed = true;
    }

    //mover para direita
    if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
        transform.eulerAngles  = new Vector3(0,ROTATE_SCALE*2,0);
        isKeyPressed = true;
    }
    
    //mover para frente e direita
    if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
        && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))){
        transform.eulerAngles  = new Vector3(0,ROTATE_SCALE*1,0);
        isKeyPressed = true;
    }

    //mover para frente e esquerda
    if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) 
        && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))){
        transform.eulerAngles  = new Vector3(0,ROTATE_SCALE*7,0);
        isKeyPressed = true;
    }

    //mover para trás e direita
    if((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
        && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))){
        transform.eulerAngles  = new Vector3(0,ROTATE_SCALE*3,0);
        isKeyPressed = true;
    }

    //mover para trás e esquerda
    if((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) 
        && (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))){
        transform.eulerAngles  = new Vector3(0,ROTATE_SCALE*5,0);
        isKeyPressed = true;
    }

    if(isOnGround && isKeyPressed){
        transform.Translate(0,0,velocidadeFrente);
    }

    if(Input.GetKey("space") && isOnGround){
        //transform.Translate(0,velocidadeCima,0);
        rigidBody.AddForce(transform.TransformDirection(Vector3.up)*velocidadeCima, ForceMode.Impulse);
        //rigidBody.AddForce(new Vector2 (0 , velocidadeCima));
        animator.SetTrigger(jump);
    }
}
function AnimationController(){
    var moveHorizontal = Input.GetAxis("Horizontal");
    var moveVertical = Input.GetAxis("Vertical");
    
    //movimento lateral e/ou movimento vertical (frente e trás)
    if(moveHorizontal == 0 && moveVertical == 0){
        isRunning = false;
    } else {
        isRunning = true;
    }
    
}

function UpdateAnimetionParameters(){
    animator.SetBool("isRunning", isRunning);
    animator.SetBool("isJumping", isJumping);
}

function OnCollisionEnter (col : Collision){
    if(col.gameObject.tag == ITEM_TAG){
        // Destroy(col.gameObject);
        animator.SetTrigger(isPickingObject);
    }
    if(col.gameObject.tag == INIMIGOS_TAG){
        // Destroy(col.gameObject);
        animator.SetTrigger(isDying);
        Debug.Log ("Morrendo");
        LoadFirstScene();
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

function LoadFirstScene()
{
    Debug.Log ("RESET");
    yield WaitForSeconds(2);
    ls.LoadScene("Menu");
}