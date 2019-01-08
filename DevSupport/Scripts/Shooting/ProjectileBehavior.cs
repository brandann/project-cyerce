using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviorOLD : MonoBehaviour {

    private Rigidbody2D _rigidBody2d;
    private Vector3 _oldVelocity = new Vector3();

	// Use this for initialization
	void Start () {
		_rigidBody2d = this.gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate()
	{
		_oldVelocity = _rigidBody2d.velocity;
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.tag.Contains("Player"))
		{
			// DO NOTHING - SHOULD NEVER REALLY COLLIDE WITH PLAYER
		}
		if (c.gameObject.tag.Contains("Projectile"))
		{
			// DO NOTHING
		}
		else if (c.gameObject.tag.Contains("Wall"))
		{
            //_rigidBody2d.velocity = Vector2.Reflect(_oldVelocity, c.contacts[0].normal);

            // DESTROY WHEN FINISHED WITH THE WALL
            Destroy((this.gameObject));
		}
		else if (c.gameObject.tag.Contains("Planet"))
		{
            //c.gameObject.GetComponent<Planet>().SplitPlanet(this.gameObject);
			//c.gameObject.SendMessage("SplitPlanet");

            // DESTROY WHEN FINISHED WITH THE PLANET
            Destroy((this.gameObject));
		}
        else
        {
            // ANYTHING NOT SPECIFICALLY DELT WITH IS A BOUNCE! WOOHOO
            //_rigidBody2d.velocity = Vector2.Reflect(_oldVelocity, c.contacts[0].normal);

            // ANYTHING NOT SPECIFICALLY DEALT WITH IS A DESTROY
            Destroy((this.gameObject));
        }
	}
}
