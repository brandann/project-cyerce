using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehavior : EnemyBase
{

    // Start is called before the first frame update
    void Start()
    {
        base.Init();

        SetMaxHealth(5);
        speed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SetLookAt(Player1Position);
        //LootAtNearestPlayer();
    }
}
