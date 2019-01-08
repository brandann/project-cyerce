using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeadMenuPanelScript : MonoBehaviour {

	public GameObject mWatchAdButton;
	public GameObject mRestartButton;
	public GameObject mMainMenuButton;
    public GameObject mDeathCountUI;

#if UNITY_ANDROID
	private void Start()
	{
	}

    private void OnDestroy()
    {
    }

    void MGlobal_OnDeath()
    {

    }

    public void OnWatchAddButton()
	{
	}

	private void AdCallback(UnityEngine.Advertisements.ShowResult result)
	{
		ReturnToGame();
	}
#else
	public void OnWatchAddButton()
	{
		print("Watch Button Pressed");
		ReturnToGame();
	}
#endif

	private void ReturnToGame()
	{
		this.gameObject.SetActive(false);

		Time.timeScale = 1;

		//TODO: make player active and invincable
		var pgo = GameObject.FindGameObjectWithTag("Player");
		var player = pgo.GetComponent<HeroBehavior>();
		player.TogglePlayerOnOff(true);

        GameObject.Find("CountDownUI").GetComponent<CountDown>().StartCountDown(Global.TIMER_CONTINUE);
	}

	public void OnRestartButton()
	{
		SceneManager.LoadScene(Global.MAIN_GAME_SCENE);
		Time.timeScale = 1;
	}

	public void OnMainMenuButton()
	{
		SceneManager.LoadScene((int)MainMenu.scenes.Main);
		Time.timeScale = 1;
	}

    public void Init()
    {
        mDeathCountUI.GetComponent<Text>().text = "Death Count: " + Global.mGlobal.DeathCount;
    }
}
