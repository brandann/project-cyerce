using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGOBehavior : MonoBehaviour
{
    private GameObject WorldMap;
    private GameObject ShopMap;
    private GameObject Camera;

    // Start is called before the first frame update
    void Start()
    {
        WorldMap = GameObject.Find("OverworldSpawner");
        ShopMap = GameObject.Find("ShopScreen");
        Camera = GameObject.Find("Main Camera");

        ShopMap.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region toshop

    private Vector3 Lastposition;

    public void TravelToShop1()
    {
        WorldMap.SetActive(false);
        ShopMap.SetActive(true);

        var g = GameObject.FindWithTag("Map/ShopOStartNode");
        var p = GameObject.FindWithTag("Player/player1");

        Lastposition = p.transform.position + new Vector3(0,-0.5f,0);
        p.transform.position = g.transform.position;
        Camera.transform.position = p.transform.position;
    }

    public void GoBackToWorld()
    {
        WorldMap.SetActive(true);
        ShopMap.SetActive(false);

        var p = GameObject.FindWithTag("Player/player1");

        p.transform.position = Lastposition;
        Camera.transform.position = p.transform.position;
    }
    #endregion
}
