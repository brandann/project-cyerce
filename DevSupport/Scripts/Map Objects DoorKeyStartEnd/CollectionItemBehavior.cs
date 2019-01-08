using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionItemBehavior : MonoBehaviour {

    private GlobalGameObject mGlobal;
	public GameObject BurstManager;
	private GameObject mHeroObject;
	public GameObject mHeartEmojiObject;

	// Use this for initialization
	void Start () {
        mGlobal = GameObject.Find("Global").GetComponent<GlobalGameObject>();
	}

	void BurstMe()
	{
		var go = Instantiate(BurstManager);
        go.GetComponent<BurstManager>().MakeBurst(7, Color.blue, this.transform.position, 1, global::BurstManager.SpriteTextures.Heart);
		Destroy(this.gameObject);
	}

	// Update is called once per frame
	void Update () {
		if (null == mHeroObject)
		{
			mHeroObject = GameObject.FindGameObjectWithTag("Player");
			mHeartEmojiObject.SetActive(false);
		}

		var dist = this.transform.position - mHeroObject.transform.position;
		if(dist.magnitude <= 4)
			mHeartEmojiObject.SetActive(true);
		else
			mHeartEmojiObject.SetActive(false);

	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag.Contains("Player"))
		{
            if(null != mGlobal)
            {
                mGlobal.IncrementCollectionItems(1);
				GameObject.Find("NotificationText").GetComponent<NotificationManager>().CollectionPickupMessage();
                GameObject.Find("CollectionImage").GetComponent<CollectionUIBehavior>().IncrementCollectionItems(1);
				BurstMe();
            }
			
		}
	}
}
