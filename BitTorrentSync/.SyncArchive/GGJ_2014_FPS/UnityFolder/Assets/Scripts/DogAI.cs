using UnityEngine;
using System.Collections;

public class DogAI : MonoBehaviour {

	public Vector3 moveDirection;
	private float distance;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.FindWithTag("Player");
		distance = Vector3.Distance(player.transform.position, this.transform.position);

	
	}
}
