using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolView : MonoBehaviour {


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag.Contains("Player"))
		{
			this.transform.parent.GetComponent<PatrolBehavior>().SetAngryState();
            this.GetComponent<CircleCollider2D>().enabled = false;
            this.GetComponent<SpriteRenderer>().enabled = false;
		}
	}

    public void ToggleViewOn()
    {
		this.GetComponent<CircleCollider2D>().enabled = true;
		this.GetComponent<SpriteRenderer>().enabled = true;
    }
}
