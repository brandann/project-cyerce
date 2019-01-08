using UnityEngine;
using System.Collections;

public class HeroBehavior : MonoBehaviour
{
	// RED | ORANGE | YELLOW | GREEN | BLUE | PURPLE
	private int mRedKey = 0;
    private int mOrangeKey = 0;
    private int mYellowKey = 0;
    private int mGreenKey = 0;
    private int mBlueKey = 0;
    private int mPurpleKey = 0;

	private bool isWinState = false;

    private NotificationManager mNotificationManager;
    public GameObject FollowPrefab;
	public GameObject BurstManager;

	private HeroMovement2D heroMovement2D;

    void Start()
    {
        Global.mGlobal.OnLevelEnd += MGlobal_OnLevelEnd;
        Global.mGlobal.OnKeyPickup += PickupKey;

        mNotificationManager = GameObject.Find("NotificationText").GetComponent<NotificationManager>();
		heroMovement2D = this.gameObject.GetComponent<HeroMovement2D>();

        isWinState = true;
	}

    private void OnDestroy()
    {
        Global.mGlobal.OnLevelEnd -= MGlobal_OnLevelEnd;
        Global.mGlobal.OnKeyPickup -= PickupKey;
    }

    private void FixedUpdate()
    {
		
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
    private void kill(string tag)
    {
        if (isWinState)
        {
            print("PLAYER IMMUNE TO KILL IN WINSTATE");
            return;
        }

		Handheld.Vibrate();

		if (PlayerHP)
		{
			print("Player has HP");
			PlayerHP = false;
			Destroy(PlayerHPFollowObject);
			isWinState = true;
			StartCoroutine("TurnOffInvincibility");
         
            GameObject.Find("Main Camera").GetComponent<CameraShake>().Shake();

			return;
		}

        if(tag == "Planet")
        {
            print("DEATH BY PLANET");
            //var globalgameobject = GameObject.Find("Global").GetComponent<GlobalGameObject>();
            //var new_spirit = Instantiate(globalgameobject.mPlanets, this.transform.position, this.transform.rotation);

            //var stay = GameObject.Find("Main Camera").GetComponent<StayOnObject>();
            //stay.mTargetObject = new_spirit;
            //stay.enabled = true;
            ////cam.transform.parent = new_spirit.transform;
        }

		var go = Instantiate(BurstManager);
		go.GetComponent<BurstManager>().MakeBurst(20, new Color(204 / 255f, 208 / 255f, 142 / 255f, 255 / 255f), this.transform.position, this.transform.localScale.x, global::BurstManager.SpriteTextures.Diamond);

		TogglePlayerOnOff(false); // turn player off

		Global.mGlobal.TriggerOnDeath();

        Global.mGlobal.DeathCount++;

		mNotificationManager.KillByPlanetMessage();
    }

	public void TogglePlayerOnOff(bool on)
	{
		this.gameObject.GetComponent<SpriteRenderer>().enabled = on;
		this.gameObject.GetComponent<CircleCollider2D>().enabled = on;
		this.gameObject.GetComponent<HeroBehavior>().enabled = on;
		heroMovement2D.TogglePlayerOnOff(on);

		if (on)
		{
			isWinState = true;
			StartCoroutine("TurnOffInvincibility");
		}
	}

	IEnumerator TurnOffInvincibility()
	{
		// WAIT FOR THE MOD DURATION TO FINISH
		yield return new WaitForSeconds(Global.TIMER_CONTINUE);
		isWinState = false;
		yield return null;
	}

    private void PickupKey(KeyBehavior.Keys key)
    {
        PickupKey(key, this.transform.position);
    }

	private void PickupKey(KeyBehavior.Keys k, Vector3 position)
    {

		GameObject.Find("NotificationText").GetComponent<NotificationManager>().KeyPickupMessage();
		//KeyIcon.sprite = GotKeyImage;

		// RED | ORANGE | YELLOW | GREEN | BLUE | PURPLE
		Color c = Color.white;

        switch (k)
        {
			case KeyBehavior.Keys.Red:
                GameObject.Find("RedKeyIcon").GetComponent<KeyIcon>().SetKeyActive(true);
				c = KeyBehavior.RedColor;
				mRedKey++;
                //print("Red Key pickup by Player2Axis");
				break;
			case KeyBehavior.Keys.Orange:
                GameObject.Find("OrangeKeyIcon").GetComponent<KeyIcon>().SetKeyActive(true);
				c = KeyBehavior.OrangeColor;
				mOrangeKey++;
                //print("Orange Key pickup by Player2Axis");
				break;
			case KeyBehavior.Keys.Yellow:
                GameObject.Find("YellowKeyIcon").GetComponent<KeyIcon>().SetKeyActive(true);
				c = KeyBehavior.YellowColor;
				mYellowKey++;
                //print("Yellow Key pickup by Player2Axis");
				break;
			case KeyBehavior.Keys.Green:
                GameObject.Find("GreenKeyIcon").GetComponent<KeyIcon>().SetKeyActive(true);
				c = KeyBehavior.GreenColor;
				mGreenKey++;
                //print("Green Key pickup by Player2Axis");
				break;
            case KeyBehavior.Keys.Blue:
                GameObject.Find("BlueKeyIcon").GetComponent<KeyIcon>().SetKeyActive(true);
                c = KeyBehavior.BlueColor;
                mBlueKey++;
                //print("Blue Key pickup by Player2Axis");
                break;
			case KeyBehavior.Keys.Purple:
                GameObject.Find("PurpleKeyIcon").GetComponent<KeyIcon>().SetKeyActive(true);
                c = KeyBehavior.PurpleColor;
                mPurpleKey++;
                //print("Purple Key pickup by Player2Axis");
				break;
            
        }
		var go = Instantiate(BurstManager);
		go.GetComponent<BurstManager>().MakeBurst(5, c, position, .5f);
		go = Instantiate(BurstManager);
        go.GetComponent<BurstManager>().MakeBurst(5, Color.black, position, .5f);
	}

	public bool HasKey(KeyBehavior.Keys k)
	{
		// RED | ORANGE | YELLOW | GREEN | BLUE | PURPLE
		switch (k)
		{
            case KeyBehavior.Keys.Red:
                if(mRedKey > 0)
                {
                    //mRedKey--;
                    if(mRedKey == 0)
                        GameObject.Find("RedKeyIcon").GetComponent<KeyIcon>().SetKeyActive(false);
                    return true;
                }
                break;
			case KeyBehavior.Keys.Orange:
				if (mOrangeKey > 0)
				{
					//mOrangeKey--;
					if (mOrangeKey == 0)
						GameObject.Find("OrangeKeyIcon").GetComponent<KeyIcon>().SetKeyActive(false);
					return true;
				}
				break;
			case KeyBehavior.Keys.Yellow:
				if (mYellowKey > 0)
				{
					//mYellowKey--;
					if (mYellowKey == 0)
						GameObject.Find("YellowKeyIcon").GetComponent<KeyIcon>().SetKeyActive(false);
					return true;
				}
				break;
			case KeyBehavior.Keys.Green:
				if (mGreenKey > 0)
				{
					//mGreenKey--;
					if (mGreenKey == 0)
						GameObject.Find("GreenKeyIcon").GetComponent<KeyIcon>().SetKeyActive(false);
					return true;
				}
				break;
			case KeyBehavior.Keys.Blue:
                if(mBlueKey > 0)
                {
					//mBlueKey--;
					if (mBlueKey == 0)
						GameObject.Find("BlueKeyIcon").GetComponent<KeyIcon>().SetKeyActive(false);
					return true;
                }
				break;
            case KeyBehavior.Keys.Purple:
                if(mPurpleKey > 0)
                {
                    //mPurpleKey--;
                    if (mPurpleKey == 0)
						GameObject.Find("PurpleKeyIcon").GetComponent<KeyIcon>().SetKeyActive(false);
                    return true;
                }
                break;
		}
        return false;
	}

    void MGlobal_OnLevelEnd()
    {
        isWinState = true;
    }
	
    public void ToggleOFFInvincible()
    {
        isWinState = false;
    }

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag.Contains("HPBoost"))
		{
			print("Boost pickup");
			this.PickupHPBoost(c.gameObject);
		}
	}

	public void PickupHPBoost(GameObject go)
	{
		if (PlayerHP)
			return;

		PlayerHP = true;

		var followGO = Instantiate(FollowPrefab);
		followGO.transform.position = go.transform.position;
		followGO.GetComponent<KeyFollowBehavior>().SetFollowGameObject(this.gameObject);

		PlayerHPFollowObject = followGO;

		Destroy(go);	
    }
	
	private GameObject PlayerHPFollowObject;
	private bool PlayerHP = false;
}
