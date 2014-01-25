using UnityEngine;
using System.Collections;

public class ScreenObjectScript : MonoBehaviour 
{

	public RenderTexture renderTexture;
	public Camera pairedCamera;

	// Use this for initialization
	void Start () 
	{
		pairedCamera = gameObject.GetComponentInChildren<RenderCameraScript>().transform.gameObject.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () 
	{

		renderTexture = gameObject.GetComponentInChildren<RenderCameraScript>().renderTexture;
		renderer.material.mainTexture = renderTexture;

	}


}
