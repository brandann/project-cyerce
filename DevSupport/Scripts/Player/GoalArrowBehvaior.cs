using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalArrowBehvaior : MonoBehaviour {

    private GameObject GoalObject;
    private Vector3 BetweenPlayerAndGoal;

	// Use this for initialization
	void Start () {
        GoalObject = GameObject.FindGameObjectWithTag("Goal");
        BetweenPlayerAndGoal = new Vector3();
	}
	
	// Update is called once per frame
	void Update () {
        BetweenPlayerAndGoal = GoalObject.transform.position - this.transform.position;	
        this.transform.up = BetweenPlayerAndGoal;
	}
}
