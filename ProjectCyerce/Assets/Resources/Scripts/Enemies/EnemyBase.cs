using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    protected enum State { OFF, PATROL, CHASE}
    protected State CurrentState;
    protected float speed;
    protected int CurrentHeath;
    protected int MaxHeath;
    protected bool isAlive;

    private Transform player1Transform;
    private Transform player2Transform;
    protected const string PLAYER1_TAG = "Player/player1";
    protected const string PLAYER2_TAG = "Player/player2";

    protected Vector3 Player1Position
    {
        get { 
            if(null == player1Transform)
                player1Transform = GameObject.FindWithTag(PLAYER1_TAG).GetComponent<Transform>();
            return player1Transform.position;
        }
    }

    protected Vector3 Player2Position
    {
        get
        {
            if (null == player2Transform)
                player2Transform = GameObject.FindWithTag(PLAYER2_TAG).GetComponent<Transform>();
            return player2Transform.position;
        }
    }

    // Start is called before the first frame update
    protected virtual void Init()
    {
        isAlive = true;
        CurrentState = State.PATROL;
        player1Transform = GameObject.FindWithTag(PLAYER1_TAG).GetComponent<Transform>();
        player2Transform = GameObject.FindWithTag(PLAYER1_TAG).GetComponent<Transform>();
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
    }

    public virtual void EnemyDeath()
    {
        isAlive = false;
        print("Enemy Death");
        Destroy(this);
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

    protected void LootAtNearestPlayer()
    {
        //todo broken
        var dist1 = Player1Position - this.transform.position;
        var dist2 = Player2Position - this.transform.position;
        Vector3 nearest = (dist1.magnitude < dist2.magnitude)? dist1:dist2;
        SetLookAt(nearest);
    }
}
