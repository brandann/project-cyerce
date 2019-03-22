using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveShopTest : MonoBehaviour
{
    public OpenShopTest ShopTest;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            var ggo = GameObject.Find("GlobalGameObject").GetComponent<GGOBehavior>();
            ggo.GoBackToWorld();
        }

    }
}
