using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIManager : MonoBehaviour
{
    public string PlayerTag;

    public Slider HealthSlider;
    public Slider ManaSlider;

    private PlayerBehavior PlayerStats;

    // Start is called before the first frame update
    void Start()
    {
        var p_go = GameObject.FindWithTag(PlayerTag.ToString());
        if (null != p_go)
        {
            PlayerStats = p_go.GetComponent<PlayerBehavior>();

            // -------------------------------------
            HealthSlider.maxValue = PlayerStats.GetMaxHealth();
            HealthSlider.minValue = 0;

            // -------------------------------------
            ManaSlider.maxValue = PlayerStats.GetMaxMana();
            ManaSlider.minValue = 0;
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (null == PlayerStats)
            return;

        HealthSlider.value = PlayerStats.GetCurrentHealth();
        ManaSlider.value = PlayerStats.GetCurrentMana();
       
    }
}
