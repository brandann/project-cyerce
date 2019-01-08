
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooterBehavior : MonoBehaviour
{
    public Transform PlayerPosition; // use for the position of the player
    public Transform PlayerAimTransform; // use for the rotation of the player

    private Vector3 GetPlayerPosition()
    {
        return PlayerPosition.position;
    }

    private Vector3 GetPlayerRotation()
    {
        return PlayerAimTransform.forward;
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
