
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class NotificationManager : MonoBehaviour {

    private static string mCurrentText;
	private Queue<string> TextQueue;
    private Text mNotificationText;

    private float mStartTime;
    private float mDurration = 0;
    private const float SecPerChar = 0.07f;//0.042647f; // 2.9 / MAX LEN

	private const bool SHOW_DOOR = false;
	private const bool SHOW_KEY = false;

    // displays when: green triangle hits the player
    private string[] mKillByPlanet =
    {
        "out of my way, small fry!",
        "bet you didn't read the instructions.",
        "pick on somebody your own size.",
        "the faint sounds of screaming were heard as your planet was crushed.",
        "did you forget to eat your spinach?",
        "maybe a smaller bite next time?"
    };

    // displays when a level is beat
    private string[] mLevelComplete =
    {
        "i applaud your ability to level up!",
        "hooray! you successfully navigated to the next level!",
        "you skills show promise, care to demonstrate again?",
        "the squeels of tiny cheers can be heard, go on the the next round.",
        "you continue to impress your people, do not disappoint them."
    };

    // displays when a door key is picked up
    private string[] mKeyPickup =
    {
        "Key Acquired",
        "oh, you got a key. good for you."
    };

    // displays when a door is unlocked
    private string[] mDoorOpen =
    {
        "Door Open",
        "As you step through this door, you ask yourself 'why'"
    };

    // display when a patrol spots you and starts to chase
    private string[] mAngryPatrol =
    {
        "I'm angry now"
    };

    // displays when a partol catches you
    private string[] mAngryPatrolCaught =
    {
        "got you now!"
    };

    // displays when you pick up a collection item
    private string[] mCollection =
    {
        "Collection Item Got"
    };

    // display when you reach the end without all the collection items
    private string[] mMissingCollection =
    {
        "I think you are missing something..."
    };

	// Use this for initialization
	void Start () {
        mNotificationText = GameObject.Find("NotificationText").GetComponent<Text>();
        mCurrentText = string.Empty; // RESET THE MESSAGE WHENEVER NOTIFICATION MANAGER IS NEW
        mStartTime = Time.timeSinceLevelLoad;
		TextQueue = new Queue<string>();
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

    public void MissingCollectionEnd()
    {
		string msg = GetNotification(mMissingCollection);
		SetNotification(msg);
    }

	public void CollectionPickupMessage()
	{
		string msg = GetNotification(mCollection);
		SetNotification(msg);
	}

	public void AngryPatrolMessage()
	{
		string msg = GetNotification(mAngryPatrol);
		SetNotification(msg);
	}

	public void PatrolCaughtPlayerMessage()
	{
		string msg = GetNotification(mAngryPatrolCaught);
		SetNotification(msg);
	}

    public void KillByPlanetMessage()
    {
		string msg = GetNotification(mKillByPlanet);
		SetNotification(msg);
    }

    public void LevelCompleteMessage()
    {
		string msg = GetNotification(mLevelComplete);
		SetNotification(msg);
    }

    public void DoorOpenMessage()
    {
		if(SHOW_DOOR)
		{
			string msg = GetNotification(mDoorOpen);
			SetNotification(msg);
		}
    }

	public void KeyPickupMessage()
	{
		if(SHOW_KEY)
		{
			string msg = GetNotification(mKeyPickup);
			SetNotification(msg);
		}
	}

    public void SignEnterMessage(string s)
    {
        SetNotification(s);
    }

    private string GetNotification(string[] collection)
    {
		return collection[Random.Range(0, collection.Length - 1)];
    }

    private void SetNotification(string msg)
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
