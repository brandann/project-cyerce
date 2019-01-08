using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

    private LineRenderer lr;
	private const float LASER_DISTANCE = 5;

    public GameObject BurstPrefab;
    private Color burstColor = new Color(255 / 255f, 116 / 255f, 2 / 255f, 255 / 255f);
    private int BurstSkipCount = 0;
    public int BurstSkipDurtion;
    public float BurstSize;
    public int BurstCount;

	// Use this for initialization
	void Start () {
        lr = this.gameObject.GetComponent<LineRenderer>();
	}

	private void FixedUpdate()
	{
		lr.SetPosition(0, transform.position);
		//RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up);
        var hits = Physics2D.RaycastAll(transform.position, transform.up);
        bool EndHit = false;
        foreach (var h in hits)
        {
			if (h.collider.gameObject.tag.Contains(Global.PLAYER_TAG))
			{
				h.collider.gameObject.SendMessage("kill", this.gameObject.tag);
				//TODO: kill player
				lr.SetPosition(1, h.point);
                EndHit = true;
			}
			else if (h.collider.gameObject.tag.Contains("Wall"))
			{
                lr.SetPosition(1, h.point);
                EndHit = true;
			}
			else if (h.collider.gameObject.tag.Contains("Door"))
			{
				lr.SetPosition(1, h.point);
				EndHit = true;
			}

            if(EndHit)
            {
                BurstSkipCount++;
                if(BurstSkipCount >= BurstSkipDurtion)
                {
					//var go = Instantiate(BurstPrefab);
                    //var bm = go.GetComponent<BurstManager>();
					//bm.MakeBurst(BurstCount, burstColor, h.point, BurstSize, global::BurstManager.SpriteTextures.Diamond);


					BurstSkipCount = 0;
                }
                return;
            }
        }
        if(!EndHit)
		{
			lr.SetPosition(1, transform.up * LASER_DISTANCE);
		}
	}
}
