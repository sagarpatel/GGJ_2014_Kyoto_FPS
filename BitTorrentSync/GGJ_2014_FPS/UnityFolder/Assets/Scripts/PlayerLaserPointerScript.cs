﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerLaserPointerScript : MonoBehaviour 
{
	Camera playerCamera;
	LineRenderer laserRenderer;

	public Material laserRendererMaterial;

	public Color laserColor_start = Color.red;
	public Color laserColor_end = Color.blue;
	public float laserStartOffset = 0.2f;
	public float laserLength = 100.0f;

	public List<Vector3> laserVerticesList;

	GameObject cursorObject;
	public float cursorScale = 0.1f;

	public float teleportHeighOffset = 1.0f;
	public Vector3 grabbedObjectPosition;

	// Use this for initialization
	void Start () 
	{
		playerCamera = gameObject.GetComponent<Camera>();
		laserRenderer = gameObject.AddComponent<LineRenderer>();
		laserRenderer.useWorldSpace = true;
		laserRenderer.material = laserRendererMaterial; //new Material(Shader.Find("Particles/Additive"));
		laserRenderer.SetColors(laserColor_start, laserColor_end); // using same start/end color for now
		laserRenderer.SetWidth(0.1f, 0.1f);
		laserRenderer.SetVertexCount(2);

		laserVerticesList = new List<Vector3>();
		laserVerticesList.Add(transform.position);
		laserVerticesList.Add(transform.position + laserLength * transform.forward);

		cursorObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
		cursorObject.transform.localScale = new Vector3(cursorScale, cursorScale, cursorScale);
		cursorObject.GetComponent<SphereCollider>().enabled = false;

	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 screenPos = new Vector3(playerCamera.pixelWidth/2.0f, playerCamera.pixelWidth/3.0f, 0);
		Ray laserRay = playerCamera.ScreenPointToRay(screenPos);
		RaycastHit hitInfo;
		if(Physics.Raycast(laserRay, out hitInfo, laserLength) )
		{
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
		Camera renderCamera = screenObject.GetComponentInChildren<ScreenObjectScript>().pairedCamera;
		Vector3 pixelCoord = new Vector3( renderCamera.pixelWidth * screenObjectHitInfo.textureCoord.x, renderCamera.pixelHeight * screenObjectHitInfo.textureCoord.y, 4);
		
		Ray tempLaserRay = renderCamera.ScreenPointToRay(pixelCoord);
		RaycastHit tempHitInfo;
		if(Physics.Raycast(tempLaserRay, out tempHitInfo, laserLength) )
		{
			if(tempHitInfo.transform.gameObject.tag == "Grab")
			{
				if(Input.GetButtonUp("Fire2"))
				{
					GrabGameOobject(tempHitInfo, renderCamera);
				}
				
			}
			else
			{
				cursorObject.transform.position = tempHitInfo.point;
				if(Input.GetButtonUp("Fire1"))
				{
					transform.parent.position = cursorObject.transform.position + new Vector3(0, teleportHeighOffset, 0);
				}
			}

		}

		

	}

	void GrabGameOobject(RaycastHit grabObjectHitInfo, Camera lastCamera)
	{
		GameObject targetGameObject = grabObjectHitInfo.transform.gameObject;

		if(targetGameObject.GetComponent<GrabbableObjectScript>().isScaled == true)
		{
			targetGameObject.transform.localScale *= CalculateScreenScale(grabObjectHitInfo, lastCamera);
		}

		targetGameObject.transform.parent = transform;
		targetGameObject.transform.localPosition = grabbedObjectPosition;
	}

	float CalculateScreenScale(RaycastHit grabObjectHitInfo, Camera lastCamera)
	{
		//distance from last camera to object
		float distanceToObject = grabObjectHitInfo.distance;
		float cameraFOV = lastCamera.fieldOfView;

		// TRIG TIME
		float cameraWidth = distanceToObject * Mathf.Tan(cameraFOV * Mathf.Deg2Rad);
		
		// need to find width of the physical screen displaying this camera (this allows us to caluclated the scaling)
		GameObject screenObject = lastCamera.transform.gameObject.GetComponent<RenderCameraScript>().pairedScreen;
		Bounds screenBounds = screenObject.GetComponent<Renderer>().bounds;
		float screenWidth = screenBounds.size.x;

		// how much space does the screen take up in the players camera
		/*
		Vector3 virtualAlignedScreenPos = playerCamera.transform.position + new Vector3(0, 0, Vector3.Distance(screenObject.transform.position, playerCamera.transform.position));
		Vector3 screenLeft = virtualAlignedScreenPos - new Vector3(screenWidth/2.0f,0,0);
		Vector3 screenRight = virtualAlignedScreenPos + new Vector3(screenWidth/2.0f,0,0);

		Vector3 screenCoordLeft = playerCamera.WorldToViewportPoint(screenLeft);
		Vector3 screenCoordRight = playerCamera.WorldToViewportPoint(screenRight);

		Debug.Log(screenCoordLeft);
		Debug.Log(screenCoordRight);
		*/


		
		float scaleRatio = screenWidth / cameraWidth;
		/*
		float playerCameraScaleRatio = screenWidth /Vector3.Distance(screenObject.transform.position, playerCamera.transform.position);
		Debug.Log(screenWidth);
		Debug.Log(Vector3.Distance(screenObject.transform.position, playerCamera.transform.position));
		Debug.Log(playerCameraScaleRatio);

		*/


		Vector3 screenCoordLeft = playerCamera.WorldToViewportPoint(screenBounds.min);
		Vector3 screenCoordRight = playerCamera.WorldToViewportPoint(screenBounds.max);
		screenCoordLeft.z = 0;
		screenCoordRight.z = 0;
		float viewportScaler =  Vector3.Distance(screenCoordLeft, screenCoordRight);

		Debug.Log(viewportScaler);

		return scaleRatio * viewportScaler ;
	}


}
