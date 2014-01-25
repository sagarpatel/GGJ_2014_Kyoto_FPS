﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 1f;
	private CharacterController controller;


	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

		velocity = transform.TransformDirection(velocity);
		velocity *= moveSpeed;
	

		controller.Move(velocity * Time.deltaTime);
	}
}