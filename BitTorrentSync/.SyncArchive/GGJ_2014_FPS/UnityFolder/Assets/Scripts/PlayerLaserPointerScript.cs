using UnityEngine;
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

	public GameObject cursorObjectPrefab;
	GameObject cursorObject;
	
	public float teleportHeighOffset = 1.0f;
	public Vector3 grabbedObjectPosition;

	public int inceptionLimit = 5;
	public int inceptionLevelCounter = 0;

	GameObject currentlyGrabbedObject;
	public Vector3 objectDropOffset;

	public float warpAnimationDuration = 1.0f;

	Vector3 teleportPosition;

	// Use this for initialization
	void Start () 
	{
		playerCamera = gameObject.GetComponent<Camera>();
		laserRenderer = gameObject.AddComponent<LineRenderer>();
		laserRenderer.useWorldSpace = true;
		laserRenderer.material = laserRendererMaterial; //new Material(Shader.Find("Particles/Additive"));
		laserRenderer.SetColors(laserColor_start, laserColor_end); // using same start/end color for now
		laserRenderer.SetWidth(0.0151f, 0.0151f);
		laserRenderer.SetVertexCount(2);

		laserVerticesList = new List<Vector3>();
		laserVerticesList.Add(transform.position);
		laserVerticesList.Add(transform.position + laserLength * transform.forward);

		cursorObject = (GameObject)Instantiate(cursorObjectPrefab, transform.position, Quaternion.identity);
		//cursorObject.transform.localScale = new Vector3(cursorScale, cursorScale, cursorScale);
		//cursorObject.GetComponent<SphereCollider>().enabled = false;

		Screen.showCursor = false;

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
		Camera renderCamera = screenObject.GetComponentInChildren<ScreenObjectScript_ManualPair>().pairedCamera;
		Vector3 pixelCoord = new Vector3( renderCamera.pixelWidth * screenObjectHitInfo.textureCoord.x, renderCamera.pixelHeight * screenObjectHitInfo.textureCoord.y, 4);
		
		Ray tempLaserRay = renderCamera.ScreenPointToRay(pixelCoord);
		RaycastHit tempHitInfo;
		if(Physics.Raycast(tempLaserRay, out tempHitInfo, laserLength) )
		{
			
			cursorObject.transform.position = tempHitInfo.point;
			/*
			if(tempHitInfo.transform.gameObject.tag == "Screen")
				cursorObject.renderer.enabled = true;
			else
				cursorObject.renderer.enabled = false;
			*/


			if(Input.GetButtonUp("Fire2"))
			{
				// Check if already holding an object
				bool isCurrentlyGrabbingObject = false;
				
				foreach(Transform child in transform)
				{
					Debug.Log(child.gameObject);
					if(child.gameObject.tag == "Grab")
						currentlyGrabbedObject = child.gameObject;
				}

				// Only try to grab something if not holding anything
				if(currentlyGrabbedObject == null)
				{
					Debug.Log("TRYING TO GRAB");
					Debug.Log(tempHitInfo.transform.gameObject);
					if(tempHitInfo.transform.gameObject.tag == "Grab")
					{
						Debug.Log(tempHitInfo.transform.gameObject);
						Debug.Log("ABOUT TO GRAB");
						GrabGameOobject(tempHitInfo, renderCamera);
						currentlyGrabbedObject = tempHitInfo.transform.gameObject;				
					}
				}
				else // Drop the object into the scene
				{	
					Debug.Log("DROPPIN IT LIKE ITS HAWT");
					currentlyGrabbedObject.transform.parent = null;
					currentlyGrabbedObject.transform.position = cursorObject.transform.position + objectDropOffset;
					currentlyGrabbedObject = null;
				}
			}
			else
			{
				// Teleport
				if(Input.GetButtonUp("Fire1"))
				{
					if(tempHitInfo.transform.gameObject.tag == "Screen" && inceptionLevelCounter < inceptionLimit )
					{
						//  RECURSE FOREVERMORE
						inceptionLevelCounter ++;
						HandleLaserOnScreen(tempHitInfo);
					}
					else
					{
						inceptionLevelCounter = 0;
						teleportPosition= cursorObject.transform.position + new Vector3(0, teleportHeighOffset, 0);
						StartCoroutine("AnimateWarp");
						
					}
				}
			}

		}
		

		

	}

	void GrabGameOobject(RaycastHit grabObjectHitInfo, Camera lastCamera)
	{
		GameObject targetGameObject = grabObjectHitInfo.transform.gameObject;

		if(targetGameObject.GetComponent<GrabbableObjectScript>().isScaled == true)
		{
			targetGameObject.transform.localScale *= 1.0f; //CalculateScreenScale(grabObjectHitInfo, lastCamera);
		}

		targetGameObject.transform.parent = transform;
		targetGameObject.transform.localPosition = grabbedObjectPosition;
		targetGameObject.transform.rotation = Quaternion.identity;
	}

	float CalculateScreenScale(RaycastHit grabObjectHitInfo, Camera lastCamera)
	{
		//distance from last camera to object
		float distanceToObject = grabObjectHitInfo.distance;
		float cameraFOV = lastCamera.fieldOfView;

		// TRIG TIME
		float cameraWidth = distanceToObject * Mathf.Tan(cameraFOV * Mathf.Deg2Rad);
		
		// need to find width of the physical screen displaying this camera (this allows us to caluclated the scaling)
		GameObject screenObject = lastCamera.transform.gameObject.GetComponent<RenderCameraScript_ManualPair>().pairedScreen;
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

	IEnumerator AnimateWarp()
	{
		float timeCounter = 0;
		while(true)
		{	
			Debug.Log("IM WARPOING");
			timeCounter += Time.deltaTime;

			playerCamera.fieldOfView = Mathf.Lerp(60, 179, timeCounter/warpAnimationDuration) ;

			if(timeCounter > warpAnimationDuration)
				break;

			//Debug.Log()
			yield return null;
		}

		transform.parent.position = teleportPosition; // cursorObject.transform.position + new Vector3(0, teleportHeighOffset, 0);
		playerCamera.fieldOfView = 60.0f;

	}


}
