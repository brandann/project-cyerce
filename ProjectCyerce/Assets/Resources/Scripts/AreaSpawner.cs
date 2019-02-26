using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpawner : MonoBehaviour
{

    public GameObject StartAreaPrefab;
    public GameObject ShopAreaPrefab;
    public GameObject DungeonAreaPrefab;
    public GameObject ForestAreaPrefab;
    public GameObject Tree;

    bool initState = false;
    public bool GetInitState { 
        get{return initState;}
        private set{initState = value;}
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitArea(FieldSpawner.FieldTags Ftag)
    {
        initState = true;
        DEV_ColorAreaPerTag(Ftag);
    }

    private void DEV_ColorAreaPerTag(FieldSpawner.FieldTags Ftag)
    {
        switch(Ftag)
        {
            case FieldSpawner.FieldTags.None:
                //DEV_buildRandomTrees();
                //Destroy(gameObject);
                break;
            case FieldSpawner.FieldTags.Start:
                print("Start POS = " + this.transform.position);
                var _startArea = Instantiate(StartAreaPrefab, transform.position, transform.rotation);
                _startArea.transform.parent = this.transform;
                break;
            case FieldSpawner.FieldTags.ShopO:
                var _shopArea = Instantiate(ShopAreaPrefab, transform.position, transform.rotation);
                _shopArea.transform.parent = this.transform;
                break;
            case FieldSpawner.FieldTags.ShopD:
                this.GetComponent<SpriteRenderer>().color = Color.magenta;
                break;
            case FieldSpawner.FieldTags.Dun1:
                var _dungeonArea = Instantiate(DungeonAreaPrefab, transform.position, transform.rotation);
                _dungeonArea.transform.parent = this.transform;
                break;
            case FieldSpawner.FieldTags.Dun2:
                this.GetComponent<SpriteRenderer>().color = Color.black;
                break;
            case FieldSpawner.FieldTags.Dun3:
                this.GetComponent<SpriteRenderer>().color = new Color(.25f, .25f, .25f, 1);
                break;
            case FieldSpawner.FieldTags.Forest:
                var _forestArea = Instantiate(ForestAreaPrefab, transform.position, transform.rotation);
                _forestArea.transform.parent = this.transform;
                break;
            case FieldSpawner.FieldTags.Lake:
                // nothing
                break;
            default:
                //DEV_buildRandomTrees();
                //Destroy(this.gameObject);
                break;
        }
    }

    private void DEV_buildRandomTrees()
    {
        int rand = Random.Range(0, 10);
        for (int i = 0; i < rand; i++)
        {
            int rx = Random.Range(-14, 14);
            int ry = Random.Range(-14, 14);
            var t = Instantiate(Tree, new Vector3(rx + transform.position.x, ry + transform.position.y, 0), transform.rotation);
            t.transform.parent = this.transform;
        }
    }
}
