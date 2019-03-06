using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileBase : MonoBehaviour
{
    /**
     * HOW TO USE THE PROJECTILE CLASSES ----------------------
     * 
     *      SETUP:    
     *          A) A PROJECTILE OBJECT SHOULD INHERIT FROM 'ProjectileBase'
     *          B) OVERRIDE THE 'Init()' FUNCTION AND CALLS THE FOLLOWING LINES:
     *                      base.Init();
     *                      SetManaCost(3);
     *                      return this;    
     *          C) SET VELOCITY IN THE UPDATE FUNCTION
     * 
     *      HOW TO USE THE PROJECTILES:
     *          A) INSTANTIATE THE OBJECT
     *          B) CALL 'object<ProjectileBase>().Init()'    
     *          C) GET THE MANA COST: cost = PB.GetManaCost();
     *          TODO CHNAGE THE USE TO A METHOD THAT ASKS FOR POS, ROT, VEL, TARGET...)   
     * 
     * 
     * THE PROJECTILEBASE SHOULD HAVE ALL COMMON METHODS INCLUDED AND OPTIONS, COLLIDER CONDIDIONS
     * 
     * USE TRACKING:
     *      CALL 'TrackEnemy()' FROM AN UPDATE FUNCTION
     *      SET mRotationSpeed FROM THE Init() TO CHANGE FROM THE DEFAULT OF '50'
     *          
     */

    protected Rigidbody2D _rigidBody2D;
    protected float Speed = 1000;
    protected Vector3 velocity;

    protected float ManaCost;

    protected float mRotationSpeed = 50;

    // Start is called before the first frame update
    void Start()
    {

    }

    public virtual ProjectileBase Init()
    {
        _rigidBody2D = this.GetComponent<Rigidbody2D>();
        return this;
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
                break; // DO NOTHING
            case "GoldCoin":
                break; // DO NOTHING
            default:
                Destroy(this.gameObject);
                break;
        }
    }

    public float GetManaCost()
    {
        return ManaCost;
    }

    public void SetManaCost(float m)
    {
        ManaCost = m;
    }

    protected void TrackEnemy()
    {
        Vector3 Position = Vector3.zero;
        if (GetNearestEnemyPosition(out Position))
        {
            var LR = NMath.GetLeftRight(this.transform, Position);
            var dir = (LR == NMath.LeftRight.Left) ? -1 : 1;
            transform.Rotate(Vector3.forward, dir * -1 * (mRotationSpeed * Time.smoothDeltaTime));
        }
    }

   protected bool GetNearestEnemyPosition(out Vector3 pos)
    {
        LayerMask mask = LayerMask.GetMask("Enemy/");
        Collider2D[] colliders = Physics2D.OverlapCircleAll(this.transform.position, 6f);
        if (colliders.Length > 0)
        {
            List<Collider2D> EnemyColliders = new List<Collider2D>();
            foreach (var c in colliders)
            {
                if (c.tag.Contains("Enemy/"))
                    EnemyColliders.Add(c);
            }

            if (EnemyColliders.Count == 0)
            {
                pos = Vector3.zero;
                return false;
            }

            Collider2D[] enemyColliders2D = EnemyColliders.ToArray();

            var nearest = enemyColliders2D[0];
            for (int i = 0; i < enemyColliders2D.Length; i++)
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
