
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NotificationManager : MonoBehaviour {

    private static string mCurrentText;
	private Queue<string> TextQueue;
    private Text mNotificationText;

    private float mStartTime;
    private float mDurration = 0;
	private const float SecPerChar = 0.042647f; // 2.9 / MAX LEN

	private const bool SHOW_DOOR = false;
	private const bool SHOW_KEY = false;


	// Use this for initialization
	void Start () {
        mNotificationText = GetComponent<Text>();
       mCurrentText = string.Empty; // RESET THE MESSAGE WHENEVER NOTIFICATION MANAGER IS NEW
        mStartTime = Time.timeSinceLevelLoad;
		TextQueue = new Queue<string>();
        SetNotification("Welcome Friends, Have a great game!!");
	}

    // Update is called once per frame
    void Update () {
        mNotificationText.text = mCurrentText;
        if (Time.timeSinceLevelLoad - mStartTime > mDurration)
		{
			mCurrentText = string.Empty;
			if(TextQueue.Count > 0)
			{
				mCurrentText = TextQueue.Dequeue();
				mDurration = CalcDisplayTime(mCurrentText);
				mStartTime = Time.timeSinceLevelLoad;
			}
		}
            
	}

    public void SetNotification(string msg)
    {
		if(mCurrentText == string.Empty)
		{
			if (mCurrentText.Equals(msg))
			{
				print("Message Already Queued, Skiped!");
				return;
			}
				
			mCurrentText = msg;
			mDurration = CalcDisplayTime(msg);
			mStartTime = Time.timeSinceLevelLoad;
		}
		else
		{
			if (TextQueue.Contains(msg))
			{
				print("Message Already Queued, Skiped!");
				return;
			}
			TextQueue.Enqueue(msg);
		}
    }

	private float CalcDisplayTime(string msg)
	{
		//return Mathf.Sqrt(msg.Length) / 4.5f;// <- trying for a long enough message
		return 2.9f;
	}
}
