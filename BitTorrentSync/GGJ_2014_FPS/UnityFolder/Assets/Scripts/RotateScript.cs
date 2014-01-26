using UnityEngine;
using System.Collections;

public class RotateScript : MonoBehaviour 
{
	public float rotationSpeed = 10.0f;
	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
	}


}
