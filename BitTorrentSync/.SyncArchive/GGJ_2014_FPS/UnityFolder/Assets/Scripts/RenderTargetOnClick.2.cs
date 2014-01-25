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
		var player = GameObject.Find("First Person Controller");
		player.transform.position = transform.FindChild("RenderCamera").transform.position;
	}

}
