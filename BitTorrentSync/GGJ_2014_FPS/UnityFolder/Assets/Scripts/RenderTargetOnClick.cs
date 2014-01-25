using UnityEngine;
using System.Collections;

public class RenderTargetOnClick : MonoBehaviour {

	void OnMouseDown () {
		GameObject player = GameObject.Find("First Person Controller");
		Transform renderCam = transform.FindChild("RenderCamera").transform;
		player.transform.position = renderCam.position;
		Transform mainCam = player.transform.FindChild("Main Camera").transform;
		mainCam.rotation = renderCam.rotation;
	}

}
