using UnityEngine;

[System.Serializable]
public class Game 
{
    private int _CurrentLevel;
	private bool _FollowMouseEnabled;
    private Global.PromptEnum _Prompt;
    private int _DeathCount;
	
	public Game()
	{
        CurrentLevel = 0;
		FollowMouseEnabled = false;
	}
	
	public int CurrentLevel
	{
		get{return _CurrentLevel;}
		set{_CurrentLevel = value;}
	}

	public bool FollowMouseEnabled
	{
		get { return _FollowMouseEnabled; }
		set { _FollowMouseEnabled = value; }
	}

    public Global.PromptEnum Prompt
    {
        get {return _Prompt; }
        set { _Prompt = value; }
    }

	public int DeathCount
	{
		get { return _DeathCount; }
		set { _DeathCount = value; }
	}
}
