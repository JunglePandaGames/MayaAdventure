#pragma strict

var target : Transform;
// ROTATE CAMERA:
var rotation:Quaternion;

function Start () {
    // ROTATE CAMERA:
    var rotation = Quaternion.Euler(0, 0, 0);
}

function Update () {
    // transform.position = target.position;
    transform.position = new Vector3(target.position.x, transform.position.y,target.position.z);
    
}
