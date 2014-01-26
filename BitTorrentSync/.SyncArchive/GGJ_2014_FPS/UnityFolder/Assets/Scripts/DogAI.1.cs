using UnityEngine;
using System.Collections;

public class DogAI : MonoBehaviour {

	public Vector3 movePoint;
	private float attackDistance;
	private Vector3 startPosition;
	private float velocity;

	// Use this for initialization
	void Start () {
		startPosition = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.FindWithTag("Player");
		float distance = Vector3.Distance(player.transform.position, this.transform.position);
		if(distance < attackDistance){
			Mathf.SmoothDamp(startPosition, movePoint,ref velocity, Time.deltaTime);
		}
	
	}
}
