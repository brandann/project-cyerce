﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    protected Rigidbody2D _rigidBody2D;
    protected float Speed = 1000;
    protected Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {

    }

    protected void Init()
    {
        _rigidBody2D = this.GetComponent<Rigidbody2D>();
    }

    private void LateUpdate()
    {
        _rigidBody2D.velocity = velocity;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Player/player1":
                break;
            case "GoldCoin":
                break;
            default:
                Destroy(this.gameObject);
                break;
        }
    }

}
