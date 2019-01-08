using UnityEngine;
using System.Collections;

public class BurstManager : MonoBehaviour {

	// PREFAB TO BURST
	// CAN BE ANYTHING THAT HAS A SPRITE AND A TRANSFORM!
	// BURST MANAGER DOES NOT KEEP TRACK OF ITS SPAWNED BURSTS
    public GameObject mBurstPrefab;
    
    // COUNTS HOW MANY BURST ARE SCEDULUED TO GO
    private float mBurstEmitterCount;
    
    // COLOR TO SET THE BURST OBJECT
    // THIS SHOULD BE OPTIONAL IN THE FUTURE BUT
    // RIGHT NOW IT HAPPENS IF YOU LIKE IT OR NOT
    private Color mColor = Color.yellow;

    private bool mBurstReady = false;

	private float mBurstScale = 1;

    public Sprite[] mSprites;
    public enum SpriteTextures { Square = 0, Heart = 1, Triangle = 2, Circle = 3, Diamond = 4};

    //----------------------
    private int _setSprite = 0;
    private SpriteRenderer _spriteRenderer;
    private BurstBehavior _burstBehavior;
	
	// MAKES A BURST OBJECT BY INSTANCIATING A GAMEOBJECT
    IEnumerator makeBurstPoint(){
		for (int i = 0; i < mBurstEmitterCount; i++)
		{
			GameObject burstGO = Instantiate(mBurstPrefab) as GameObject;
			burstGO.transform.position = this.transform.position;
			burstGO.transform.Rotate(new Vector3(0, 0, Random.Range(0, 360)));
			burstGO.GetComponent<SpriteRenderer>().color = mColor;
			burstGO.transform.localScale *= mBurstScale;
			burstGO.GetComponent<SpriteRenderer>().sprite = mSprites[_setSprite];
		}
		
		// DESTROY MYSELF WHEN I HAVE MADE ALL THE BURSTS
		this.Complete();
		yield return null;
    }

	private void Complete()
	{
		Destroy(this.gameObject);
	}

	private Sprite GetSprite(GameObject burstObject)
	{
		return burstObject.GetComponent<SpriteRenderer>().sprite;
	}

    public void MakeBurst(int burstCount, Color burstColor, Vector3 position/*, float burstInitSize -- NOT USED YET*/)
    {
		MakeBurst(burstCount, burstColor, position, 1);
	}

	public void MakeBurst(int burstCount, Color burstColor, Vector3 position, float burstScale, SpriteTextures tex = SpriteTextures.Square)
	{
		this.transform.position = position;
		mBurstEmitterCount = burstCount;
		mColor = burstColor;
		mBurstScale = burstScale;
		mBurstReady = true;
        _setSprite = (int) tex;

		StartCoroutine("makeBurstPoint");
	}
}
