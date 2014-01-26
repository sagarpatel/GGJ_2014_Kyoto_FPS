using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

	public bool isGetKey = false;
	
	// Use this for initialization
	void Start () 
	{
		isGetKey = false;
	}
	
	void OnGUI(){
		if(isGetKey)
			GUI.Label(new Rect (10, 10, 100, 20), "I have a key.");
	}

	void GetKey(){
		isGetKey = true;
	}

}
