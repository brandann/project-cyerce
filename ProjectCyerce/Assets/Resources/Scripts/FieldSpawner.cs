using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSpawner : MonoBehaviour
{
    public enum FieldTags { None, Start, ShopO, ShopD, Dun1, Dun2, Dun3}

    public AreaSpawner[] AreaSpawners;
    public FieldTags CurrentTag = FieldTags.None;

    bool initState = false;
    public bool GetInitState
    {
        get { return initState; }
        private set { initState = value; }
    }

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
        AreaSpawnerList.RemoveAt(rand);

        TargetArea.InitArea(t);

        foreach (var a in AreaSpawnerList)
            Destroy(a.gameObject);

        initState = true;
    }
}
