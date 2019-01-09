using UnityEngine;
using System.Collections;

public class SnakeHeadBehavior : MonoBehaviour
{
    // MOVEMENT
	private Rigidbody2D _rigidbody2D;
	private Vector2 _oldVelocity;
    private const float SPEED = 1.2f;
    private const float CHASE_SPEED = 3f;
    private const float DIST_TRACK_PLAYER = 12f;
    private const float VELOCITY_SPEED_TO_PLAYER = .2f;
    private float CurrentSpeed;

    // BODY
    public GameObject TailPrefab;
    private TailBehavior MyTailObject;
    private SpriteRenderer _spriteRenderer;

    // STATE
    private bool isChase = false;

    // PLAYER TEMP
    private const string PLAYER1_TAG = "Player/player1";
    private const string PLAYER2_TAG = "Player/player2";
    private GameObject HeroObject;

	void Start()
	{
        print("here");
		_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
		//Global.mGlobal.OnLevelEnd += MGlobal_OnLevelEnd;
		this.Init();
		CurrentSpeed = 1;
		_spriteRenderer = this.GetComponent<SpriteRenderer>();
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

    private void OnEnable()
	{
		_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
	}

	private void OnDestroy()
    {
       //Global.mGlobal.OnLevelEnd -= MGlobal_OnLevelEnd;
	}

    private void MGlobal_OnLevelEnd()
    {
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
	    Destroy(this.gameObject);
		yield return null;
	}

    private void FixedUpdate()
	{
        if (ChaseHero())
        {
			UpdateChase(true);
        }
        else
        {
			UpdateChase(false);
		}
            
		_oldVelocity = _rigidbody2D.velocity;
	}

	private void UpdateChase(bool c)
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
        _rigidbody2D.velocity = SPEED * _rigidbody2D.velocity.normalized * CurrentSpeed;
    }

    void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.tag.Contains(PLAYER1_TAG) || c.gameObject.tag.Contains(PLAYER2_TAG))
		{
            //c.gameObject.SendMessage("kill", this.gameObject.tag);
            Debug.Log("Snake Hit The Player");
		}
        else
        {
            // FOR ANYTHING ELSE, GO AHEAD AND BOUNCE THE PLANET
            _rigidbody2D.velocity = Vector2.Reflect(_oldVelocity, c.contacts[0].normal);
        }
	}

    // this is used when interacting with the pace object
    public void RelfectMyDirection(GameObject go)
    {
       //_rigidbody2D.velocity = Vector2.Reflect(_oldVelocity, go.GetComponent<PaceEnemy>().GetCurrentDirection());
    }

    private bool ChaseHero()
    {
        //TODO add player 2 tracking
        if (null == HeroObject)
            HeroObject = GameObject.FindWithTag(PLAYER1_TAG);
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
}