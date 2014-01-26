using UnityEngine;
using System.Collections;

public class DogAI : MonoBehaviour {

	public AudioClip se;
	private  Vector3 targetPosition;
	private float attackDistance;
	private Vector3 startPosition;
	private float velocity;
	private AudioSource audioSource;
	private bool isAttack = false;
	// Use this for initialization
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();
		velocity = 0f;
		attackDistance = 6f;
		targetPosition = new Vector3(-2.5f, 0.3f, 3f);
		startPosition = this.transform.position;
		isAttack = false;
	}

	// Update is called once per frame
	void Update () {

		GameObject player = GameObject.FindWithTag("MainCamera");
		float distance = Vector3.Distance(player.transform.position, this.transform.position);
		if(distance < attackDistance){
			if(isAttack == false){
				isAttack = true;
				audioSource.Play();
			}
			velocity += Time.deltaTime;
			velocity = Mathf.Clamp(velocity,0f,1f);
			this.transform.position  = Vector3.Lerp(startPosition, targetPosition, velocity);
		}else{
			/*
			velocity -= Time.deltaTime;
			velocity = Mathf.Clamp(velocity,0f,1f);
			this.transform.position  = Vector3.Lerp(targetPosition, startPosition, velocity);
			*/
		}
	
	}
}
