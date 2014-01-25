using UnityEngine;
using System.Collections;

public class ScreenObjectScript : MonoBehaviour 
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
		if(renderTexture != null)
		{
			renderer.material.mainTexture = renderTexture;
		}




	}


}
