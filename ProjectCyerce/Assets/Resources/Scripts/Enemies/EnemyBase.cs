using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    protected enum State { OFF, PATROL, CHASE} // STATE OF THE ENEMY
    protected State CurrentState; // CURRENT STATE OF THE ENEMY
    protected float Speed; // ENEMY SPEED
    protected int CurrentHeath; // ENEMY CURRENT HEALTH
    protected int MaxHeath; // ENEMY MAX HEALTH - USED FOR GAINING HEALTH (NOT TO GO OVER)
    protected bool isAlive; // TRUE: ENEMY IS ALIVE
    protected float PlayerPatrolDist; // TODO THIS NEEDS BETTER DEFINED
    protected int DamageToPlayerOnCollision; // DAMAGE DELT TO PLAYER WHEN ENEMY BUMBS HIM

    private Transform player1Transform;
    private Transform player2Transform;
    protected const string PLAYER1_TAG = "Player/player1";
    protected const string PLAYER2_TAG = "Player/player2";

    private float TimeofLastBump; // TRACK WHEN LAST BUMP HAPPENED
    private float TimeBetweenBumps = 1.0f; // HELPS FROM MANY CONTINUOUS BUMPS

    public GameObject GoldCoinPrefab; // GOLD COIN TO DROP WHEN ENEMY DIES

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
        CurrentState = State.OFF;
        TimeofLastBump = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        switch(CurrentState)
        {
            case State.OFF:
                UpdateStateOff();
                break;
            case State.PATROL:
                UpdateStatePatrol();
                break;
            case State.CHASE:
                UpdateStateChase();
                break;
        }
    }

    protected abstract void UpdateStateOff();
    protected abstract void UpdateStatePatrol();
    protected abstract void UpdateStateChase();

    public virtual void TakeDamage(int dmg)
    {
        CurrentHeath -= dmg;
        if (CurrentHeath <= 0)
            EnemyDeath();
        CurrentHeath = Mathf.Clamp(CurrentHeath, 0, MaxHeath);
        print("Enemy Took " + dmg + ", Health at " + CurrentHeath);
        if (State.OFF == CurrentState)
            CurrentState = State.CHASE;
    }

    protected virtual void EnemyDeath()
    {
        DropLoot();
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

    private readonly int[] Drop_Health_Range = { 0, 5 };
    private readonly int[] Drop_Gold_Range = { 6, 75 };

    protected void DropLoot()
    {
        var r = Random.Range(0, 100);
        if (r > Drop_Health_Range[0] && r < Drop_Health_Range[1])
            DropHealth();
        if (r > Drop_Gold_Range[0] && r < Drop_Gold_Range[1])
            DropGold();
    }

    protected void DropHealth()
    {
        print("Drop Health");
    }

    private readonly int[] GoldDrop = { 10, 9, 9, 8, 8, 8, 7, 7, 7, 7, 5, 5, 5, 5, 5, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

    protected void DropGold()
    {
        var r = Random.Range(0, GoldDrop.Length);
        var dropVal = GoldDrop[r];
        for(int i = 0; i < dropVal; i++)
        {
            var go = Instantiate(GoldCoinPrefab);
            var pos = this.transform.position + (Random.insideUnitSphere);
            go.transform.position = pos;
        }
        // CREATE GOLD FOR GOLDDROP[R];
        print("Drop Gold: " + dropVal);
    }
}
