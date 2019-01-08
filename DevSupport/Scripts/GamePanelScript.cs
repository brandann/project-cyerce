using UnityEngine;

public class GamePanelScript : MonoBehaviour {

	public GameObject mGamePanel;
	public GameObject mPausePanel;
    public GameObject mWatchAdPanel;
	public GameObject mDialogPanel;
	
	public void OnPauseButton()
	{
		Time.timeScale = 0;
		mPausePanel.SetActive(true);
		mGamePanel.SetActive(false);
	}

    public void OnWatchAd()
    {
        Time.timeScale = 0;
        mWatchAdPanel.SetActive(true);
        mGamePanel.SetActive(false);
    }

	public void OnDialog(string s)
	{
		Time.timeScale = 0;
		mDialogPanel.SetActive(true);
		mDialogPanel.GetComponent<DialogPanel>().SetMessage(s);
		mGamePanel.SetActive(false);
		
	}
}
