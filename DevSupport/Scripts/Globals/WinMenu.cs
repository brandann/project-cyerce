using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour {

    public enum scenes { Main = 0, PlayerInChangingWorld = 1, ScaleSquares = 2 }
    public Text text;

	#region ButtonPress

	private void Start()
	{
		text.text = Global.mGlobal.LevelEndQuote;
	}

	public void PlayButtonPressed()
    {
		SceneManager.LoadScene("NodeGridMapScene");
	}

    public void QuitButtonPressed()
    {
        Application.Quit();
    }
    #endregion

    void Update()
    {
		if (Input.GetMouseButtonDown(0))
		{
			PlayButtonPressed();
		}
    }
}
