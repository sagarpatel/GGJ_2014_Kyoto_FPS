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

	GameObject cursorObject;
	public float cursorScale = 0.1f;

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

		cursorObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		cursorObject.transform.localScale = new Vector3(cursorScale, cursorScale, cursorScale);

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
			if(hitInfo.transform.gameObject.tag == "Screen")
			{
				HandleLaserOnScreen(hitInfo);
			}
		}
		else
		{
			laserVerticesList[1] = transform.position + laserLength * laserRay.direction;
		}


		HandleLaser();

		if(Input.GetButtonUp("Fire1"))
		{
			transform.position = cursorObject.transform.position;
		}
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

	void HandleLaserOnScreen(RaycastHit screenObjectHitInfo)
	{
		GameObject screenObject = screenObjectHitInfo.transform.gameObject;
		Vector3 screenCoordinates = new Vector3(0, 0, 0);

		Vector3 worldPositionHit = screenObjectHitInfo.point;
		screenCoordinates = screenObject.transform.position - worldPositionHit;
		



		// place cursor in world space 
		Camera renderCamera = screenObject.GetComponent<ScreenObjectScript>().pairedCamera;
		Vector3 pixelCoord = new Vector3( renderCamera.pixelWidth * screenObjectHitInfo.textureCoord.x, renderCamera.pixelHeight * screenObjectHitInfo.textureCoord.y, 10);
		

		Vector3 worldPositionCursor = renderCamera.ScreenToWorldPoint(pixelCoord);
		cursorObject.transform.position = worldPositionCursor;

		Debug.Log(worldPositionCursor);

	}


}
