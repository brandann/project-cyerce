using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShopTest : MonoBehaviour
{

    

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
        OnButtonClick();
    }

    public void OnButtonClick()
    {


        MovePlayerToShop();
    }

    private void MovePlayerToShop()
    {

    }

    public void BackToWorld()
    {

    }
}
