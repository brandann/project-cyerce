using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float Speed;
    public float VelocityLerp;

    //PRIVATE
    private Rigidbody2D _rigidBody2D;

	// Use this for initialization
	void Start () {
        _rigidBody2D = this.GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        HandleKeyboardInput();

	}

    private void HandleKeyboardInput()
    {
        var MoveInput = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            MoveInput.y++;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            MoveInput.x--;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            MoveInput.y--;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            MoveInput.x++;
        }

        SetVelocityInput(MoveInput);
    }

    private void SetVelocityInput(Vector2 input)
    {
        var raw_velocity = input * Speed * Time.deltaTime;
        var new_velocity = Vector3.Lerp(_rigidBody2D.velocity, raw_velocity, VelocityLerp);
        _rigidBody2D.velocity = new_velocity;
    }
}
