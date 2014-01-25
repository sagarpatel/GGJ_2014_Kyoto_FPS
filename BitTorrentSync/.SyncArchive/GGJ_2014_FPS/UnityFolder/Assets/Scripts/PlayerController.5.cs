﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 10f;
	private CharacterController controller;


	public float lookSensitivity = 10f;
	public float lookSmoothDamp = 0.1f;
	float yPosition = 0;
	float zPosition = 0;
	float currentYPosition = 0;
	float currentZPosition = 0;
	float yPositionV = 0;
	float xPositionV = 0;

	void Start () {
		controller = GetComponent<CharacterController>();
	}

	void Update() {
		yPosition = Input.GetAxis("Horizontal") * lookSensitivity;
		zPosition = Input.GetAxis("Horizontal") * lookSensitivity;
		//zPosition = Mathf.Clamp(zPosition, -90, 90);
		currentZPosition = Mathf.SmoothDamp(currentZPosition, zPosition, ref xPositionV, lookSmoothDamp);
		currentYPosition = Mathf.SmoothDamp(currentYPosition, yPosition, ref yPositionV, lookSmoothDamp);
		transform.position = new Vector3(currentZPosition, currentYPosition, 0);
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
