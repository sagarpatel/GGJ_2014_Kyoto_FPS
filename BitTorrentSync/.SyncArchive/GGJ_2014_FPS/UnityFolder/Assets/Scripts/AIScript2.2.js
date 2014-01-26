#pragma strict
var walkSpeed = 10.0;
var rotateSpeed = 3.0;

var directionTraveltime = 1.0;
var idleTime = 0.2;
var randomValue = 2.0;

private var rotation:Vector3;
var RandomDirection:float;
private var timeToNewDirection=0.0;
private var characterController:CharacterController;

function Start () {
	if(GetComponent("CharacterController")==null)
		gameObject.AddComponent(typeof(CharacterController));
	characterController = GetComponent(CharacterController);
	characterController.center = Vector3 (0, 0, 3.5);
	while(true){
		yield Idle();
		
	}
}

private var rand:float;
private var randRotateDirection:float;
function Idle(){
	
	while(true){
		if(Time.time > timeToNewDirection){
			randRotateDirection = Random.Range(-1f, 1f);
			rand = Random.Range(0f, randomValue);
			yield WaitForSeconds(idleTime*rand);
			RandomDirection = Random.value;
			rotation = Vector3(0, 0, RandomDirection);	
			timeToNewDirection = Time.time + directionTraveltime + rand;
			
		}		
		transform.Rotate(rotation * randRotateDirection  * rotateSpeed, RandomDirection* rotateSpeed);

		var walkForward = transform.TransformDirection(Vector3(0,-1,0));
		var tmp:float = Mathf.Abs(timeToNewDirection - Time.time);
		var speed:float = (tmp < 1f)? tmp:(Time.time + directionTraveltime + rand - timeToNewDirection);
		characterController.SimpleMove(walkForward * speed * walkSpeed);
		
		yield;
	}

}