using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    private Transform mCameraTransform;

    // How long the object should shake for.
    public float mShake = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float mShakeAmount = 0.7f;
    public float mDecreaseFactor = .1f;

    private GameObject player;

	private void Start()
	{
		Global.mGlobal.OnDeath += Shake;
	}

	private void OnDestroy()
	{
		Global.mGlobal.OnDeath -= Shake;
	}

	void Awake()
    {
        if (mCameraTransform == null)
        {
            mCameraTransform = GameObject.Find("Main Camera").GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
		player = GameObject.FindGameObjectWithTag("Player");
	}

    void Update()
    {
        if ((mShake != 0 && Time.deltaTime == 0) || mShake <= 0)
		{
			mShake = 0;
            return;
		}

        if(null == player)
        {
			player = GameObject.FindGameObjectWithTag("Player");
			return;
        }

        mCameraTransform.localPosition += Random.insideUnitSphere * mShakeAmount * Time.timeScale;
		mShake -= Time.deltaTime * mDecreaseFactor;
	}

	public void Shake()
    {
        Shake(0.1f);
    }

    public void Shake(float f)
    {
        mShake = f;
    }
}
