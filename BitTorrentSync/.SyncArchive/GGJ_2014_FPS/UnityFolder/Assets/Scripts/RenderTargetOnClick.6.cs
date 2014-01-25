using UnityEngine;
using System.Collections;

public class RenderTargetOnClick : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown () {
		GameObject player = GameObject.Find("First Person Controller");
		Transform renderCam = transform.FindChild("RenderCamera").transform;
		player.transform.position = cam.position;
		Transform mainCam = player.transform.FindChild("Main Camera").transform;
		mainCam.rotation = cam.rotation;
	}

}
