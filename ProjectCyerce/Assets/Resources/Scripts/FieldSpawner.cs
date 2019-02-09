using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSpawner : MonoBehaviour
{
    public enum FieldTags { None, Start, ShopO, ShopD, Dun1, Dun2, Dun3}

    public AreaSpawner[] AreaSpawners;
    public FieldTags CurrentTag = FieldTags.None;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitField(FieldTags t)
    {
        CurrentTag = t;

        List<AreaSpawner> AreaSpawnerList = new List<AreaSpawner>();
        foreach (var f in AreaSpawners)
            AreaSpawnerList.Add(f);

        int rand = Random.Range(0, AreaSpawnerList.Count);
        var TargetArea = AreaSpawnerList[rand];

        TargetArea.InitArea(t);
    }
}
