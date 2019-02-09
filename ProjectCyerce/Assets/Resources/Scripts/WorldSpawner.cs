using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpawner : MonoBehaviour
{
    public FieldSpawner[] FieldSpanwers;
    List<FieldSpawner> FieldSpawnerList;

    public GameObject HeroPrefab; // TEMP

    // Start is called before the first frame update
    void Start()
    {
        InitWorld();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitWorld()
    {
        FieldSpawnerList = new List<FieldSpawner>();
        foreach (var f in FieldSpanwers)
            FieldSpawnerList.Add(f);

        InitFieldTag(FieldSpawner.FieldTags.Start);
        InitFieldTag(FieldSpawner.FieldTags.ShopO);
        InitFieldTag(FieldSpawner.FieldTags.ShopD);
        InitFieldTag(FieldSpawner.FieldTags.Dun1);
        InitFieldTag(FieldSpawner.FieldTags.Dun2);
        InitFieldTag(FieldSpawner.FieldTags.Dun3);

        var SP = GameObject.FindWithTag("Map/StartNode").transform.position;
        Instantiate(HeroPrefab, SP, transform.rotation);
    }

    private void InitFieldTag(FieldSpawner.FieldTags t)
    {
        int rand = Random.Range(0, FieldSpawnerList.Count);
        var F = FieldSpawnerList[rand];
        F.InitField(t);
        FieldSpawnerList.RemoveAt(rand);
    }
}
