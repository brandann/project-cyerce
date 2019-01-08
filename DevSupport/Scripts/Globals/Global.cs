using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Global {

    public const string MAIN_GAME_SCENE = "NodeGridMapScene";

    public enum PromptEnum { Prompt, Continue, Restart }
	

	public const float DEFAULT_CAMERA_SIZE = 6f; // was 4.5f
	public const float CAMERA_GROWTH_RATE = 1.1f;
	
	public const string PLANET_TAG = "Planet";
	public const string PATROL_TAG = "Patrol";
	public const string COLLECTION_TAG = "CollectionItem";
	public const string PLAYER_TAG = "Player";

    public const float TIMER_CONTINUE = 1.0f; //1.5f
    public const float TIMER_NEW_GAME = 1.0f; //3.0f
	public const bool ALLOW_CONTINUE = false;

	private static Global _global;
    public static Global mGlobal{
        set { _global = value; }
        get
        {
            if (_global == null)
                _global = new Global();
            return _global;
        }
    }

	private Vector3 _startingPosition;
	public Vector3 StartingPosition
	{
		get { return _startingPosition; }
		set { _startingPosition = value; }
	}

	public string LevelEndQuote = "";
	public Vector3 JoystickStartingPosition;

	public int MaxLevel = 1;
    public const int MinLevel = 0;
	public List<GameMap> LevelList;

	public Global()
	{
		SaveLoad.Load();
		
		LevelList = new List<GameMap>(); //[MAX_LEVELS];
		
		LevelList.Add(new Map000()); // REMOVE UPON RELEASE

		// TUTORIAL ---------------------
		LevelList.Add(new Map001());
		LevelList.Add(new Map002());
		LevelList.Add(new Map003());
		LevelList.Add(new Map004());

		// GAME ------------------------
		LevelList.Add(new Map101());
		LevelList.Add(new Map102());
		LevelList.Add(new Map103());
		LevelList.Add(new Map104());
		LevelList.Add(new Map107());
		LevelList.Add(new Map111());
		LevelList.Add(new Map112());
		LevelList.Add(new Map118());

		MaxLevel = LevelList.Count;
	}

	public int CurrentLevel
    {
        get { return SaveLoad.SavedGame.CurrentLevel; }
        set {
			SaveLoad.SavedGame.CurrentLevel = value;
			SaveLoad.Save();
        }
    }

    public int DeathCount
    {
        get { return SaveLoad.SavedGame.DeathCount; }
        set
        {
            SaveLoad.SavedGame.DeathCount = value;
            SaveLoad.Save();
        }
    }

    // TRIGGERS WHEN THE LEVEL ENDS, SPECIFICALLY WHEN THE PLAYER
    // REACHES THE GOAL POINT AND THE LEVEL IS BEAT
    public delegate void LevelEndHandler();
    public event LevelEndHandler OnLevelEnd = delegate { };
    public void TriggerOnLevelEnd() { OnLevelEnd(); }

    // TRIGGERS WHEN A NEW LEVEL IS TO BE LOADED
    // ON START GAME OR AFTER A LEVEL HAS BEEN COMPLETED
    // ALSO TRIGGERS WHEN A PLAYER DIES AND RESTARTS A LEVEL
    public delegate void NewLevelHandler(int LevelIndex);
    public event NewLevelHandler OnNewLevel = delegate { };
    public void TriggerOnNewLevel(int i) { OnNewLevel(i); }

    // TRIGGERS WHEN THE PLAYER DIES
    public delegate void DeathHandler();
    public event DeathHandler OnDeath = delegate { };
    public void TriggerOnDeath() { OnDeath(); }

    // TRIGGERS WHEN THE PLAYER DIES
    public delegate void CollectionHandler();
    public event CollectionHandler OnCollection = delegate { };
    public void TriggerOnCollection() { OnCollection(); }

    // TRIGGERS WHEN THE PLAYER PICKS UP A KEY
    public delegate void KeyHandler(KeyBehavior.Keys key);
    public event KeyHandler OnKeyPickup = delegate { };
    public void TriggerOnKeyPickup(KeyBehavior.Keys key) { OnKeyPickup(key); }

    // TRIGGERS WHEN THE PLAYER CLICKS ON WATCH AD
    public delegate void WatchAdHandler();
    public event WatchAdHandler OnWatchAd = delegate {};
    public void TriggerOnWatchAd() { OnWatchAd(); }
}
