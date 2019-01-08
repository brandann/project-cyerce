using UnityEngine;

public class Boost : MonoBehaviour {

	private const int ORTH = 20;
	private const int DIAG = 14;
	private const int OFF = 0;

	public enum BoostDir { North = 0, NorthEast = 1, East = 2, SouthEast = 3, South = 4, SouthWest = 5, West = 6, NorthWest = 7}
	private Vector2[] BoostList = {
		new Vector2(OFF, ORTH), // NORTH
		new Vector2(DIAG, DIAG), // NORTHEAST
		new Vector2(ORTH, OFF), // EAST
		new Vector2(DIAG, -DIAG), // SOUTHEAST
		new Vector2(OFF, -ORTH), // SOUTH
		new Vector2(-DIAG, -DIAG), // SOUTHWEST
		new Vector2(-ORTH, OFF), // WEST
		new Vector2(-DIAG, DIAG), // NORTHWEST
	};

	private BoostDir _boostDirection;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag.Contains(Global.PLAYER_TAG))
		{
			var player = collision.gameObject.GetComponent<HeroMovement2D>();
			if (null != player)
				player.ApplyBoost(BoostList[(int) BoostDirection]);
		}
        else if(collision.gameObject.tag.Contains("Planet"))
        {
			var enemy = collision.gameObject.GetComponent<SpiritEnemy>();
			if (null != enemy)
				enemy.ApplyBoost(BoostList[(int)BoostDirection]);
        }
	}

	public BoostDir BoostDirection
	{
		get { return _boostDirection; }
		set
		{
			_boostDirection = value;
			this.transform.up = BoostList[(int)BoostDirection].normalized;
			// rotate
		}
		

	}
}
