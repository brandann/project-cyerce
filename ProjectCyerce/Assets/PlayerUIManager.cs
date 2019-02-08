using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public string PlayerID;
    public Image[] HeartContainers;

    private PlayerBehavior PlayerStats;

    protected const string PLAYER1_TAG = "Player/player1";
    protected const string PLAYER2_TAG = "Player/player2";

    // Start is called before the first frame update
    void Start()
    {
        var p_go = GameObject.FindWithTag("Player/" + PlayerID);
        if (null != p_go)
            PlayerStats = p_go.GetComponent<PlayerBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if (null == PlayerStats)
            return;
    }
}
