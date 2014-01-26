using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	public AudioClip doorSE;
	public float unlockableDisntance = 4f;
	private bool isUnlockable = false;
	private int labelWidth=120;
	private int labelHeight=40;

	// Use this for initialization
	void Start () {
		isUnlockable = false;
	}
	
	// Update is called once per frame
	void Update () {
		GameObject player = GameObject.FindWithTag("MainCamera");
		float distance = Vector3.Distance(player.transform.position, this.transform.position);
		bool isKey = false;
		foreach (Transform child in player.transform){
			//Debug.Log(child.gameObject.name);
			if(child.gameObject.name == "house key"){
				isKey = true;
			}
		}
		if(distance < unlockableDisntance && isKey){
			isUnlockable = true;
		}
		if( isUnlockable && Input.GetKeyDown ("space")){
			Destroy (this.gameObject);
		}
	}

	void OnGUI(){
		var box_x = Screen.width/2 - labelWidth / 2;
		var box_y = Screen.height/2 - labelHeight / 2;
		if(isUnlockable){
			GUI.Box( new Rect(box_x,box_y,labelWidth,labelHeight)
			        ,"Open the Door!!\nPush Space");
		}
	}

}
