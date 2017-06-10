#pragma strict

//[Header("Movement")]
var velocidadeFrente : float;
var velocidadeCima : float;
var velocidadeLado : float;
private final var ROTATE_SCALE : float = 90;
private final var ITEM_TAG : String = "Item"; 
private final var turnSpeed : float = 200f;


//[Header("Components")]
var animator : Animator;
var rigidBody : Rigidbody;

//[Header("Animation")]
var isRunning : boolean = false;
var isJumping : boolean = false;
final var isPickingObject : String = "isPickingObject"; 

function Start () {
    animator = GetComponent("Animator");
    rigidBody = GetComponent("Rigidbody");
}

function Update () {
    velocidadeCima = 15*Time.deltaTime;
    velocidadeFrente = 10*Time.deltaTime;
    velocidadeLado = 10*Time.deltaTime;
    //input
    MovementsController();
    //animation / input
    AnimationController();
    //UI update set animation
    UpdateAnimetionParameters();
    
}
function MovementsController(){
    //mover para frente
    if(Input.GetKey("w")){
        transform.Translate(0,0,velocidadeFrente);
        //transform.Rotate = new Vector3(0,ROTATE_SCALE*0,0);
        
    }

    //mover para trás
    if(Input.GetKey("s")){
        transform.Translate(0,0,-velocidadeFrente);
        //transform.Rotate = new Vector3(0,ROTATE_SCALE*2,0);
    }

    //mover para esquerda
    if(Input.GetKey("a")){
        transform.Translate(-velocidadeLado,0,0);
        //transform.Rotate(Vector3(0,ROTATE_SCALE*1,0));
        transform.Rotate(Vector3.up , -turnSpeed * Time.deltaTime);
        
    }

    //mover para direita
    if(Input.GetKey("d")){
        transform.Translate(velocidadeLado,0,0);
        //transform.Rotate((0,ROTATE_SCALE*3,0));
        transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
        
    }

    if(Input.GetKey("space")){
        transform.Translate(0,velocidadeCima,0);
    }
}
function AnimationController(){
    var moveHorizontal = Input.GetAxis("Horizontal");
    var moveVertical = Input.GetAxis("Vertical");
    var moveJump = Input.GetAxis("Jump");

    //movimento lateral e/ou movimento vertical (frente e trás)
    if(moveHorizontal == 0 && moveVertical == 0){
        isRunning = false;
    } else {
        isRunning = true;
    }
    
    //movimento Jump
    if(moveJump == 0){
        isJumping = false;
    } else {
        isJumping = true;
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
}