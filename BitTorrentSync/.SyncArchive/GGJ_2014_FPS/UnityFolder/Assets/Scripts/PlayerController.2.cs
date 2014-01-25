using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 10f;
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

		if(Input.GetAxis("Horizontal") || Input.GetAxis("Vertical")){
			var target = 1f;
		}
		var newPosition = Mathf.SmoothDamp(transform.position.y, target.position.y,
		                                           yVelocity, 0.3f);
		transform.position = Vector3(transform.position.x, newPosition, transform.position.z);

		controller.Move(velocity * Time.deltaTime);
	}
}
