
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShooterBehavior : MonoBehaviour
{
    public Transform PlayerPosition; // use for the position of the player
    public Transform PlayerAimTransform; // use for the rotation of the player
    public GameObject ProjectilePrefab;

    public float MAX_Mana;
    public float Mana_RegenRate;
    protected float CURRENT_Mana;

    public Slider Mana_UI;

    protected float GainManaSeconds = 1; // how ofter gain mana
    protected float GainManaAmmount = 5; // how much mana to gain

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
        Mana_UI = GameObject.FindWithTag("UI/ManaSlider").GetComponent<Slider>();
        CURRENT_Mana = MAX_Mana;
        Mana_UI.maxValue = MAX_Mana;
        StartCoroutine(GainMana());
    }

    // Update is called once per frame
    void Update()
    {
        HandleClickWatch();
        //CURRENT_Mana = Mathf.Clamp(CURRENT_Mana + (Mana_RegenRate * Time.deltaTime), 0, MAX_Mana);
    }

    private void LateUpdate()
    {
        Mana_UI.value = CURRENT_Mana;
    }

    private void HandleClickWatch()
    {
        if(Input.GetMouseButtonDown(0))
        {
            
            var SPP = Instantiate(ProjectilePrefab, this.transform.position, PlayerAimTransform.rotation);
            var PB = SPP.gameObject.GetComponent<ProjectileBase>().Init();
            //var PB.Init();
            var cost = PB.GetManaCost();
            var val = CURRENT_Mana - cost;
            if (val >= 0)
            {
                CURRENT_Mana = val;
            }
            else
            {
                Destroy(SPP.gameObject);
            }

        }
    }

    IEnumerator GainMana()
    {
        // WAIT FOR THE MOD DURATION TO FINISH
        var sprite = this.GetComponent<SpriteRenderer>();
        while (true)
        {
            yield return new WaitForSeconds(GainManaSeconds);
            CURRENT_Mana += GainManaAmmount;
        }
        //yield return null;
    }
}
