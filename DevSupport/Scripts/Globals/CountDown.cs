using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : MonoBehaviour {

    private Text UI_Text;

    private float _currentTime = -1;
    private const int DEC_PLACE = 2;

	// Use this for initialization
	void Start () {
        UI_Text = this.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        //SetUI_Time();
	}

	public void StartCountDown(float c)
	{
		UI_Text = this.GetComponent<Text>();
		_currentTime = c;
		StartCoroutine("ReadyGoRoutine");
	}
	/*
		public void StartCountDown(float c)
		{
			_currentTime = c;
			//UI_Text.text = _currentTime.ToString();
			UI_Text.text = "Ready...";
		}

		private void SetUI_Time()
		{

			if (_currentTime > 0)
			{
				int t = (int) _currentTime * 10 * DEC_PLACE;
				float f = t / (10 * DEC_PLACE);
				//UI_Text.text = (f+0f).ToString();
				_currentTime -= Time.unscaledDeltaTime;
			}
			else
			{
				UI_Text.text = "";
				_currentTime = -1;
			}

		}
	*/
	IEnumerator ReadyGoRoutine()
	{
		UI_Text.text = "Ready...";
		yield return new WaitForSeconds(_currentTime);
		UI_Text.text = "GO!";
		yield return new WaitForSeconds(_currentTime);
		UI_Text.text = "";
	}
}
