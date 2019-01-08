using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMouseLook();
    }

    private void HandleMouseLook()
    {
        var pos = Input.mousePosition;

        var mouse_pos = Camera.main.ScreenToWorldPoint(pos);
        mouse_pos.z = 0;

        SetLookPosition(mouse_pos);
    }

    private void SetLookPosition(Vector3 pos)
    {
        var diff = pos - this.transform.position;
        this.transform.up = diff.normalized;
    }
}
