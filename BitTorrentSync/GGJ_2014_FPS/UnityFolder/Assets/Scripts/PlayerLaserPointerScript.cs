using UnityEngine;
using System.Collections;

public class PlayerLaserPointerScript : MonoBehaviour 
{
	Camera playerCamera;
	LineRenderer laserRenderer;

	public Color laserColor_start = Color.red;
	public Color laserColor_end = Color.blue;


	// Use this for initialization
	void Start () 
	{
		playerCamera = gameObject.GetComponent<Camera>();
		laserRenderer = gameObject.GetComponent<LineRenderer>();
		laserRenderer.useWorldSpace = true;
		laserRenderer.SetColors(laserColor_start, laserColor_end); // using same start/end color for now
		laserRenderer.SetWidth(0.2f, 0.2f);
		laserRenderer.SetVertexCount(2);
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 screenPos = new Vector3(playerCamera.pixelWidth/2.0f, playerCamera.pixelWidth/2.0f, 0);
		Ray laserRay = playerCamera.ScreenPointToRay(screenPos);
		Debug.DrawRay(laserRay.origin, laserRay.direction, Color.green);


		HandleLaser();
	}

	void HandleLaser()
	{

		laserRenderer.SetPosition(0, transform.position);

		Vector3 targetPos = transform.position + 100.0f * transform.forward;

		laserRenderer.SetPosition(1, targetPos);

	}


}
