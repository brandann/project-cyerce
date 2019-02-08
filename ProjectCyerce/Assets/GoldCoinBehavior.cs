using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldCoinBehavior : MonoBehaviour
{
    public int GoldValue = 1;

    public void SetValue(int val)
    {
        GoldValue = val;
    }

    private int GetValue()
    {
        return GoldValue;
    }
}
