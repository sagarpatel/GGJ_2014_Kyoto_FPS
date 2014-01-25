using UnityEngine;
using System.Collections;

public class RenderCameraScript1 : MonoBehaviour 
{

	public RenderTexture renderTexture;
	public int textureWidth = 1080;
	public int textureHeight = 720;
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
		transform.parent.transform.renderer.material.mainTexture = renderTexture;
	}


}
