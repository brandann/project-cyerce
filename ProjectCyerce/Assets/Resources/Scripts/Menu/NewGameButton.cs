using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGameButton : MonoBehaviour {

    public GameObject SavePanel;

    public void Click()
    {
        //SavePanel.SetActive(true);
        SceneManager.LoadScene("TestingScene");
    }
}
