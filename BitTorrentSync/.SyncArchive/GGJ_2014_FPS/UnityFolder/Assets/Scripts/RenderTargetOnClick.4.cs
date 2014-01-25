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
		Transform cam = transform.FindChild("RenderCamera").transform;
		player.transform.position = cam.position;
		player.transform.rotation = cam.rotation;
	}

}
