using UnityEngine;
using System.Collections;

public class RenderCameraScript_ManualPair : MonoBehaviour 
{

	public RenderTexture renderTexture;
	public int textureWidth = 1080;
	public int textureHeight = 720;
	Camera renderCamera;

	public GameObject pairedScreen;

	// Use this for initialization
	void Start () 
	{

		renderTexture = new RenderTexture(textureWidth, textureHeight, 16, RenderTextureFormat.ARGB32);
		renderCamera = gameObject.GetComponent<Camera>();
		renderCamera.targetTexture = renderTexture;
		
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
