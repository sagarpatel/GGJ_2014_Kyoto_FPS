#pragma strict
var isHungry:boolean = false;
var targetTransform:Vector3;
private var startPosition:Vector3;
private var lerpVelocity:float = 0f;

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
	isHungry = false;
	targetTransform = GameObject.Find("hamburger").transform;
	if(GetComponent("CharacterController")==null)
		gameObject.AddComponent(typeof(CharacterController));
	characterController = GetComponent(CharacterController);
	characterController.radius = 0.5f;
	characterController.center = Vector3 (0, 0, 0.9);
	while(true){
		yield Idle();
		
	}
}

function Hungry(){
	isHungry = true;
}

private var rand:float;
private var randRotateDirection:float;
function Idle(){
	
	while(true){
		if(!isHungry){
		if(Time.time > timeToNewDirection){
			randRotateDirection = Random.Range(-1f, 1f);
			rand = Random.Range(0f, randomValue);
			yield WaitForSeconds(idleTime*rand);
			RandomDirection = Random.value;
			rotation = Vector3(0, 0, RandomDirection);	
			timeToNewDirection = Time.time + directionTraveltime + rand;
			
		}		
		transform.Rotate(rotation * randRotateDirection  * rotateSpeed, RandomDirection* rotateSpeed);

		var walkForward = transform.TransformDirection(Vector3(1,0,0));
		var tmp:float = Mathf.Abs(timeToNewDirection - Time.time);
		var speed:float = (tmp < 1f)? tmp:(Time.time + directionTraveltime + rand - timeToNewDirection);
		characterController.SimpleMove(walkForward * speed * walkSpeed);
		startPosition = this.transform.position;
		}else{
			this.transform.LookAt(targetTransform);
			lerpVelocity += Time.deltaTime;
			lerpVelocity = Mathf.Clamp(lerpVelocity,0f,1f);
			this.transform.position  = Vector3.Lerp(startPosition, targetTransform.position, lerpVelocity);
		
		}
		
		yield;
	}

}