using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehavior : MonoBehaviour {

    public KeyBehavior.Keys mCurrentKey;
	private SpriteRenderer sprite;
	private bool _RandomDoor = false;
	private float mStartTime;
	private float mDurration = .07f;

	public GameObject BurstManager;

	private int fakeindex = 0;

	// Use this for initialization
	void Start () {
		sprite = this.GetComponent<SpriteRenderer>();
        sprite.color = Color.grey;
		//UpdateDoorColor(mCurrentKey);
        Global.mGlobal.OnKeyPickup += KeyPickup;
	}

    private void OnDestroy()
    {
        Global.mGlobal.OnKeyPickup -= KeyPickup;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

	private void FixedUpdate()
	{
		if(_RandomDoor && (Time.timeSinceLevelLoad - mStartTime > mDurration))
		{
			if (fakeindex++ > 5)
				fakeindex = 0;
			UpdateDoorColor((KeyBehavior.Keys)fakeindex);
		}
		
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.tag.Contains("Player"))
		{
            if (c.gameObject.GetComponent<HeroBehavior>().HasKey(mCurrentKey))
            {
                OpenDoor();   
            }
		}
	}

    private void KeyPickup(KeyBehavior.Keys key)
    {
		if (key == mCurrentKey)
		{
            UpdateDoorColor(key);
			//OpenDoor();
		}
    }

	private Color GetColor(KeyBehavior.Keys key)
	{
		switch (key)
		{
			case KeyBehavior.Keys.Red:
				return KeyBehavior.RedColor;
			case KeyBehavior.Keys.Orange:
				return KeyBehavior.OrangeColor;
			case KeyBehavior.Keys.Yellow:
				return KeyBehavior.YellowColor;
			case KeyBehavior.Keys.Green:
				return KeyBehavior.GreenColor;
			case KeyBehavior.Keys.Blue:
				return KeyBehavior.BlueColor;
			case KeyBehavior.Keys.Purple:
				return KeyBehavior.PurpleColor;
		}
		return Color.white;
	}

	private void UpdateDoorColor(KeyBehavior.Keys key)
	{
		// RED | ORANGE | YELLOW | GREEN | BLUE | PURPLE
		sprite.color = GetColor(key);
		mStartTime = Time.timeSinceLevelLoad;
	}

	public void SetRandom()
	{
		_RandomDoor = true;
		var rand = UnityEngine.Random.Range(0, 6);
		mCurrentKey = (KeyBehavior.Keys) rand;
	}

    private void OpenDoor()
    {
		GameObject.Find("NotificationText").GetComponent<NotificationManager>().DoorOpenMessage();
		Destroy(this.gameObject);
		var go = Instantiate(BurstManager);
		go.GetComponent<BurstManager>().MakeBurst(4, GetColor(mCurrentKey), this.transform.position, this.transform.localScale.x, global::BurstManager.SpriteTextures.Square);
	}
}
