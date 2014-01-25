using UnityEngine;
using System.Collections;

public class PlayerController2 : MonoBehaviour {

	public float moveSpeed = 10f;
	private CharacterController controller;

	void Start () {
		controller = GetComponent<CharacterController>();
	}

	void FixedUpdate () {
		Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		velocity = transform.FindChild("Main Camera").transform.TransformDirection(velocity);
						//+ new Vector3(0f, -9.8f*Time.deltaTime, 0f);
		velocity *= moveSpeed;
		controller.rigidbody.AddForce(new Vector3(0f, -9.8f*Time.deltaTime, 0f));
		controller.Move(velocity * Time.deltaTime);
	}

}
