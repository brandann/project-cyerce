using UnityEngine;
using System.Collections;

public class FanRotation : MonoBehaviour {

    public int RotationDirection;
    public float RotationSpeed;

    private float _currentSpeed;

    private bool turning = true;

	// Use this for initialization
	void Start () {
        _currentSpeed = RotationSpeed;
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.Rotate(new Vector3(0, 0, -1 * RotationDirection * _currentSpeed * Time.deltaTime));
	}

    public void ToggleEase()
    {
        print("toggleease");
        if (turning)
            EaseStop();
        else
            EaseStart();
    }

    private void EaseStop()
    {
        print("easestop");
        turning = false;
        StartCoroutine("StopRoutine"); 
    }

    private void EaseStart()
    {
        print("easestart");
        turning = true;
        StartCoroutine("StartRoutine");
    }

	public void Reverse()
	{
		RotationDirection *= -1;
	}

	IEnumerator StopRoutine()
	{
        while (_currentSpeed > 0)
        {
            yield return new WaitForSeconds(.1f);
            _currentSpeed--;

        }
        _currentSpeed = 0;
		yield return null;
	}

	IEnumerator StartRoutine()
	{
		while (_currentSpeed < RotationSpeed)
		{
			yield return new WaitForSeconds(.1f);
            _currentSpeed++;

		}
        _currentSpeed = RotationSpeed;
		yield return null;
	}
}
