using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardProjectileBehavior : ProjectileBase
{
    private void Start()
    {
        base.Init();
    }

    // Update is called once per frame
    void Update()
    {
        velocity = this.transform.up.normalized * Speed * Time.deltaTime;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (collision.tag.Contains("Enemy"))
        {
            collision.gameObject.SendMessage("TakeDamage", 1);
            print("Trigger Damage");
            Destroy(this.gameObject);
        }
    }
}
