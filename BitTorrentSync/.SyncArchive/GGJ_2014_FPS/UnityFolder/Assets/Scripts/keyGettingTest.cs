using UnityEngine;
using System.Collections;

public class keyGettingTest : MonoBehaviour {

	public AudioClip getSE;
	private float lastTime = 0f;
	private bool isGetKey = false;
	private AudioSource audioSource;

	void Start(){
		isGetKey = false;
		audioSource = gameObject.GetComponent<AudioSource>();
	}

	void Update()
	{
		if(Time.time - lastTime > 0.5f && isGetKey){
			this.gameObject.SetActiveRecursively(false);
		}
	}

	void GetKey()
	{
		isGetKey = true;
		lastTime = Time.time;
		GameObject.FindWithTag("Player").SendMessage("GetKey");
		audioSource.Play();
	}

}
