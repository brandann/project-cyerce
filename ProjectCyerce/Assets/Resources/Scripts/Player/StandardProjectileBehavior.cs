using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardProjectileBehavior : ProjectileBase
{

    private void Start()
    {
        //Init();
    }

    public override ProjectileBase Init()
    {
        base.Init();
        SetManaCost(3);
        return this;
    }

    // Update is called once per frame
    void Update()
    {
        velocity = this.transform.up.normalized * Speed * Time.deltaTime;
    }

    private void FixedUpdate()
    {
        TrackEnemy();
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.tag.Contains("Enemy"))
        {
            collision.gameObject.SendMessage("TakeDamage", 1);
            Destroy(this.gameObject);
        }
    }
}
