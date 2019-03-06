using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : EnemyBase
{
    private SpriteRenderer sprite;
    public GameObject ProjectilePrefab;

    private float TimeofLastShot;
    private float TimeBetweenShots = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        base.Init();

        InitBase(5, 0, 0);

        sprite = this.GetComponent<SpriteRenderer>();
        PlayerPatrolDist = 12;
        TimeofLastShot = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateChaseState();
        ChasePlayer();
    }

    private void LateUpdate()
    {
        ShootPlayer();
    }

    private void UpdateChaseState()
    {
        if(null!= Player1Transform)
        {
            var distPlayer1 = this.transform.position - Player1Transform.position;
            if (distPlayer1.magnitude <= PlayerPatrolDist)
            {
                CurrentState = State.CHASE;
                return;
            }
        }

        if(null != Player2Transform)
        {
            var distPlayer2 = this.transform.position - Player2Transform.position;
            if (distPlayer2.magnitude <= PlayerPatrolDist)
            {
                CurrentState = State.CHASE;
                return;
            }
        }

        CurrentState = State.PATROL;
    }

    private void ChasePlayer()
    {
        if (CurrentState == State.CHASE)
        {
            //SetLookAt(Player1Position);
            var pos = new Vector3(); 
            GetNearestPlayerPosition(out pos);
            SetLookAt(pos);
            sprite.color = Color.red;
        }
        else
        {
            sprite.color = Color.black;
        }
    }

    private void ShootPlayer()
    {
        if (CurrentState != State.CHASE)
            return;

        if ((Time.timeSinceLevelLoad - TimeofLastShot) < TimeBetweenShots)
            return;

        var SPP = Instantiate(ProjectilePrefab, this.transform.position + this.transform.up, this.transform.rotation);
        TimeofLastShot = Time.timeSinceLevelLoad;
    }

    protected override void UpdateStateOff()
    {
        throw new System.NotImplementedException();
    }

    protected override void UpdateStatePatrol()
    {
        throw new System.NotImplementedException();
    }

    protected override void UpdateStateChase()
    {
        throw new System.NotImplementedException();
    }
}
