#pragma strict

var target : Transform;
public var distance = 20;
public var targetHeight = 0;
private var x = 0.0;
private var y = 0.0;  
 
function Start () {
    var angles = transform.eulerAngles;
    x = angles.x;
    y = angles.y;
}
 
function LateUpdate () {
   if(!target)
      return;
   
      y = target.eulerAngles.y;
 
 
  // ROTATE CAMERA:
   var rotation:Quaternion = Quaternion.Euler(x, 0, 0);
   transform.rotation = rotation;
   
   // POSITION CAMERA:
   var position = target.position - (rotation * Vector3.forward * distance + Vector3(0,-targetHeight,0));
   transform.position = position;
 
}