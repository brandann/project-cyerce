﻿using UnityEngine;
using System.Collections;

public class StayOnObject : MonoBehaviour {
    public GameObject mTargetObject;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(mTargetObject == null)
        {
            mTargetObject = GameObject.FindWithTag("Player");
            return;
        }
            
		if (!toggle)
			return;


		//this.transform.parent = mTargetObject.transform;
        
        // KEEP THIS TRANSFORM AT THE TARGET POSITION
        // -10 IS FOR THE CAMERA TO KEEP THE CAMERA AT THE
        // STANDARD CAMERA HEIGHT.
        this.transform.position = new Vector3(
            mTargetObject.transform.position.x,
            mTargetObject.transform.position.y,
            -10);

		//this.gameObject.GetComponent<StayOnObject>().enabled = false;
	}

	bool toggle = true;

	public void ToggleStay()
	{
		toggle = !toggle;
	}
}
