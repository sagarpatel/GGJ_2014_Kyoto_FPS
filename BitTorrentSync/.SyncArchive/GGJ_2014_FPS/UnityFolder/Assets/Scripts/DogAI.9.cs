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

		GameObject player = GameObject.FindWithTag("MainCamera");
		float distance = Vector3.Distance(player.transform.position, this.transform.position);
		if(distance < attackDistance){
			velocity *= Time.deltaTime;
			this.transform.position  = Vector3.Lerp(startPosition, targetPosition, Mathf.Clamp(velocity,0f,1f));
		}
		Debug.Log(distance + "  " + attackDistance);
	
	}
}
