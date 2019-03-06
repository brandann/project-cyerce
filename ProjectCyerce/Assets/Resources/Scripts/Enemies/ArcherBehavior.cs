using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherBehavior : EnemyBase
{
    protected Vector3 TargetPlayerPosition;
    protected Vector3 TargetPosition;
    private float PlayerPatrolDist;


    protected override void UpdateStateChase()
    {
        throw new System.NotImplementedException();
    }

    protected override void UpdateStateOff()
    {
        GetNearestPlayerPosition(out TargetPlayerPosition);
        var dist = this.transform.position - TargetPlayerPosition; // DIST BETWEEN PLAYER AND PLANET
        if (dist.magnitude <= PlayerPatrolDist)
        {
            print("Snake at STATE.CHASE");
            CurrentState = State.CHASE;
            return;
        }
    }

    protected override void UpdateStatePatrol()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
