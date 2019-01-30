using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    protected float Speed;
    protected int CurrentHeath;
    protected int MaxHeath;

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
}
