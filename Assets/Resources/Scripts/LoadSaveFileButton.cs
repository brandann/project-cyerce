using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSaveFileButton : MonoBehaviour {

    public GameObject LoadPanel;

	public void Click()
    {
        LoadPanel.SetActive(true);
        SaveData s = new SaveData();
        s.LoadDataFromFile();

    }
}
