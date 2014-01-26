using UnityEngine;
using System.Collections;

public class ScreenObjectScript_ManualPair : MonoBehaviour 
{

	public RenderTexture renderTexture;
	public Camera pairedCamera;


	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		renderTexture = pairedCamera.transform.gameObject.GetComponentInChildren<RenderCameraScript_ManualPair>().renderTexture;
		renderer.material.mainTexture = renderTexture;
	}


}

