using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CameraManager : MonoBehaviour {

    public delegate void LevelupHandler(int level);
    public event LevelupHandler OnLevelup = delegate { };
    public static float WinWaitTimeToRestart = 3;
    private Camera camera;

    private int _currentLevel = 1;
    public const int MAX_LEVEL = 4;
    public int CurrentLevel
    {
        get { return _currentLevel; }
        private set { _currentLevel = value; }
    }

    public NotificationManager mNotificationManager;

	// Use this for initialization
	void Start () {
        mNotificationManager = GameObject.Find("NotificationText").GetComponent<NotificationManager>();
        camera = this.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		
		//if (Input.touchCount == 2)
		//{
		//	// Store both touches.
		//	Touch touchZero = Input.GetTouch(0);
		//	Touch touchOne = Input.GetTouch(1);

		//	// Find the position in the previous frame of each touch.
		//	Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
		//	Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

		//	// Find the magnitude of the vector (the distance) between the touches in each frame.
		//	float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
		//	float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

		//	// Find the difference in the distances between each frame.
		//	float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

		//	// If the camera is orthographic...

		//	// ... change the orthographic size based on the change in distance between the touches.
		//	camera.orthographicSize += deltaMagnitudeDiff * 1;

  //          // Make sure the orthographic size never drops below zero.
  //          camera.orthographicSize = Mathf.Min(camera.orthographicSize, 12);
		//	camera.orthographicSize = Mathf.Max(camera.orthographicSize, Global.DEFAULT_CAMERA_SIZE);
		//}
		//else if(camera.orthographicSize > Global.DEFAULT_CAMERA_SIZE)
		//{
		//	camera.orthographicSize -= Global.CAMERA_GROWTH_RATE;
		//}
		//else if(camera.orthographicSize < Global.DEFAULT_CAMERA_SIZE)
		//{
		//	camera.orthographicSize += Global.CAMERA_GROWTH_RATE;
		//}
	}

	public void restartAfter3Seconds()
	{
		StartCoroutine("ResetRoutine");
	}

	// COROUTINE FOR SPEED MOD
	IEnumerator ResetRoutine()
	{
		// WAIT FOR THE MOD DURATION TO FINISH
		yield return new WaitForSeconds(3);
		SceneManager.LoadScene((int)MainMenu.scenes.Main);
		yield return null;
	}

    IEnumerator LoadWinScreenRoutine()
    {
        // WAIT FOR THE MOD DURATION TO FINISH
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("WinScreen");
        yield return null;
    }

    public void ZoomOnPlayer()
    {
        StartCoroutine("ZoomOnPlayerRoutine");
    }

    IEnumerator ZoomOnPlayerRoutine()
    {
        while (camera.orthographicSize > .1f)
        {
            camera.orthographicSize *= .98f;
            yield return new WaitForSeconds(.02f * Time.timeScale);
        }
        yield return null;
    }

    // COROUTINE FOR SPEED MOD
    IEnumerator WinRoutine()
    {
        // WAIT FOR THE MOD DURATION TO FINISH
        yield return new WaitForSeconds(WinWaitTimeToRestart);
        var l = GameObject.FindGameObjectsWithTag("SpawnedBlocks");
        foreach (GameObject p in l)
        {
            Destroy(p);
        }

        //Planet
        var planets = GameObject.FindGameObjectsWithTag("Planet");
        foreach (GameObject p in planets)
        {
            Destroy(p);
        }
        OnLevelup(_currentLevel);
        print("level: " + _currentLevel);
        yield return null;
    }
}
