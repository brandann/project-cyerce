using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaceEnemy : MonoBehaviour {

	private float Speed = 3.333f;
	private Vector3 Vertical = new Vector2(0, 1);
	private Vector3 Horizontal = new Vector2(1, 0);
	private Vector3 CurrentDirection = Vector3.zero;
	public enum PaceDirection { Horizontal, Vertical, Up, Down, Left, Right }



	// Use this for initialization
	void Start () {
        
	}

	void Update()
	{
		this.transform.position += (CurrentDirection * Speed * Time.smoothDeltaTime);
	}

	public void SetPaceDirection(PaceDirection dir)
	{
		switch (dir)
		{
			case PaceDirection.Horizontal:
				this.CurrentDirection = Horizontal;
				break;
			case PaceDirection.Vertical:
				this.CurrentDirection = Vertical;
				break;
            case PaceDirection.Up:
                this.CurrentDirection = Vertical;
                break;
            case PaceDirection.Down:
                this.CurrentDirection = -Vertical;
                break;
            case PaceDirection.Left:
                this.CurrentDirection = -Horizontal;
                break;
            case PaceDirection.Right:
                this.CurrentDirection = Horizontal;
                break;
		}
	}

    public Vector3 GetCurrentDirection()
    {
        return CurrentDirection;
    }


	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag.Contains(Global.PLAYER_TAG))
		{
			collision.gameObject.SendMessage("kill", this.gameObject.tag);
            CurrentDirection *= -1;
			StartCoroutine(PauseBounce());
		}
		else if (collision.gameObject.tag.Contains("Wall"))
		{
			CurrentDirection *= -1;
			StartCoroutine(PauseBounce());
            BurstHere(collision.gameObject.transform.position, this.transform.position);
		}
		else if (collision.gameObject.tag.Contains("Door"))
		{
			CurrentDirection *= -1;
			StartCoroutine(PauseBounce());
			BurstHere(collision.gameObject.transform.position, this.transform.position);
		}
		else if (collision.gameObject.tag.Contains("LavaBlock"))
		{
			CurrentDirection *= -1;
			StartCoroutine(PauseBounce());
			BurstHere(collision.gameObject.transform.position, this.transform.position);
		}
        else if (collision.gameObject.tag.Contains("Planet"))
        {
            collision.gameObject.GetComponent<SpiritEnemy>().RelfectMyDirection(this.gameObject);
			BurstHere(collision.gameObject.transform.position, this.transform.position);
		}
	}

	IEnumerator PauseBounce()
	{
		var tempSpeed = Speed;
		Speed = 0;
		yield return new WaitForSeconds(.01f);
		Speed = tempSpeed;
		yield return null;
	}

    public GameObject BurstPrefab;
    private int BurstCount = 4;
    private Color BurstColor = new Color(33/255f, 174/255f, 75/255f, 255/255f);
    private float BurstSize = 1;
    private void BurstHere(Vector3 pos1, Vector3 pos2)
    {
        print("Pace Burst");
        var pos = (pos1 + pos2) / 2;
		var go = Instantiate(BurstPrefab);
		go.GetComponent<BurstManager>().MakeBurst(BurstCount, BurstColor, pos, BurstSize, global::BurstManager.SpriteTextures.Circle);
	}
}
