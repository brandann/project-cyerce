using UnityEngine;
using System.Collections;

public class SpiritEnemy : MonoBehaviour
{
	private Rigidbody2D _rigidbody2D;
	private Vector2 _oldVelocity;
    public GameObject TailPrefab;

    private GameObject HeroObject;

	public GameObject BurstManager;

    public GameObject mSpriteFound;
	private SpriteRenderer _spriteRenderer;

	private bool isChase = false;
	
	private const int BURST_COUNT = 8;
    private const float SPEED = 1.2f;
    private const float CHASE_SPEED = 2f;
    private const float DIST_TRACK_PLAYER = 6.5f;
    private const float VELOCITY_SPEED_TO_PLAYER = .15f;

	private float CurrentSpeed;

	private TailBehavior MyTailObject;

	void Start()
	{
		_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
		Global.mGlobal.OnLevelEnd += MGlobal_OnLevelEnd;
		this.Init();
		CurrentSpeed = 1;
		_spriteRenderer = this.GetComponent<SpriteRenderer>();
	}

	private void OnEnable()
	{
		_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
	}

	private void OnDestroy()
    {
        Global.mGlobal.OnLevelEnd -= MGlobal_OnLevelEnd;
	}

    void MGlobal_OnLevelEnd()
    {
		//Destroy(this.gameObject);
		//var go = Instantiate(BurstManager);
		//go.GetComponent<BurstManager>().MakeBurst(BURST_COUNT, Color.black, this.transform.position, this.transform.localScale.x, global::BurstManager.SpriteTextures.Circle);
		//Destroy(this.gameObject);
        StartCoroutine("FadeSprite");
	}

	IEnumerator FadeSprite()
	{
        // WAIT FOR THE MOD DURATION TO FINISH
        var sprite = this.GetComponent<SpriteRenderer>();
		while (sprite.color.a > .1)
        {
            var color = sprite.color;
            color.a *= .9f;
            sprite.color = color;
            yield return new WaitForSeconds(.1f);
        }
		var go = Instantiate(BurstManager);
		go.GetComponent<BurstManager>().MakeBurst(BURST_COUNT, Color.black, this.transform.position, this.transform.localScale.x, global::BurstManager.SpriteTextures.Circle);
		Destroy(this.gameObject);
		yield return null;
	}

    void FixedUpdate()
	{
        if (ChaseHero())
        {
			//mSpriteFound.SetActive(true);
			UpdateChase(true);
        }
        else
        {
			//mSpriteFound.SetActive(false);
			UpdateChase(false);
		}
            
		_oldVelocity = _rigidbody2D.velocity;
	}

	void UpdateChase(bool c)
	{
		if (c == isChase)
			return;

		isChase = c;
		_spriteRenderer.color = (isChase ? Color.red : Color.white);
		MyTailObject.SetChase(isChase);
		CurrentSpeed = (isChase ? CHASE_SPEED : 1);
	}

    private void LateUpdate()
    {
        _rigidbody2D.velocity = SPEED * _rigidbody2D.velocity.normalized * CurrentSpeed + BoostDirection;
    }

    void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.tag.Contains(Global.PLAYER_TAG))
		{
			c.gameObject.SendMessage("kill", this.gameObject.tag);
		}
        else
        {
            // FOR ANYTHING ELSE, GO AHEAD AND BOUNCE THE PLANET
            _rigidbody2D.velocity = Vector2.Reflect(_oldVelocity, c.contacts[0].normal);
        }
	}

    public void RelfectMyDirection(GameObject go)
    {
        _rigidbody2D.velocity = Vector2.Reflect(_oldVelocity, go.GetComponent<PaceEnemy>().GetCurrentDirection());
    }

    public void Init()
    {
        _rigidbody2D.velocity = Random.insideUnitCircle.normalized * SPEED;

		if (null != MyTailObject)
			return;
		var go = Instantiate(TailPrefab);
		go.transform.position = this.transform.position;
		MyTailObject = go.GetComponent<TailBehavior>();
		MyTailObject.SetParent(this.gameObject);
    }

    private bool ChaseHero()
    {
        if (null == HeroObject)
            HeroObject = GameObject.FindWithTag(Global.PLAYER_TAG);
        if (null == HeroObject)
            return false;

        // GOT THE HERO OBJECT!
        var dist = this.transform.position - HeroObject.transform.position; // DIST BETWEEN PLAYER AND PLANET

        if (dist.magnitude > DIST_TRACK_PLAYER)
            return false;

        var small_norm = dist.normalized * VELOCITY_SPEED_TO_PLAYER; // 1/10TH OF THE NORMAILZED DISTANCE BETWEEN PLAYER AND PLANET
        _rigidbody2D.velocity -= new Vector2(small_norm.x, small_norm.y);
        return true;
    }
    public Vector2 BoostDirection;
	private Rigidbody2D _rigidBody2d;
	private IEnumerator coroutine;
    private const float MIN_BOOST = .1f;
    private const float BOOST_DECAY_RATE = .95f;

	public void ApplyBoost(Vector2 boost)
	{
		BoostDirection = boost;
		if (null != coroutine)
			StopCoroutine(coroutine);
		coroutine = BoostRoutine();
		StartCoroutine(coroutine);
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