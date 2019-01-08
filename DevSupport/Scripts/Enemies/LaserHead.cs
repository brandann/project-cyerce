using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserHead : MonoBehaviour {

	private Vector3[] LaserPosition =
	{
		new Vector3(0,.5f,0),
		new Vector3(-.5f,0,0),
		new Vector3(0,-.5f,0),
		new Vector3(.5f,0,0)
	};

	private float[] LaserRotation = {0,90,180,270};

	public GameObject LaserPrefab;

	// Use this for initialization
	void Start () {
		MakeLaser(LaserPrefab, LaserPosition[0], LaserRotation[0]);
		MakeLaser(LaserPrefab, LaserPosition[1], LaserRotation[1]);
		MakeLaser(LaserPrefab, LaserPosition[2], LaserRotation[2]);
		MakeLaser(LaserPrefab, LaserPosition[3], LaserRotation[3]);
	}
	
	// Update is called once per frame
	void Update () {
        	
	}

    private void OnMouseDown()
    {
        var _fan = this.GetComponent<FanRotation>();
        if (null != _fan)
            _fan.ToggleEase();
    }

    void MakeLaser(GameObject prefab, Vector3 pos, float rot)
	{
		var go = Instantiate(prefab);
		go.transform.parent = this.transform;
		go.transform.position = this.transform.position + pos;
		go.transform.Rotate(new Vector3(0, 0, 1), rot);
	}

	public void ReverseRotation()
	{
		var fan = this.gameObject.GetComponent<FanRotation>();
		if (null != fan)
			fan.Reverse();
	}

	public void SetDirection(int i)
	{
		var fan = this.gameObject.GetComponent<FanRotation>();
		if (null != fan)
			fan.RotationDirection = i;
	}
}
