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
            print("Trigger Damage");
            Destroy(this.gameObject);
        }
    }

    void TrackEnemy()
    {
        Vector3 Position = Vector3.zero;
        if(GetNearestEnemyPosition(out Position))
        {
            var dist = this.transform.position - Position;
            var small_norm = dist.normalized * 2;
            _rigidBody2D.velocity -= new Vector2(small_norm.x, small_norm.y);
        }
    }

    bool GetNearestEnemyPosition(out Vector3 pos)
    {
        LayerMask mask = LayerMask.GetMask("Enemy/");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, 6f);
        if (colliders.Length > 0)
        {
            List<Collider2D> EnemyColliders = new List<Collider2D>();
            foreach(var c in colliders)
            {
                if (c.tag.Contains("Enemy/"))
                    EnemyColliders.Add(c);
            }

            if(EnemyColliders.Count == 0)
            {
                pos = Vector3.zero;
                return false;
            }

            Collider2D[] enemyColliders2D = EnemyColliders.ToArray();

            var nearest = enemyColliders2D[0];
            for(int i = 0; i < enemyColliders2D.Length; i++)
            {
                var dist_current = this.transform.position - nearest.transform.position;
                var dist_temp = this.transform.position - enemyColliders2D[i].transform.position;
                if (dist_temp.magnitude > dist_current.magnitude)
                    nearest = enemyColliders2D[i];
            }
            pos = nearest.gameObject.transform.position;
            return true;
        }
        pos = Vector3.zero;
        return false;
    }
}
