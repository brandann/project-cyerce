using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FightingSimButton : MonoBehaviour
{
	public void Click()
	{
		SceneManager.LoadScene("FightingSim");
	}
}
