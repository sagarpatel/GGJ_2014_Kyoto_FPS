using UnityEngine;
using System.Collections;

public class DogAI : MonoBehaviour {

	public Vector3 targetPosition;
	public float attackDistance;
	private Vector3 startPosition;
	private float velocity;

	// Use this for initialization
	void Start () {
		velocity = 1f;
		startPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		velocity *= Time.deltaTime;
		GameObject player = GameObject.FindWithTag("MainCamera");
		float distance = Vector3.Distance(player.transform.position, this.transform.position);
		if(distance < attackDistance){
			this.transform.position  = Vector3.Lerp(startPosition, targetPosition, velocity);
		}
		Debug.Log(distance + "  " + attackDistance);
	
	}
}
