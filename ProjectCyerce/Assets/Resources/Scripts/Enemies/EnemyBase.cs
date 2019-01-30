﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected enum State { OFF, PATROL, CHASE}
    protected State CurrentState;
    protected float Speed;
    protected int CurrentHeath;
    protected int MaxHeath;
    protected bool isAlive;
    protected float PlayerPatrolDist;
    protected int DamageToPlayerOnCollision;

    private Transform player1Transform;
    private Transform player2Transform;
    protected const string PLAYER1_TAG = "Player/player1";
    protected const string PLAYER2_TAG = "Player/player2";

    private float TimeofLastBump;
    private float TimeBetweenBumps = 1.0f;

    protected Transform Player1Transform
    {
        get {
            if (null == player1Transform)
            {
                var go = GameObject.FindWithTag(PLAYER1_TAG);
                if (null != go)
                    player1Transform = go.transform;
            }
            return player1Transform;
        }
    }

    protected Transform Player2Transform
    {
        get
        {
            if (null == player2Transform)
            {
                var go = GameObject.FindWithTag(PLAYER2_TAG);
                if (null != go)
                    player2Transform = go.transform;
            }
            return player2Transform;
        }
    }

    // Start is called before the first frame update
    protected virtual void Init()
    {
        isAlive = true;
        CurrentState = State.PATROL;
        TimeofLastBump = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void TakeDamage(int dmg)
    {
        CurrentHeath -= dmg;
        if (CurrentHeath <= 0)
            EnemyDeath();
        CurrentHeath = Mathf.Clamp(CurrentHeath, 0, MaxHeath);
        print("Enemy Took " + dmg + ", Health at " + CurrentHeath);
    }

    protected virtual void EnemyDeath()
    {
        isAlive = false;
        print("Enemy Death");
        Destroy(this.gameObject);
    }

    protected void SetMaxHealth(int i)
    {
        MaxHeath = CurrentHeath = i;
    }

    protected void SetLookAt(Vector3 pos)
    {
        var diff = pos - this.transform.position;
        this.transform.up = diff.normalized;
    }

    protected Vector3 GetNearestPlayerPosition()
    {
        if (null != Player1Transform && null != Player2Transform)
        {
            var dist1 = Player1Transform.position - this.transform.position;
            var dist2 = Player2Transform.position - this.transform.position;
            if (dist1.magnitude < dist2.magnitude)
                return (Player1Transform.position);
            return(Player2Transform.position);
        }
        if (null != Player1Transform && null == Player2Transform)
            return (Player1Transform.position);
        if (null == Player1Transform && null != Player2Transform)
            return(Player2Transform.position);

        return new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
    }

    protected void SetDamageOnCollisionWithPlayer(int dmg)
    {
        DamageToPlayerOnCollision = dmg;
    }

    protected void BumpPlayer(GameObject col)
    {
        if ((Time.timeSinceLevelLoad - TimeofLastBump) < TimeBetweenBumps)
            return;

        if (!col.tag.Contains("Player"))
            return;

        col.SendMessage("TakeDamage", DamageToPlayerOnCollision);
        TimeofLastBump = Time.timeSinceLevelLoad;
    }
}
