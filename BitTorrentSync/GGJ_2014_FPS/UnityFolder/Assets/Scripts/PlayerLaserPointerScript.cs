using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerLaserPointerScript : MonoBehaviour 
{
	Camera playerCamera;
	LineRenderer laserRenderer;

	public Color laserColor_start = Color.red;
	public Color laserColor_end = Color.blue;
	public float laserStartOffset = 0.2f;
	public float laserLength = 10.0f;

	public List<Vector3> laserVerticesList;

	// Use this for initialization
	void Start () 
	{
		playerCamera = gameObject.GetComponent<Camera>();
		laserRenderer = gameObject.GetComponent<LineRenderer>();
		laserRenderer.useWorldSpace = true;
		laserRenderer.SetColors(laserColor_start, laserColor_end); // using same start/end color for now
		laserRenderer.SetWidth(0.1f, 0.1f);
		laserRenderer.SetVertexCount(2);

		laserVerticesList = new List<Vector3>();
		laserVerticesList.Add(transform.position);
		laserVerticesList.Add(transform.position + laserLength * transform.forward);

	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 screenPos = new Vector3(playerCamera.pixelWidth/2.0f, playerCamera.pixelWidth/2.0f, 0);
		Ray laserRay = playerCamera.ScreenPointToRay(screenPos);
		RaycastHit hitInfo;
		if(Physics.Raycast(laserRay, out hitInfo, laserLength) )
		{
			Debug.Log("HHTIIING");
			laserVerticesList[1] = hitInfo.point;
		}
		else
		{
			laserVerticesList[1] = transform.position + laserLength * laserRay.direction;
		}


		HandleLaser();
	}

	void HandleLaser()
	{

		Vector3 startPosition = transform.position;
		startPosition -= laserStartOffset * transform.up;
		laserVerticesList[0] = startPosition;

		for(int i =0; i < laserVerticesList.Count; i++)
		{
			laserRenderer.SetPosition(i, laserVerticesList[i]);
		}

	}


}
