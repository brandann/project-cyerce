using UnityEngine;
using System.Collections;

public class SnakeHeadBehavior : EnemyBase
{
    // MOVEMENT
	private Rigidbody2D _rigidbody2D;
	private Vector2 _oldVelocity;
    private const float CHASE_SPEED = 2f;
    private float CurrentSpeed;

    // BODY
    public GameObject TailPrefab;
    private TailBehavior MyTailObject;
    private SpriteRenderer _spriteRenderer;

    private int MAX_HEALTH = 3;
    private int DMG_TO_PLAYER_ON_COLLISION = 1;

    private int MY_PATROL_DISTANCE = 8;

	void Start()
	{
		_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        //Global.mGlobal.OnLevelEnd += MGlobal_OnLevelEnd;
        Speed = .1f;
		CurrentSpeed = Speed;
        PlayerPatrolDist = MY_PATROL_DISTANCE;
		_spriteRenderer = this.GetComponent<SpriteRenderer>();

        //_rigidbody2D.velocity = Random.insideUnitCircle.normalized * Speed;

        if (null != MyTailObject)
            return;
        var go = Instantiate(TailPrefab);
        go.transform.position = this.transform.position;
        MyTailObject = go.GetComponent<TailBehavior>();
        MyTailObject.SetParent(this.gameObject);
        SetMaxHealth(MAX_HEALTH);
        SetDamageOnCollisionWithPlayer(DMG_TO_PLAYER_ON_COLLISION);
        CurrentState = State.OFF;
        base.Init();
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

		_oldVelocity = _rigidbody2D.velocity;
	}

    private void LateUpdate()
    {
        _rigidbody2D.velocity =  _rigidbody2D.velocity.normalized * CurrentSpeed;
    }

    void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.tag.Contains(PLAYER1_TAG) || c.gameObject.tag.Contains(PLAYER2_TAG))
		{
            BumpPlayer(c.gameObject);
            Debug.Log("Snake Hit The Player");
		}
        else
        {
            // FOR ANYTHING ELSE, GO AHEAD AND BOUNCE THE PLANET
            _rigidbody2D.velocity = Vector2.Reflect(_oldVelocity, c.contacts[0].normal);
        }
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Contains("Box/"))
            _rigidbody2D.velocity = Vector2.Reflect(_oldVelocity, collision.gameObject.transform.position.normalized);
    }

    // this is used when interacting with the pace object
    public void RelfectMyDirection(GameObject go)
    {
       //_rigidbody2D.velocity = Vector2.Reflect(_oldVelocity, go.GetComponent<PaceEnemy>().GetCurrentDirection());
    }

    protected override void EnemyDeath()
    {
        Destroy(MyTailObject.gameObject);
        base.EnemyDeath();
    }

    protected override void UpdateStateOff()
    {
        Vector3 PlayerPos = new Vector3();
        GetNearestPlayerPosition(out PlayerPos);
        var dist = this.transform.position - PlayerPos;// DIST BETWEEN PLAYER AND ENEMY
        if (dist.magnitude <= PlayerPatrolDist)
        {
            CurrentState = State.CHASE;
            return;
        }

        _spriteRenderer.color = Color.black;
        CurrentSpeed = 0;
    }

    protected override void UpdateStatePatrol()
    {
        Vector3 PlayerPos = new Vector3();
        GetNearestPlayerPosition(out PlayerPos);
        var dist = this.transform.position - PlayerPos;// DIST BETWEEN PLAYER AND ENEMY
        if (dist.magnitude <= PlayerPatrolDist)
        {
            CurrentState = State.CHASE;
            return;
        }

        _spriteRenderer.color = Color.white;
        MyTailObject.SetChase(false);
        CurrentSpeed = Speed;
    }

    protected override void UpdateStateChase()
    {
        Vector3 PlayerPos = new Vector3();
        GetNearestPlayerPosition(out PlayerPos);
        var dist = this.transform.position - PlayerPos;// DIST BETWEEN PLAYER AND ENEMY
        if (dist.magnitude > PlayerPatrolDist)
        {
            CurrentState = State.PATROL;
            return;
        }

        var small_norm = dist.normalized * CHASE_SPEED; //VELOCITY_SPEED_TO_PLAYER; // 1/10TH OF THE NORMAILZED DISTANCE BETWEEN PLAYER AND PLANET
        _rigidbody2D.velocity -= new Vector2(small_norm.x, small_norm.y);

        _spriteRenderer.color = Color.red;
        MyTailObject.SetChase(true);
        CurrentSpeed = CHASE_SPEED;
    }
}