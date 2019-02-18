using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoldUIBehavior : MonoBehaviour
{
    private PlayerBehavior PlayerStats1;
    private PlayerBehavior PlayerStats2;

    protected const string PLAYER1_TAG = "Player/player1";
    protected const string PLAYER2_TAG = "Player/player2";

    public TMPro.TextMeshProUGUI GoldUI;
    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("FindPlayers");
    }

    // Update is called once per frame
    void Update()
    {
        if(null != PlayerStats1)
            GoldUI.SetText("" + CalcGold());
    }

    IEnumerator FindPlayers()
    {
        while (null == PlayerStats1)
        {
            yield return new WaitForSeconds(.2f);
            var p_go = GameObject.FindWithTag(PLAYER1_TAG);
            if (null != p_go)
                PlayerStats1 = p_go.GetComponent<PlayerBehavior>();
        }

        yield return null;
    }

    public int CalcGold()
    {
        if (null != PlayerStats1)
            return PlayerStats1.GetGoldValue();
        return -1;
    }
}
