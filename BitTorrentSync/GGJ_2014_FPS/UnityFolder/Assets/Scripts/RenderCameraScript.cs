﻿using UnityEngine;
using System.Collections;

public class RenderCameraScript : MonoBehaviour 
{

	public RenderTexture renderTexture;
	public int textureWidth = 108;
	public int textureHeight = 72;
	Camera renderCamera;
	// Use this for initialization
	void Start () 
	{
		renderTexture =  new RenderTexture(textureWidth, textureHeight, 16, RenderTextureFormat.ARGB32);
		renderCamera = gameObject.GetComponent<Camera>();
		renderCamera.targetTexture = renderTexture;
	}
	
	// Update is called once per frame
	void Update () 
	{

		GameObject testScreen = GameObject.FindWithTag("Screen");
		testScreen.renderer.material.mainTexture = renderTexture;
	
	}


}
