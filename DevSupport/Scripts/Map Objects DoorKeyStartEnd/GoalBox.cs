using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalBox : MonoBehaviour {

    private NotificationManager mNotificationManager;

	// Use this for initialization
	void Start () {
        mNotificationManager = GameObject.Find("NotificationText").GetComponent<NotificationManager>();
        Global.mGlobal.OnCollection += MGlobal_OnCollection;
        Global.mGlobal.OnLevelEnd += OnLevelEnd;
	}

	private void OnDestroy()
	{
		Global.mGlobal.OnCollection -= MGlobal_OnCollection;
        Global.mGlobal.OnLevelEnd -= OnLevelEnd;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag.Contains("Player"))
		{
            if(GameObject.Find("Global").GetComponent<GlobalGameObject>().CollectionAcheived)
            {
				Global.mGlobal.TriggerOnLevelEnd();
				mNotificationManager.LevelCompleteMessage();
				Destroy(this.gameObject);
				this.gameObject.SetActive(false);
            }
            else{
                mNotificationManager.MissingCollectionEnd();
            }
		}
	}

    private void MGlobal_OnCollection()
    {
    }

	private void OnLevelEnd()
	{
        
	}
}
