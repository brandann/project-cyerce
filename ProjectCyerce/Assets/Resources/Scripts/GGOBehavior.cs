using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GGOBehavior : MonoBehaviour
{
    private GameObject WorldMap;
    private GameObject ShopMap;
    private GameObject Camera;
    private GameObject ScreenFadeImage;

    // Start is called before the first frame update
    void Start()
    {
        WorldMap = GameObject.Find("OverworldSpawner");
        ShopMap = GameObject.Find("ShopScreen");
        Camera = GameObject.Find("Main Camera");
        ScreenFadeImage = GameObject.Find("ScreenFadeImage");

        ShopMap.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region toshop

    private Vector3 Lastposition;

    public IEnumerator FadeScreen()
    {
        var fade = ScreenFadeImage.GetComponent<Image>();
        int count = 30;
        float duration = .33f;
        Color targetColor = new Color(1, 1, 1, 1);
        float lerp = 0.1f;
        while(count >= 0)
        {
            fade.color = Color.Lerp(fade.color, targetColor, lerp);
            count--;
            yield return new WaitForSeconds(duration);
        }
        yield return null;
    }

    public void TravelToShop1()
    {
        StartCoroutine(FadeScreen());
        WorldMap.SetActive(false);
        ShopMap.SetActive(true);

        var shopNode = GameObject.FindWithTag("Map/ShopOStartNode");
        var player = GameObject.FindWithTag("Player/player1");

        var playerRigidBody = player.GetComponent<Rigidbody2D>();
        playerRigidBody.velocity = Vector3.zero;

        Lastposition = player.transform.position + new Vector3(0,-0.5f,0);
        player.transform.position = shopNode.transform.position;
        Camera.transform.position = player.transform.position;
    }

    public void GoBackToWorld()
    {
        WorldMap.SetActive(true);
        ShopMap.SetActive(false);

        var player = GameObject.FindWithTag("Player/player1");

        var playerRigidBody = player.GetComponent<Rigidbody2D>();
        playerRigidBody.velocity = Vector3.zero;

        player.transform.position = Lastposition;
        Camera.transform.position = player.transform.position;
    }
    #endregion
}
