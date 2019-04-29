using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer1 : MonoBehaviour
{
	Camera camera;
	// Start is called before the first frame update
	void Start()
	{
		camera = this.GetComponent<Camera>();
		float vertExtent = camera.orthographicSize;
		float horzExtent = vertExtent * Screen.width / Screen.height;
		leftBound = (float)(horzExtent - spriteBounds.localScale.x / 2.0f);
		rightBound = (float)(spriteBounds.localScale.x / 2.0f - horzExtent);
		bottomBound = (float)(vertExtent - spriteBounds.localScale.y / 2.0f);
		topBound = (float)(spriteBounds.localScale.y / 2.0f - vertExtent);
	}

	// Update is called once per frame
	void Update()
	{
		FollowPlayers();
	}

	private void LateUpdate()
	{
		this.transform.position = Vector3.Lerp(this.transform.position, ShakePosition + targetPosition, FollowLerp);
		CheckBounds();
	}

	#region FOLLOW
	public Transform player1Transform;
	protected const string PLAYER1_TAG = "Player/player1";
	protected Vector3 targetPosition;
	public float FollowLerp;

	protected Transform Player1Transform
	{
		get
		{
			if (null == player1Transform)
			{
				var go = GameObject.FindWithTag(PLAYER1_TAG);
				if (null != go)
					player1Transform = go.transform;
			}
			return player1Transform;
		}
	}

	private void FollowPlayers()
	{
		if (null != Player1Transform)
			targetPosition = Player1Transform.position;
		else
			targetPosition = new Vector3(0, 0, 0);

		targetPosition.z = -10;
	}
	#endregion

	#region BOUNDS
	private float rightBound;
	private float leftBound;
	private float topBound;
	private float bottomBound;
	private Vector3 pos;
	public Transform spriteBounds;

	private void CheckBounds()
	{

		//if (Left(this.transform, CAMERA_FACTOR_16_9 * this.GetComponent<Camera>().orthographicSize) < Left(CameraBounds, CameraBounds.localScale.x))
		//{
		//	print("CAMERA IS LEFT");
		//	var vec = new Vector3(Left(CameraBounds, CameraBounds.localScale.x), 0, 0);
		//	this.transform.position += vec;
		//}

		var pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
		pos.x = Mathf.Clamp(pos.x, leftBound, rightBound);
		pos.y = Mathf.Clamp(pos.y, bottomBound, topBound);
		transform.position = pos;
	}

	private float Left(Transform p, float w)
	{
		return p.position.x - (p.localScale.x / 2);
	}
	#endregion

	#region SHAKE

	// How long the object should shake for.
	public float mShake;

	// Amplitude of the shake. A larger value shakes the camera harder.
	public float mShakeAmount;
	public float mDecreaseFactor = .1f;

	protected Vector3 ShakePosition;
	
    public void Shake(float shake, float amount)
    {
        mShake = shake;
        mShakeAmount = amount;
        StartCoroutine(ShakeCamera());
    }

    protected IEnumerator ShakeCamera()
    {
        while (mShake > 0)
        {
            ShakePosition = Random.insideUnitSphere.normalized * mShakeAmount * Time.timeScale;
            mShake -= Time.deltaTime * mDecreaseFactor;
            if (Time.deltaTime <= 0 || mShake <= 0)
                mShake = 0;
            yield return new WaitForSeconds(0);
        }
        ShakePosition = new Vector3(0, 0, 0);
        yield return null;
    }

	#endregion
}
