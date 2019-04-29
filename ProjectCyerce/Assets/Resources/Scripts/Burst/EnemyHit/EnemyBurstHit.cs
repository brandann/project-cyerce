using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBurstHit : MonoBehaviour
{
	public int BurstCount;
	public GameObject BurstPoint;

	private float BurstRange;
	private float BurstArea;

    // Start is called before the first frame update
    void Start()
    {
		BurstArea = (float) 360 / BurstCount;
		BurstRange = (float) BurstArea / 2;

		StartCoroutine(CreateBursts());
		Invoke("DestroyMe", 2f);
    }

	private void DestroyMe()
	{
		Destroy(this.gameObject);
	}

	private void MakeBurst(float Point, float Range)
	{
		var offset = Random.Range(-Range, Range);
		var SpawnDeg = Point + offset;

		var BurstPointObject = Instantiate(BurstPoint);
		BurstPointObject.transform.position = this.transform.position;
		BurstPointObject.transform.Rotate(new Vector3(0, 0, SpawnDeg));

		//BurstPointObject.transform.RotateAround(transform.position, transform.up, SpawnDeg);
	}
	

    // Update is called once per frame
    void Update()
    {
        
    }

	protected IEnumerator CreateBursts()
	{
		for (int i = 0; i < BurstCount; i++)
		{
			MakeBurst(BurstArea * i, BurstRange);
		}
		yield return null;
	}
}
