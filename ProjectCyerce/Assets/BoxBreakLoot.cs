using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxBreakLoot : MonoBehaviour
{
    private readonly int[] Drop_Health_Range = { 0, 5 };
    private readonly int[] Drop_Gold_Range = { 6, 20 };

    public GameObject GoldCoinPrefab;

    protected void DropLoot()
    {
        var r = Random.Range(0, 100);
        if (r > Drop_Health_Range[0] && r < Drop_Health_Range[1])
            DropHealth();
        if (r > Drop_Gold_Range[0] && r < Drop_Gold_Range[1])
            DropGold();
        Destroy(this.gameObject);
    }

    protected void DropHealth()
    {
        print("Drop Health");
    }

    private readonly int[] GoldDrop = { 10, 9, 9, 8, 8, 8, 7, 7, 7, 7, 5, 5, 5, 5, 5, 4, 4, 4, 4, 4, 4, 3, 3, 3, 3, 3, 3, 3, 2, 2, 2, 2, 2, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1 };

    protected void DropGold()
    {
        var r = Random.Range(0, GoldDrop.Length);
        var dropVal = GoldDrop[r];
        for (int i = 0; i < dropVal; i++)
        {
            var go = Instantiate(GoldCoinPrefab);
            var pos = this.transform.position + (Random.insideUnitSphere);
            go.transform.position = pos;
        }
        // CREATE GOLD FOR GOLDDROP[R];
        print("Drop Gold: " + dropVal);
    }

    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    if (collision.gameObject.tag.Contains("Player"))
    //        DropLoot();
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
            DropLoot();
        else if (collision.gameObject.tag.Contains("Projectile/"))
            DropLoot();
    }
}
