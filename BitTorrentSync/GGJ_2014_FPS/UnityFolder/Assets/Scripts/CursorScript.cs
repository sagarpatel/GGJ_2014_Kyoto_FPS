using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour 
{	
	public float rotationSpeed = 10.0f;
	public Color cursorColor;

	// Use this for initialization
	void Start () 
	{
		renderer.material.color = cursorColor;
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
	}
}
