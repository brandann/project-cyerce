using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldSpawner : MonoBehaviour
{
    public enum FieldTags { None, Start, ShopO, ShopD, Dun1, Dun2, Dun3, Forest, Lake}

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
        switch (t)
        {
            case FieldSpawner.FieldTags.None:
                BasicInit(t);
                break;
            case FieldSpawner.FieldTags.Start:
                BasicInit(t);
                break;
            case FieldSpawner.FieldTags.ShopO:
                this.GetComponent<SpriteRenderer>().color = Color.blue;
                BasicInit(t);
                break;
            case FieldSpawner.FieldTags.ShopD:
                this.GetComponent<SpriteRenderer>().color = Color.magenta;
                BasicInit(t);
                break;
            case FieldSpawner.FieldTags.Dun1:
                this.GetComponent<SpriteRenderer>().color = Color.grey;
                BasicInit(t);
                break;
            case FieldSpawner.FieldTags.Dun2:
                this.GetComponent<SpriteRenderer>().color = Color.black;
                BasicInit(t);
                break;
            case FieldSpawner.FieldTags.Dun3:
                this.GetComponent<SpriteRenderer>().color = new Color(.25f, .25f, .25f, 1);
                BasicInit(t);
                break;
            case FieldSpawner.FieldTags.Forest:
                ForrestInit(t);
                break;
            case FieldSpawner.FieldTags.Lake:
                BasicInit(t);
                break;
            default:
                BasicInit(t);
                break;

        }
    }

    protected void BasicInit(FieldTags t)
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

    protected void ForrestInit(FieldTags t)
    {
        for (int i = 0; i < AreaSpawners.Length; i++)
        {
            AreaSpawners[i].InitArea(t);
        }
    }
}
