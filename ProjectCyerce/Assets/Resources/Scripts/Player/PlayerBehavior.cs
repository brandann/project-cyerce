using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    // -----------------------------------------
    protected float Speed;

    // -----------------------------------------
    #region HEALTH
    protected int CurrentHeath;
    protected int MaxHeath = 6;

    public int GetMaxHealth() { return MaxHeath; }
    public int GetCurrentHealth() { return CurrentHeath; }
    #endregion

    // -----------------------------------------
    #region GOLD
    protected int GoldValue = 10;

    public int GetGoldValue() { return GoldValue; }
    #endregion

    // -----------------------------------------
    #region MANA
    protected int MaxMana = 100;
    protected int CurrentMana;

    public int GetMaxMana() { return MaxMana; }
    public int GetCurrentMana() { return CurrentMana; }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        CurrentHeath = MaxHeath;
        CurrentMana = MaxMana;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int dmg)
    {
		// DEAL DAMAGE TO THE PLAYER
        CurrentHeath -= dmg;
        CurrentHeath = Mathf.Clamp(CurrentHeath, 0, MaxHeath);
        print("Player Took " + dmg + ", Health at " + CurrentHeath);

		// SHAKE THE CAMERA
		var go = GameObject.FindWithTag("MainCamera");
		if (null != go)
			go.GetComponent<FollowPlayer1>().Shake(.02f, 2f);

		// KILL THE PLAYER
		if (CurrentHeath <= 0)
			Destroy(this.gameObject); // TODO: DON'T DESTROY, DO SOMETHING BETTER
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
