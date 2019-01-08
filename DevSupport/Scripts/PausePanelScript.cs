using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelScript : MonoBehaviour {

	public GameObject mGamePanel;

	private const string SKIPPABLE = "video";
	private const string REWARD = "rewardVideo";

    private void Start()
    {
        
    }

    public void OnReturnButton()
	{
		Time.timeScale = 1;
		this.gameObject.SetActive(false);
		mGamePanel.SetActive(true);
	}

	public void OnRestartButton()
	{
		//print("Restart Button Pressed");
		SceneManager.LoadScene(Global.MAIN_GAME_SCENE);
		Time.timeScale = 1;
	}

	public void OnMainMenuButton()
	{
		//print("Main Menu Button Pressed");
		SceneManager.LoadScene((int)MainMenu.scenes.Main);
		Time.timeScale = 1;
	}

    public void OnWatchAd()
    {
        print("Checking if Unity Ads are ready...");
		if (UnityEngine.Advertisements.Advertisement.IsReady())
		{
            print("attempting to play unity ad...");
			var options = new UnityEngine.Advertisements.ShowOptions { resultCallback = AdCallback };
			UnityEngine.Advertisements.Advertisement.Show(SKIPPABLE, options);
		}
		else
		{
            AdCallback(UnityEngine.Advertisements.ShowResult.Failed);
		}
    }

	private void AdCallback(UnityEngine.Advertisements.ShowResult result)
	{
        print("Unity ad callback: " + result.ToString());
		GameObject.FindWithTag("Player").GetComponent<HeroBehavior>().PickupHPBoost(this.gameObject);
        Destroy(GameObject.FindWithTag("WatchTV"));
		Time.timeScale = 1;
		this.gameObject.SetActive(false);
		mGamePanel.SetActive(true);
	}
}
