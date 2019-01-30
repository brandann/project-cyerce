using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    protected Rigidbody2D _rigidBody2D;
    protected float Speed = 500;
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
        if (collision.gameObject.tag.Contains("Wall"))
            Destroy(this.gameObject);
    }
}
