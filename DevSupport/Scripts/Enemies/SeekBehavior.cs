using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekBehavior : MonoBehaviour {

    private GameObject mOther = null;
    public float mSpeed;
    public float mRotation;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (null == mOther)
            return;
        
		var LR = NMath.GetLeftRight(this.transform, mOther.transform);
		//var FB = NMath.GetFrontBack(this.transform, mOther.transform);
		var dir = (LR == NMath.LeftRight.Left) ? -1 : 1;
		transform.Rotate(Vector3.forward, dir * -1 * (mRotation * Time.smoothDeltaTime));
		this.GetComponent<Rigidbody2D>().velocity = transform.up.normalized * (mSpeed * Time.deltaTime);
		//transform.position += transform.up * ( * Time.smoothDeltaTime);
	}

    public void SetTarget(GameObject go)
    {
        mOther = go;
    }
}
