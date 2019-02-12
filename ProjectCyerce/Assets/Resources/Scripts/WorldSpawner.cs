using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpawner : MonoBehaviour
{
    public FieldSpawner[] FieldSpanwers;
    List<FieldSpawner> FieldSpawnerList;

    public GameObject HeroPrefab; // TEMP

    public GameObject FieldPrefab;

    public int MapWidth;
    public int MapHeight;
    public int FieldSize;
    

    // Start is called before the first frame update
    void Start()
    {
        //InitWorld();
        InitDynamicWorld();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitDynamicWorld()
    {
        FieldSpawnerList = new List<FieldSpawner>();
        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                var g = Instantiate(FieldPrefab, new Vector3(x * FieldSize, y * FieldSize, 0), transform.rotation);
                FieldSpawnerList.Add(g.GetComponent<FieldSpawner>());
            }
        }

        InitFieldTag(FieldSpawner.FieldTags.Start);
        InitFieldTag(FieldSpawner.FieldTags.ShopO);
        InitFieldTag(FieldSpawner.FieldTags.ShopD);
        InitFieldTag(FieldSpawner.FieldTags.Dun1);
        InitFieldTag(FieldSpawner.FieldTags.Dun2);
        InitFieldTag(FieldSpawner.FieldTags.Dun3);

        var SP = GameObject.FindWithTag("Map/StartNode").transform;
        print("World Start POS = " + SP.position);
        Instantiate(HeroPrefab, SP.position, SP.rotation);
        this.transform.position = new Vector3(((MapWidth-1) * FieldSize) / 2, ((MapHeight-1) * FieldSize) / 2, 0);
        //this.transform.localScale = new Vector3(MapWidth * FieldSize, MapHeight * FieldSize, 0);
        GetComponent<SpriteRenderer>().size = new Vector2(MapWidth * FieldSize, MapHeight * FieldSize) * .1f;
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

        var SP = GameObject.FindWithTag("Map/StartNode").transform;
        print("World Start POS = " + SP.position);
        Instantiate(HeroPrefab, SP.position, SP.rotation);
    }

    private void InitFieldTag(FieldSpawner.FieldTags t)
    {
        int rand = Random.Range(0, FieldSpawnerList.Count);
        var F = FieldSpawnerList[rand];
        F.InitField(t);
        FieldSpawnerList.RemoveAt(rand);
    }
}
