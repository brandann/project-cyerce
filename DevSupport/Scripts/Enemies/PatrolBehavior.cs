using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehavior : MonoBehaviour {

    private float Angry_radius = .5f;
    private float Patrol_radius = 2f;

    private Vector3 Angry_scale = new Vector3(2, 2, 2);
    private Vector3 Patrol_scale = new Vector3(1, 1, 1);

    private bool Angry_trigger = false;
    private bool Patrol_trigger = true;

    //private Color Angry_color = Color.red;
    //private Color Patrol_color = Color.white;

    //private float Angry_Chase_dur = 8f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.tag.Contains("Player"))
		{
			c.gameObject.SendMessage("kill");
            GameObject.Find("NotificationText").GetComponent<NotificationManager>().PatrolCaughtPlayerMessage();
		}
	}

    public void SetPoints(Vector3[] p)
    {
		for (int i = 0; i < p.Length; i++)
			p[i] += this.transform.position;
		this.gameObject.GetComponent<TraverseWaypoints>().SetPoints(
			p,
			TraverseWaypoints.eWaypointAction.TraverseNext,
            TraverseWaypoints.eWhereToStart.StartPosition,
			true);
    }

    public void SetAngryState()
    {
		// start seeking
		var col = this.GetComponent<CircleCollider2D>();
		col.radius = Angry_radius;
		col.isTrigger = Angry_trigger;
		//var sprite = this.GetComponent<SpriteRenderer>().color = Angry_color;
		this.transform.localScale = Angry_scale;
        col.enabled = true;
		GameObject.Find("NotificationText").GetComponent<NotificationManager>().AngryPatrolMessage();

        // toggle move state
        this.GetComponent<TraverseWaypoints>().enabled = false;
        var seek = this.GetComponent<SeekBehavior>();
        seek.enabled = true;
        seek.SetTarget(GameObject.FindWithTag("Player"));
    }

    public void SetPatrolState()
    {
		// start seeking
		var col = this.GetComponent<CircleCollider2D>();
		col.radius = Patrol_radius;
		col.isTrigger = Patrol_trigger;
		//var sprite = this.GetComponent<SpriteRenderer>().color = Patrol_color;
		this.transform.localScale = Patrol_scale;
        col.enabled = false;
		//GameObject.Find("NotificationText").GetComponent<NotificationManager>().LostPlayerBackToPatrol();

		// toggle move state
		this.GetComponent<TraverseWaypoints>().enabled = true;
        var seek = this.GetComponent<SeekBehavior>();
        seek.enabled = false;
        seek.SetTarget(null);
    }

}
