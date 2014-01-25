using UnityEngine;
using System.Collections;

public class PlayerController2 : MonoBehaviour {

	public float moveSpeed = 10f;
	private CharacterController controller;

	void Start () {
		controller = GetComponent<CharacterController>();
	}

	void FixedUpdate () {
		Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), -9.8f*Time.deltaTime, Input.GetAxis("Vertical"));
		velocity = transform.FindChild("Main Camera").transform.TransformDirection(velocity);
		velocity *= moveSpeed;
		controller.Move(velocity * Time.deltaTime);
	}

}
