using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMapDoorBehavior : MonoBehaviour
{
    public FieldSpawner.FieldTags ShopID;

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
        if(collision.gameObject.tag.Contains("Player"))
        {
            print("PLAYER ENTER SHOP: " + ShopID.ToString());
        }
    }
}
