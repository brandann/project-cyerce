using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldSpawner : MonoBehaviour
{
    public FieldSpawner[] FieldSpanwers;
    List<FieldSpawner> FieldSpawnerList;

    public GameObject HeroPrefab; // TEMP

    public GameObject FieldPrefab;

    public GameObject WorldBoundryPrefab;
    private float WorldBoundrySize = 6;

    public int MapWidth;
    public int MapHeight;
    public int FieldSize;
    

    // Start is called before the first frame update
    void Start()
    {
        //InitWorld();
        InitDynamicWorld();
        InitFieldTags();

        //DEV create hero
        var SP = GameObject.FindWithTag("Map/StartNode").transform;
        print("World Start POS = " + SP.position);
        Instantiate(HeroPrefab, SP.position, SP.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitFieldTags()
    {
        InitFieldTag(FieldSpawner.FieldTags.Start); //1
        InitFieldTag(FieldSpawner.FieldTags.ShopO); //2
        InitFieldTag(FieldSpawner.FieldTags.Dun1);  //3
        InitFieldTag(FieldSpawner.FieldTags.Forest); //4
        //InitFieldTag(FieldSpawner.FieldTags.ShopD);
        //InitFieldTag(FieldSpawner.FieldTags.Dun2);
        //InitFieldTag(FieldSpawner.FieldTags.Dun3);
    }

    public void InitDynamicWorld()
    {
        FieldSpawnerList = new List<FieldSpawner>();
        int WorldWidth = MapWidth * FieldSize;
        int WorldHeight = MapHeight * FieldSize;

        for (int x = 0; x < MapWidth; x++)
        {
            for (int y = 0; y < MapHeight; y++)
            {
                var g = Instantiate(FieldPrefab, new Vector3(x * FieldSize, y * FieldSize, 0), transform.rotation);
                FieldSpawnerList.Add(g.GetComponent<FieldSpawner>());
            }
        }

        this.transform.position = new Vector3(((MapWidth-1) * FieldSize) / 2, ((MapHeight-1) * FieldSize) / 2, 0);
        //this.transform.localScale = new Vector3(WorldWidth, WorldHeight, 0);
        GetComponent<SpriteRenderer>().size = new Vector2(WorldWidth, WorldHeight);

        CreateBoundries(WorldWidth, WorldHeight);
    }

    private void CreateBoundries(int WorldWidth, int WorldHeight)
    {
        float BoundryOffset = (FieldSize / 2) + (WorldBoundrySize / 2);

        // LEFT
        CreateSingleBoundry(0 - BoundryOffset, WorldHeight / 2 - (FieldSize / 2), WorldBoundrySize, WorldHeight + WorldBoundrySize);

        // RIGHT
        CreateSingleBoundry(WorldWidth + BoundryOffset - FieldSize, WorldHeight / 2 - (FieldSize / 2), WorldBoundrySize, WorldHeight + WorldBoundrySize);

        // TOP
        CreateSingleBoundry(WorldWidth / 2 - (FieldSize / 2), WorldHeight + BoundryOffset - FieldSize, WorldWidth + WorldBoundrySize, WorldBoundrySize);

        // TOP
        CreateSingleBoundry(WorldWidth / 2 - (FieldSize / 2), 0 - BoundryOffset, WorldWidth + WorldBoundrySize, WorldBoundrySize);
    }

    private void CreateSingleBoundry(float x, float y, float width, float height)
    {
        var w = Instantiate(WorldBoundryPrefab, new Vector3(x, y, 0), transform.rotation);
        w.transform.localScale = new Vector3(width, height, 0);
    }

    private void InitFieldTag(FieldSpawner.FieldTags t)
    {
        int rand = Random.Range(0, FieldSpawnerList.Count);
        var F = FieldSpawnerList[rand];
        F.InitField(t);
        FieldSpawnerList.RemoveAt(rand);
    }
}
