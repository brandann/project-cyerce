using System.Collections;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class HeroMovement2D : MonoBehaviour {

	// SPEED
	private float SpeedPC = 10;
	private float SpeedAndroid = 30;
	private float TouchSpeed = 1.3f;
	private float JoystickSpeed = 6.666f;
	private float SpeedModifier = 1;

	// VELOCITY
	private Vector3 mVelocity;
	public Vector2 BoostDirection;

	// CONSTS
	private const float BOOST_DECAY_RATE = .95f;
	private const float MIN_BOOST = .1f;

	// REFRENCES
	private Rigidbody2D _rigidBody2d;
	private IEnumerator coroutine;

	// Use this for initialization
	void Start () {
		mVelocity = new Vector3(0, 0, 0);
		_rigidBody2d = this.gameObject.GetComponent<Rigidbody2D>();
		this.transform.position = Global.mGlobal.StartingPosition;
		Global.mGlobal.OnDeath += MGlobal_OnDeath;
		_rigidBody2d.gravityScale = 0;
    }

	private void OnDestroy()
	{
		Global.mGlobal.OnDeath -= MGlobal_OnDeath; 
	}

	private void MGlobal_OnDeath()
	{
		_rigidBody2d.velocity = Vector2.zero;
		SpeedModifier = 0;
	}

	public void TogglePlayerOnOff(bool on)
	{
		//Debug.Log("Toggle: " + on);
		//_rigidBody2d.gravityScale = 1 * (on ? 1 : 0);
	}
	
	// Update is called once per frame
	void Update () {
#if UNITY_ANDROID
		if (SaveLoad.SavedGame.FollowMouseEnabled)
		{
			Move_Joystick();
		}
		else
		{
			Move_Tilt();
		}
#else
        if(SaveLoad.SavedGame.FollowMouseEnabled)
        {
			Move_Mouse();
        }
        else
        {
			Move_Keyboard();
        }
#endif
	}

	private void LateUpdate()
	{
		_rigidBody2d.velocity += BoostDirection;
		_rigidBody2d.velocity *= SpeedModifier;
	}

	// ------------------------------------------------
	// MOVE THE PLAYER VIA THE KEYBOARD ARROWS/WASD
	private void Move_Keyboard()
	{
		// GET THE VELOCITY FROM INPUT AND ANDJUST IT TO SPEEDS
		if (Input.GetKey(KeyCode.UpArrow))
			mVelocity.y = 1;
		else if (Input.GetKey(KeyCode.DownArrow))
			mVelocity.y = -1;
		else
		{
			mVelocity.y = 0;
			var y = Input.GetAxis("Vertical");
			mVelocity.y = y;
		}

		if (Input.GetKey(KeyCode.RightArrow))
			mVelocity.x = 1;
		else if (Input.GetKey(KeyCode.LeftArrow))
			mVelocity.x = -1;
		else
		{
			mVelocity.x = 0;
			var x = Input.GetAxis("Horizontal");
			mVelocity.x = x;
		}

		_rigidBody2d.velocity = (mVelocity.normalized * mVelocity.magnitude * SpeedPC);
	}

	// ------------------------------------------------
	// MOVE THE PLAYER TOWARDS THE MOUSE CLICK LOCATION
	private void Move_Mouse()
	{
		if (Input.GetMouseButton(0))
		{
			// GET MOUSE POSITION
			Vector3 pos = Input.mousePosition;
			pos.z = 20;
			pos = Camera.main.ScreenToWorldPoint(pos);
			pos.z = 0;

			mVelocity = pos - this.transform.position;
		}
		else
		{
			mVelocity = mVelocity * .9f;
		}
		_rigidBody2d.velocity = (mVelocity.normalized * mVelocity.magnitude * 2);
	}

	// ------------------------------------------------
	// MOVE THE PLAYER TOWARDS THE MOUSE CLICK LOCATION
	private void Move_Touch()
	{
		if (Input.touchCount > 1)
			return;
		if (Input.GetMouseButton(0))
		{
			// GET MOUSE POSITION
			Vector3 pos = Input.mousePosition;
			pos.z = 20;
			pos = Camera.main.ScreenToWorldPoint(pos);
			pos.z = 0;

			mVelocity = pos - this.transform.position;
		}
		else
		{
			mVelocity = mVelocity * .9f;
		}
		_rigidBody2d.velocity = (mVelocity.normalized * mVelocity.magnitude) * JoystickSpeed;
	}

	private void Move_Joystick()
	{
		var y = CrossPlatformInputManager.GetAxis("Vertical");
		var x = CrossPlatformInputManager.GetAxis("Horizontal");

        var velocity = new Vector2(x, y) * JoystickSpeed;
        if (velocity.magnitude == 0)
            _rigidBody2d.velocity *= 0.8f;
        else
            _rigidBody2d.velocity = velocity;
	}

	// ------------------------------------------------
	// MOVE THE PLAYER IN THE DIRECTION OF THE PHONE TILT
	private void Move_Tilt()
	{
		_rigidBody2d.velocity = new Vector2(Input.acceleration.x, Input.acceleration.y) * SpeedAndroid;
	}

	public void ApplyBoost(Vector2 boost)
	{
        _rigidBody2d.AddForce(boost);
		//BoostDirection = boost;
		//if (null != coroutine)
		//	StopCoroutine(coroutine);
		//coroutine = BoostRoutine();
		//StartCoroutine(coroutine);
	}

	IEnumerator BoostRoutine()
	{
		// WAIT FOR THE MOD DURATION TO FINISH
		while (BoostDirection.magnitude >= MIN_BOOST)
		{
			yield return new WaitForSeconds(Time.fixedDeltaTime);
			BoostDirection *= BOOST_DECAY_RATE;
		}
		BoostDirection = Vector2.zero;
		yield return null;
	}
}
