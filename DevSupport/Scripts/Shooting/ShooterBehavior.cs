using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBehavior : MonoBehaviour {

    public GameObject Projectle;
    private const float PROJECTILE_SPEED = 40;
    private const int BURST_COUNT = 12;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

			//if (Input.GetMouseButtonDown(0))
			//{
			//	// GET MOUSE POSITION
			//	Vector3 pos = Input.mousePosition;
			//	pos.z = 20;
			//	pos = Camera.main.ScreenToWorldPoint(pos);
			//	pos.z = 0;

			//	Shoot(pos);
			//}
	}

    public void Burst(Vector3 position)
    {
        // TODO: make this a coroutine
        float rotationDegree = 0;
		for (int i = 0; i < BURST_COUNT; i++)
		{
			// INSTANTIATE OBJECTS
			var go = GameObject.Instantiate(Projectle);

			// DO LOGIC
			rotationDegree = (360 / BURST_COUNT) * i;
			go.transform.Rotate(new Vector3(0, 0, rotationDegree));
			go.transform.position = this.transform.position + go.transform.up;

			go.GetComponent<Rigidbody2D>().velocity = go.transform.up * PROJECTILE_SPEED;
		}
    }

    public void Shoot(Vector3 position)
    {
		// TODO: make this a coroutine
		
        // GET THE VECTOR BETWEEN THE PLAYER AND THE MOUSE POSITION
		var VecBetweenPlayerAndMouse = position - this.transform.position;

		// INSTANTIATE OBJECTS
		var go = GameObject.Instantiate(Projectle);

		// SET THE PROJECTILE POSITION AND ROTATION
		go.transform.position = this.transform.position + VecBetweenPlayerAndMouse.normalized;
		go.transform.up = VecBetweenPlayerAndMouse;

		// GIVE THE PROJECTILE A VELOCITY TO MOVE
		go.GetComponent<Rigidbody2D>().velocity = VecBetweenPlayerAndMouse.normalized * PROJECTILE_SPEED;
    }
}
