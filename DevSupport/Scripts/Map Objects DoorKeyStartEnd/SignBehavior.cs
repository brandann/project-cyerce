using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignBehavior : MonoBehaviour {

    public string mSignMessage;
	public NotificationManager _notificationManager;

	// Use this for initialization
	void Start () {
		_notificationManager = GameObject.Find("NotificationText").GetComponent<NotificationManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag.Contains("Player"))
		{
			_notificationManager.SignEnterMessage("Tap Sign To Read Message");
			//_notificationManager.SignEnterMessage(mSignMessage);
		}
	}

	private void OnMouseDown()
	{
		GameObject.Find("GamePanel").GetComponent<GamePanelScript>().OnDialog(mSignMessage);
		//_notificationManager.SignEnterMessage(mSignMessage);
	}
}
