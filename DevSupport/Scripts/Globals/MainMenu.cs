using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public enum scenes { Main = 0, PlayerInChangingWorld = 1, ScaleSquares = 2 }
    public enum Input {Tilt=0, Touch=1, Mouse=2, Keyboard=3}
    public enum PhoneInput {Tilt=0, Touch=1}
    public enum PCInput {Mouse=2, Keyboard=3}

	private const string Input_Joystick = "Joystick";
	private const string Input_Tilt = "Screen Tilt";


    public Text text;

    public GameObject mMainPanel;
    public GameObject mSettingsPanel;
	public GameObject mResetPanel;
    public GameObject mPromptPanel;
    public GameObject mCreditsPanel;

    public GameObject mPromptButtonText;

	private void FixedUpdate()
    {
        if (UnityEngine.Input.GetKeyDown(KeyCode.KeypadEnter))
            PlayButtonPressed();
    }

    #region ButtonPress
    public void PlayButtonPressed()
    {
        //Global.mGlobal.CurrentLevel = 1;
        SceneManager.LoadScene("NodeGridMapScene");
    }

    public void QuitButtonPressed()
    {
        Application.Quit();
    }

    public void SettingsButtonPressed()
    {
        mSettingsPanel.SetActive(true);
        mMainPanel.SetActive(false);
        var go = GameObject.Find("InputText").GetComponent<Text>();
		
		go.text = "Input: " + (SaveLoad.SavedGame.FollowMouseEnabled? Input_Joystick : Input_Tilt);

		SaveLoad.Load();
		mPromptButtonText.GetComponent<Text>().text = "on death: " + SaveLoad.SavedGame.Prompt.ToString();
    }

    public void SettingsBackButtonPressed()
    {
		mSettingsPanel.SetActive(false);
		mMainPanel.SetActive(true);
    }

    public void SettingsInputButton()
    {
        SaveLoad.SavedGame.FollowMouseEnabled = !SaveLoad.SavedGame.FollowMouseEnabled;
        var go = GameObject.Find("InputText").GetComponent<Text>();
        go.text = "Input: " + (SaveLoad.SavedGame.FollowMouseEnabled ? Input_Joystick : Input_Tilt);
		SaveLoad.Save();
    }
	
    public void DevResetButton()
    {
		mResetPanel.SetActive(true);
		mSettingsPanel.SetActive(false);
	}

	public void YesButton()
	{
		mResetPanel.SetActive(false);
		mSettingsPanel.SetActive(true);

		SaveLoad.Reset();

		var go = GameObject.Find("InputText").GetComponent<Text>();
		go.text = "Input: " + (SaveLoad.SavedGame.FollowMouseEnabled ? Input_Joystick : Input_Tilt);

		mPromptButtonText.GetComponent<Text>().text = SaveLoad.SavedGame.Prompt.ToString();
	}

	public void NoButton()
	{
		mResetPanel.SetActive(false);
		mSettingsPanel.SetActive(true);
	}

    public void SettingsPromptButton()
    {
        mPromptPanel.SetActive(true);
        mSettingsPanel.SetActive(false);
        SaveLoad.Load();
        mPromptButtonText.GetComponent<Text>().text = SaveLoad.SavedGame.Prompt.ToString();
    }

    public void Prompt_Restart()
    {
        SaveLoad.SavedGame.Prompt = Global.PromptEnum.Restart;
        SaveLoad.Save();
        mPromptButtonText.GetComponent<Text>().text = "on death: " + SaveLoad.SavedGame.Prompt.ToString();
		mPromptPanel.SetActive(false);
		mSettingsPanel.SetActive(true);
    }

    public void Prompt_Continue()
    {
        SaveLoad.SavedGame.Prompt = Global.PromptEnum.Continue;
        SaveLoad.Save();
        mPromptButtonText.GetComponent<Text>().text = "on death: " + SaveLoad.SavedGame.Prompt.ToString();
		mPromptPanel.SetActive(false);
		mSettingsPanel.SetActive(true);
    }

    public void Prompt_prompt()
    {
        SaveLoad.SavedGame.Prompt = Global.PromptEnum.Prompt;
		SaveLoad.Save();
        mPromptButtonText.GetComponent<Text>().text = "on death: " + SaveLoad.SavedGame.Prompt.ToString();
		mPromptPanel.SetActive(false);
		mSettingsPanel.SetActive(true);
    }

    public void CreditsBackButton()
    {
        mCreditsPanel.SetActive(false);
        mSettingsPanel.SetActive(true);
    }

    public void ShowCreditsButton()
    {
		mCreditsPanel.SetActive(true);
		mSettingsPanel.SetActive(false);
    }

    #endregion

    private void Start()
    {
        #if UNITY_ANDROID
		Screen.sleepTimeout = (int)SleepTimeout.NeverSleep;
        #endif
	}


	void Update()
    {
        
    }
}