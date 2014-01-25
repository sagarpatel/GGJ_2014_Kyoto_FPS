using UnityEngine;
using System.Collections;

public class PlayerController1 : MonoBehaviour {
	
	public float moveSpeed = 10f;	
	
	public float lookSensitivity = 10f;
	public float lookSmoothDamp = 0.1f;
	float yPosition = 0;
	float zPosition = 0;
	float currentYPosition = 0;
	float currentZPosition = 0;
	float yPositionV = 0;
	float zPositionV = 0;
	
	void Update() {
		zPosition = Input.GetAxis("Horizontal") * lookSensitivity;
		yPosition = Input.GetAxis("Vertical") * lookSensitivity;

		currentZPosition = Mathf.SmoothDamp(currentZPosition, zPosition, ref zPositionV, lookSmoothDamp);
		currentYPosition = Mathf.SmoothDamp(currentYPosition, yPosition, ref yPositionV, lookSmoothDamp);
		transform.position = transform.TransformDirection(new Vector3(currentZPosition, 0, currentYPosition));
	}
	
	
	
	
	
	
	/*

	void FixedUpdate () {
		Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Horizontal"));

		velocity = transform.TransformDirection(velocity);
		velocity *= moveSpeed;

		//if(Input.GetAxis("Horizontal") || Input.GetAxis("Vertical")){
		//	var target = 1f;
		//}
		//var newPosition = Mathf.SmoothDamp(transform.position.y, target.position.y, yVelocity, 0.3f);
		//transform.position = Vector3(transform.position.x, newPosition, transform.position.z);

		controller.Move(velocity * Time.deltaTime);
	}
	*/
}