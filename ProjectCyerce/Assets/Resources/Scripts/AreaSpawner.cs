using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpawner : MonoBehaviour
{

    public GameObject StartAreaPrefab;

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
                break;
            case FieldSpawner.FieldTags.Start:
                this.GetComponent<SpriteRenderer>().color = Color.green;
                Instantiate(StartAreaPrefab, transform.position, transform.rotation);
                break;
            case FieldSpawner.FieldTags.ShopO:
                this.GetComponent<SpriteRenderer>().color = Color.blue;
                break;
            case FieldSpawner.FieldTags.ShopD:
                this.GetComponent<SpriteRenderer>().color = Color.magenta;
                break;
            case FieldSpawner.FieldTags.Dun1:
                this.GetComponent<SpriteRenderer>().color = Color.grey;
                break;
            case FieldSpawner.FieldTags.Dun2:
                this.GetComponent<SpriteRenderer>().color = Color.black;
                break;
            case FieldSpawner.FieldTags.Dun3:
                this.GetComponent<SpriteRenderer>().color = new Color(.25f, .25f, .25f, 1);
                break;

        }
    }
}
