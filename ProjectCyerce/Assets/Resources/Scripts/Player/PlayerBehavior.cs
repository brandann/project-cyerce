using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    protected float Speed;
    protected int CurrentHeath;
    protected int MaxHeath;

    protected int GoldValue = 10;

    // Start is called before the first frame update
    void Start()
    {
        MaxHeath = CurrentHeath = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int dmg)
    {
        CurrentHeath -= dmg;
        if (CurrentHeath <= 0)
            Destroy(this.gameObject);
        CurrentHeath = Mathf.Clamp(CurrentHeath, 0, MaxHeath);
        print("Player Took " + dmg + ", Health at " + CurrentHeath);

        var go = GameObject.FindWithTag("MainCamera");
        if (null != go)
            go.GetComponent<Follow2Players>().Shake(.02f, 1f);
    }

    public int GetGoldValue()
    {
        return GoldValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag.Contains("GoldCoin"))
        {
            var go = collision.gameObject.GetComponent<GoldCoinBehavior>();
            if (null != go)
            {
                GoldValue += go.GoldValue;
                Destroy(collision.gameObject);
            }

        }
    }
}
