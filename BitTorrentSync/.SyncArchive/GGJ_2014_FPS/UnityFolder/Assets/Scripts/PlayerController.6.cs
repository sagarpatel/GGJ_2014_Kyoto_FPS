using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 10f;
	private CharacterController controller;

	void Start () {
		controller = GetComponent<CharacterController>();
	}

	void FixedUpdate () {
		Vector3 velocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Horizontal"));

		velocity = transform.TransformDirection(velocity);
		velocity *= moveSpeed;
		controller.Move(velocity * Time.deltaTime);
	}

}
