using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GlobalGameObject : MonoBehaviour {

    public GameObject mPlayer;
    public GameObject mPlanets;
    public GameObject mPatrol;
    public GameObject mDoor;
    public GameObject mStartBox;
    public GameObject mGoalBox;

    public Text mCollectionText;
    //public static GameMap[] mLevels;
    public GameObject[] mKeyPrefabs;
    public GameObject mCollectionPrefab;
    public GameObject mWallPrefab;
    public GameObject mSignPrefab;
	public GameObject mLaserHeadPrefab;
	//public GameObject mPortalPrefab;
	//public GameObject mSaveFlagPrefab;
	public GameObject mLavaPrefab;
	public GameObject mBoostPrefab;
	public GameObject mPaceEnemyPrefab;
	public GameObject mWallQ1;
	public GameObject mWallQ2;
	public GameObject mWallQ3;
	public GameObject mWallQ4;
	public GameObject mHPBoostPrefab;
    public GameObject mWatchAdPrefab;
    public GameObject mPathPrefab;
	private Camera mCamera;

	public GameObject mDeadMenuPanel;
	
    private const int LEVEL_ERROR = -1;
	
    private int LevelCollectionItems;
    private int mCollectionItemsAquired = 0;

    public bool CollectionAcheived
    {
        get { return LevelCollectionItems == mCollectionItemsAquired; }
        set { }
    }

    // Use this for initialization
    void Start () {

        if (!UnityEngine.Advertisements.Advertisement.isInitialized)
        {
            print("Init Advertisment");
            UnityEngine.Advertisements.Advertisement.Initialize("1717922");
        }

		Screen.orientation = ScreenOrientation.LandscapeLeft;
		
        Initiate(true);
    }

    private void Initiate(bool NewGame)
    {
        Global.mGlobal.OnDeath += OnDeath;
        Global.mGlobal.OnLevelEnd += OnLevelEnd;

        mCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        Instantiate_Level(Global.mGlobal.CurrentLevel);
        Instantiate_Replace(Global.PLANET_TAG, mPlanets);
        LevelCollectionItems = CountCollectionItems(Global.COLLECTION_TAG);

		if (NewGame)
            Instantiate(mPlayer);
		
        mCollectionItemsAquired = 0;

        StartCoroutine("LevelStartRoutine");
    }

    private void MakePath(int x, int y)
    {
        Instantiate(mPathPrefab, new Vector3(x, y, 0), this.transform.rotation);
    }

    private void Instantiate_Level(int currentLevel)
    {
        Map map = Global.mGlobal.LevelList[currentLevel].GetMap();
        var mArray = map.MapArray;

        for (int i = 0; i < mArray.GetLength(0); i++)
		{
            for (int j = 0; j < mArray.GetLength(1); j++)
			{
                bool makePath = true;
                int x = j;
                int y = -i;
                switch(mArray[i,j])
                {
                    case GameMap.ERROR: //-1
                        // do nothing this is either white space or path
                        makePath = false;
                        break;
                    case GameMap.WALL: //0
                        Instantiate(mWallPrefab, new Vector3(x, y, 0), this.transform.rotation);
                        break;
                    case GameMap.OPEN_PATH: // 1
                        break;
					case GameMap.KEY_RED: // 2
						Instantiate(mKeyPrefabs[0], new Vector3(x, y, 0), this.transform.rotation);
						break;
					case GameMap.KEY_ORANGE: // 3
						Instantiate(mKeyPrefabs[1], new Vector3(x, y, 0), this.transform.rotation);
						break;
					case GameMap.KEY_YELLOW: // 4
						Instantiate(mKeyPrefabs[2], new Vector3(x, y, 0), this.transform.rotation);
						break;
					case GameMap.KEY_GREEN: // 5
						Instantiate(mKeyPrefabs[3], new Vector3(x, y, 0), this.transform.rotation);
						break;
					case GameMap.KEY_BLUE: // 6
						Instantiate(mKeyPrefabs[4], new Vector3(x, y, 0), this.transform.rotation);
						break;
					case GameMap.KEY_PURPLE: // 7
						Instantiate(mKeyPrefabs[5], new Vector3(x, y, 0), this.transform.rotation);
						break;
					case GameMap.START_NODE: // 8
						Instantiate(mStartBox, new Vector3(x, y, 0), this.transform.rotation);
                        Global.mGlobal.StartingPosition = new Vector3(x, y, 0);
                        mCamera.GetComponent<Camera>().transform.position = Global.mGlobal.StartingPosition;
						break;
                    case GameMap.END_NODE:
						Instantiate(mGoalBox, new Vector3(x, y, 0), this.transform.rotation);
						break;
					case GameMap.DOOR_RED:
						var drr = Instantiate(mDoor, new Vector3(x, y, 0), this.transform.rotation);
                        drr.GetComponent<DoorBehavior>().mCurrentKey = KeyBehavior.Keys.Red;
						break;
					case GameMap.DOOR_BLUE:
						var drb = Instantiate(mDoor, new Vector3(x, y, 0), this.transform.rotation);
						drb.GetComponent<DoorBehavior>().mCurrentKey = KeyBehavior.Keys.Blue;
						break;
					case GameMap.DOOR_GREEN:
						var drg = Instantiate(mDoor, new Vector3(x, y, 0), this.transform.rotation);
						drg.GetComponent<DoorBehavior>().mCurrentKey = KeyBehavior.Keys.Green;
						break;
					case GameMap.DOOR_ORANGE:
						var dro = Instantiate(mDoor, new Vector3(x, y, 0), this.transform.rotation);
						dro.GetComponent<DoorBehavior>().mCurrentKey = KeyBehavior.Keys.Orange;
						break;
					case GameMap.DOOR_PURPLE:
						var drp = Instantiate(mDoor, new Vector3(x, y, 0), this.transform.rotation);
						drp.GetComponent<DoorBehavior>().mCurrentKey = KeyBehavior.Keys.Purple;
						break;
					case GameMap.DOOR_YELLOW:
						var dry = Instantiate(mDoor, new Vector3(x, y, 0), this.transform.rotation);
						dry.GetComponent<DoorBehavior>().mCurrentKey = KeyBehavior.Keys.Yellow;
						break;
					case GameMap.COLLECTION:
						Instantiate(mCollectionPrefab, new Vector3(x, y, 0), this.transform.rotation);
						break;
                    case GameMap.MONSTER:
						Instantiate(mPlanets, new Vector3(x, y, 0), this.transform.rotation);
						break;
					case GameMap.DOOR_RANDOM:
						var drand = Instantiate(mDoor, new Vector3(x, y, 0), this.transform.rotation);
                        //var rand = UnityEngine.Random.Range(0, 6);
                        //print("Random Door: " + rand);
						DoorBehavior randomDoorBehavior = drand.GetComponent<DoorBehavior>();
						randomDoorBehavior.SetRandom();
						break;
                    case GameMap.SIGN_SA:
                        var signa = Instantiate(mSignPrefab, new Vector3(x, y, 0), this.transform.rotation);
						var signa_t = signa.GetComponent<SignBehavior>();
						signa_t.mSignMessage = Global.mGlobal.LevelList[currentLevel].SA_TEXT;
                        break;
					case GameMap.SIGN_SB:
						var signb = Instantiate(mSignPrefab, new Vector3(x, y, 0), this.transform.rotation);
						var signb_t = signb.GetComponent<SignBehavior>();
						signb_t.mSignMessage = Global.mGlobal.LevelList[currentLevel].SB_TEXT;
						break;
					case GameMap.SIGN_SC:
						var signc = Instantiate(mSignPrefab, new Vector3(x, y, 0), this.transform.rotation);
						var signc_t = signc.GetComponent<SignBehavior>();
						signc_t.mSignMessage = Global.mGlobal.LevelList[currentLevel].SC_TEXT;
						break;
					case GameMap.SIGN_SD:
						var signd = Instantiate(mSignPrefab, new Vector3(x, y, 0), this.transform.rotation);
						var signd_t = signd.GetComponent<SignBehavior>();
						signd_t.mSignMessage = Global.mGlobal.LevelList[currentLevel].SD_TEXT;
						break;
					case GameMap.LASERHEAD_CLOCKWISE:
						var lclock = Instantiate(mLaserHeadPrefab, new Vector3(x, y, 0), this.transform.rotation);
						var laser_clock = lclock.GetComponent<LaserHead>();
						if (null != laser_clock)
							laser_clock.SetDirection(1);
						break;
					case GameMap.ENEMY_PACE_UP:
						var ENEMY_PACE_UP_up = Instantiate(mPaceEnemyPrefab, new Vector3(x, y, 0), this.transform.rotation);
						ENEMY_PACE_UP_up.GetComponent<PaceEnemy>().SetPaceDirection(PaceEnemy.PaceDirection.Up);

						//var ENEMY_PACE_UP_left = Instantiate(mPaceEnemyPrefab, new Vector3(x, y, 0), this.transform.rotation);
						//ENEMY_PACE_UP_left.transform.position += new Vector3(0, 0, 0);
						//ENEMY_PACE_UP_left.GetComponent<PaceEnemy>().SetPaceDirection(PaceEnemy.PaceDirection.Up);

						//var ENEMY_PACE_UP_right = Instantiate(mPaceEnemyPrefab, new Vector3(x, y, 0), this.transform.rotation);
						//ENEMY_PACE_UP_right.transform.position += new Vector3(0, -.5f, 0);
						//ENEMY_PACE_UP_right.GetComponent<PaceEnemy>().SetPaceDirection(PaceEnemy.PaceDirection.Up);
						break;
					case GameMap.ENEMY_PACE_DOWN:
						var ENEMY_PACE_DOWN = Instantiate(mPaceEnemyPrefab, new Vector3(x, y, 0), this.transform.rotation);
						ENEMY_PACE_DOWN.GetComponent<PaceEnemy>().SetPaceDirection(PaceEnemy.PaceDirection.Down);
						break;
					case GameMap.ENEMY_PACE_LEFT:
						var ENEMY_PACE_LEFT = Instantiate(mPaceEnemyPrefab, new Vector3(x, y, 0), this.transform.rotation);
						ENEMY_PACE_LEFT.GetComponent<PaceEnemy>().SetPaceDirection(PaceEnemy.PaceDirection.Left);
						break;
					case GameMap.ENEMY_PACE_RIGHT:
						var ENEMY_PACE_RIGHT = Instantiate(mPaceEnemyPrefab, new Vector3(x, y, 0), this.transform.rotation);
						ENEMY_PACE_RIGHT.GetComponent<PaceEnemy>().SetPaceDirection(PaceEnemy.PaceDirection.Right);
						break;
					case GameMap.HP_BOOST:
						Instantiate(mHPBoostPrefab, new Vector3(x, y, 0), this.transform.rotation);
						break;
					case GameMap.ENEMY_PACE_VERTICAL:
						var ENEMY_PACE_HORIZONTAL_UP = Instantiate(mPaceEnemyPrefab, new Vector3(x, y, 0), this.transform.rotation);
						ENEMY_PACE_HORIZONTAL_UP.GetComponent<PaceEnemy>().SetPaceDirection(PaceEnemy.PaceDirection.Up);
						var ENEMY_PACE_HORIZONTAL_DOWN = Instantiate(mPaceEnemyPrefab, new Vector3(x, y, 0), this.transform.rotation);
						ENEMY_PACE_HORIZONTAL_DOWN.GetComponent<PaceEnemy>().SetPaceDirection(PaceEnemy.PaceDirection.Down);
						break;
					case GameMap.ENEMY_PACE_HORIZONTAL:
						var ENEMY_PACE_HORIZONTAL_LEFT = Instantiate(mPaceEnemyPrefab, new Vector3(x, y, 0), this.transform.rotation);
						ENEMY_PACE_HORIZONTAL_LEFT.GetComponent<PaceEnemy>().SetPaceDirection(PaceEnemy.PaceDirection.Left);
						var ENEMY_PACE_HORIZONTAL_RIGHT = Instantiate(mPaceEnemyPrefab, new Vector3(x, y, 0), this.transform.rotation);
						ENEMY_PACE_HORIZONTAL_RIGHT.GetComponent<PaceEnemy>().SetPaceDirection(PaceEnemy.PaceDirection.Right);
						break;
					case GameMap.LASERHEAD_COUNTER:
						var lcount = Instantiate(mLaserHeadPrefab, new Vector3(x, y, 0), this.transform.rotation);
						var laser_count = lcount.GetComponent<LaserHead>();
						if (null != laser_count)
							laser_count.SetDirection(-1);
						break;
					case GameMap.BOOST_NORTH:
						var go_bn = Instantiate(mBoostPrefab, new Vector3(x, y, 0), this.transform.rotation);
						go_bn.GetComponent<Boost>().BoostDirection = Boost.BoostDir.North;
						break;
					case GameMap.BOOST_NORTHEAST:
						var go_bne = Instantiate(mBoostPrefab, new Vector3(x, y, 0), this.transform.rotation);
						go_bne.GetComponent<Boost>().BoostDirection = Boost.BoostDir.NorthEast;
						break;
					case GameMap.BOOST_EAST:
						var go_be = Instantiate(mBoostPrefab, new Vector3(x, y, 0), this.transform.rotation);
						go_be.GetComponent<Boost>().BoostDirection = Boost.BoostDir.East;
						break;
					case GameMap.BOOST_SOUTHEAST:
						var go_bse = Instantiate(mBoostPrefab, new Vector3(x, y, 0), this.transform.rotation);
						go_bse.GetComponent<Boost>().BoostDirection = Boost.BoostDir.SouthEast;
						break;
					case GameMap.BOOST_SOUTH:
						var go_bs = Instantiate(mBoostPrefab, new Vector3(x, y, 0), this.transform.rotation);
						go_bs.GetComponent<Boost>().BoostDirection = Boost.BoostDir.South;
						break;
					case GameMap.BOOST_SOUTHWEST:
						var go_bsw = Instantiate(mBoostPrefab, new Vector3(x, y, 0), this.transform.rotation);
						go_bsw.GetComponent<Boost>().BoostDirection = Boost.BoostDir.SouthWest;
						break;
					case GameMap.BOOST_WEST:
						var go_bw = Instantiate(mBoostPrefab, new Vector3(x, y, 0), this.transform.rotation);
						go_bw.GetComponent<Boost>().BoostDirection = Boost.BoostDir.West;
						break;
					case GameMap.BOOST_NORTHWEST:
						var go_bnw = Instantiate(mBoostPrefab, new Vector3(x, y, 0), this.transform.rotation);
						go_bnw.GetComponent<Boost>().BoostDirection = Boost.BoostDir.NorthWest;
						break;
					case GameMap.WALL_Q1:
						Instantiate(mWallQ1, new Vector3(x, y, 0), this.transform.rotation);
						break;
					case GameMap.WALL_Q2:
						Instantiate(mWallQ2, new Vector3(x, y, 0), this.transform.rotation);
						break;
					case GameMap.WALL_Q3:
						Instantiate(mWallQ3, new Vector3(x, y, 0), this.transform.rotation);
						break;
					case GameMap.WALL_Q4:
						Instantiate(mWallQ4, new Vector3(x, y, 0), this.transform.rotation);
						break;
					case GameMap.LAVA_BLOCK_BOTTOM:
						var lbb = Instantiate(mLavaPrefab, new Vector3(x, y, 0), this.transform.rotation);
						lbb.transform.Rotate(new Vector3(0,0,0));
						break;
					case GameMap.LAVA_BLOCK_LEFT:
						var lbl = Instantiate(mLavaPrefab, new Vector3(x, y, 0), this.transform.rotation);
						lbl.transform.Rotate(new Vector3(0, 0, -90));
						break;
					case GameMap.LAVA_BLOCK_TOP:
						var lbt = Instantiate(mLavaPrefab, new Vector3(x, y, 0), this.transform.rotation);
						lbt.transform.Rotate(new Vector3(0, 0, 180));
						break;
					case GameMap.LAVA_BLOCK_RIGHT:
						var lbr = Instantiate(mLavaPrefab, new Vector3(x, y, 0), this.transform.rotation);
						lbr.transform.Rotate(new Vector3(0, 0, 90));
						break;
					case GameMap.WATCH_TV:
						Instantiate(mWatchAdPrefab, new Vector3(x, y, 0), this.transform.rotation);
						break;
                    //case GameMap.TURRET_UP:
                    //    var tu = Instantiate(mTurretPrefab, new Vector3(x, y, 0), this.transform.rotation);
                    //    break;
                    //case GameMap.TURRET_DOWN:
                    //    var td = Instantiate(mTurretPrefab, new Vector3(x, y, 0), this.transform.rotation);
                    //    break;
                    //case GameMap.TURRET_LEFT:
                    //    var tl = Instantiate(mTurretPrefab, new Vector3(x, y, 0), this.transform.rotation);
                    //    break;
                    //case GameMap.TURRET_RIGHT:
                        //var tr = Instantiate(mTurretPrefab, new Vector3(x, y, 0), this.transform.rotation);
                        //break;

				}
                if(makePath)
                    MakePath(x, y);
				
			}
		}
    }

    private void Instantiate_Replace_Keys()
    {
        var nodes = GameObject.FindGameObjectsWithTag("Key");
        foreach (GameObject n in nodes)
        {
            var vec = n.transform.position;
            var key = n.GetComponent<KeyBehavior>().mCurrentKey;
            Destroy(n);
            var go = Instantiate(mKeyPrefabs[(int) key]);
            go.transform.position = vec;
        }
    }

    private int CountCollectionItems(string cOLLECTION_TAG)
    {
        var items = GameObject.FindGameObjectsWithTag(cOLLECTION_TAG);
        int count = 0;
        foreach (GameObject n in items)
        {
            var vec = n.transform.position;
            Destroy(n);
            var go = Instantiate(mCollectionPrefab);
            go.transform.position = vec;
            count++;
        }
        return items.Length;
    }

    private void Instantiate_Replace(string prefab_tag, GameObject prefab_object)
    {
        var nodes = GameObject.FindGameObjectsWithTag(prefab_tag);
        foreach (GameObject n in nodes)
        {
            var vec = n.transform.position;
            Destroy(n);
            var go = Instantiate(prefab_object);
            go.transform.position = vec;
            go.gameObject.SendMessage("Init");
        }
    }

    private void Instantiate_Patrol()
    {
        var nodes = GameObject.FindGameObjectsWithTag(Global.PATROL_TAG);
        for (int i = 0; i < nodes.Length; i++)
        {
            var pos = nodes[i].transform.position;
            Vector3[] vec = nodes[i].GetComponent<PatrolNode>().Points;
            var go = Instantiate(mPatrol);
            go.transform.position = pos;
            go.GetComponent<PatrolBehavior>().SetPoints(vec);
            Destroy(nodes[i]);
        }
    }

    private void OnDestroy()
    {
        Global.mGlobal.OnDeath -= OnDeath;
        Global.mGlobal.OnLevelEnd -= OnLevelEnd;
    }

    private void OnDeath()
    {
        StartCoroutine("ResetRoutine");
    }

    IEnumerator ResetRoutine()
    {
        // WAIT FOR THE MOD DURATION TO FINISH
        yield return new WaitForSeconds(1.1f);
		var deadPanel = mDeadMenuPanel.GetComponent<DeadMenuPanelScript>();
        deadPanel.gameObject.SetActive(true);
        deadPanel.Init();

        GameObject.Find("GamePanel").SetActive(false);
        GameObject.Find("MobileSingleStickControl").SetActive(false);

		if(!Global.ALLOW_CONTINUE)
		{
			if (SaveLoad.SavedGame.Prompt == Global.PromptEnum.Prompt)
			{
				mDeadMenuPanel.SetActive(true);
				Time.timeScale = 0;
			}
			else if (SaveLoad.SavedGame.Prompt == Global.PromptEnum.Continue)
			{
				deadPanel.OnWatchAddButton();
			}
			else if (SaveLoad.SavedGame.Prompt == Global.PromptEnum.Restart)
			{
				deadPanel.OnRestartButton();
			}
		}
		else
		{
			deadPanel.OnRestartButton();
		}
		
		yield return null;
    }

    private void OnLevelEnd()
    {
        StartCoroutine("LevelEndRoutine");
    }

    IEnumerator LevelEndRoutine()
    {
        // WAIT FOR THE MOD DURATION TO FINISH
        mCamera.GetComponent<CameraManager>().ZoomOnPlayer();
        Time.timeScale = .1f;
        yield return new WaitForSeconds(Global.TIMER_NEW_GAME * Time.timeScale);
		
        print("END LEVEL: " + Global.mGlobal.CurrentLevel);

		Global.mGlobal.LevelEndQuote = Global.mGlobal.LevelList[Global.mGlobal.CurrentLevel].GetLevelEndQuote();

        Global.mGlobal.CurrentLevel++;

        Time.timeScale = 1;
        if(Global.mGlobal.CurrentLevel < Global.mGlobal.MaxLevel)
            SceneManager.LoadScene("WinScreen");
        else
            SceneManager.LoadScene("GameWinScreen");
        yield return null;
    }

    IEnumerator LevelStartRoutine()
    {
        // WAIT FOR THE MOD DURATION TO FINISH
        IncrementCollectionItems(0);
		GameObject.Find("CountDownUI").GetComponent<CountDown>().StartCountDown(Global.TIMER_NEW_GAME);

        while (mCamera.orthographicSize < Global.DEFAULT_CAMERA_SIZE)
        {
            yield return new WaitForSeconds(0);
            mCamera.orthographicSize *= Global.CAMERA_GROWTH_RATE;
        }
        mCamera.orthographicSize = Global.DEFAULT_CAMERA_SIZE;

        Time.timeScale = .1f;
		
        yield return new WaitForSeconds(Global.TIMER_NEW_GAME * Time.timeScale);

        Time.timeScale = 1;

		//GameObject.FindGameObjectWithTag(Global.PLAYER_TAG).GetComponent<HeroBehavior>().ToggleOFFInvincible();
		GameObject.FindGameObjectWithTag(Global.PLAYER_TAG).GetComponent<HeroBehavior>().TogglePlayerOnOff(true);

		yield return null;
    }

    public void IncrementCollectionItems(int i)
    {
        mCollectionItemsAquired += i;
        if (mCollectionItemsAquired == LevelCollectionItems)
            Global.mGlobal.TriggerOnCollection();
    }

    public void SetLevel(int lvl, GameMap script)
    {
        
    }
}
