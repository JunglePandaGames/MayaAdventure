#pragma strict

var velocidadeFrente : float;
var velocidadeCima : float;
var velocidadeLado : float;


function Start () {

}

function Update () {

velocidadeCima = 15*Time.deltaTime;
velocidadeFrente = 10*Time.deltaTime;
velocidadeLado = 10*Time.deltaTime;

if(Input.GetKey("w"))

{

transform.Translate(0,0,velocidadeFrente);

	}

if(Input.GetKey("s"))

{

transform.Translate(0,0,-velocidadeFrente);

	}

if(Input.GetKey("a"))

{

transform.Translate(-velocidadeLado,0,0);

	}

if(Input.GetKey("d"))

{

transform.Translate(velocidadeLado,0,0);

	}

if(Input.GetKey("space"))

{

transform.Translate(0,velocidadeCima,0);

	}
}
