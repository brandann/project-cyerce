using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailBehavior : MonoBehaviour {

    private GameObject mParent = null;
	private TailBehavior MyTailObject;
	private SpriteRenderer _spriteRenderer;
	public GameObject TailPrefab;

	private bool isChase = false;

	// Use this for initialization
	void Start () {
        //Global.mGlobal.OnLevelEnd += MGlobal_OnLevelEnd;
		_spriteRenderer = this.GetComponent<SpriteRenderer>();
	}

	private void OnEnable()
	{
		_spriteRenderer = this.GetComponent<SpriteRenderer>();
	}

	private void OnDestroy()
	{
		//Global.mGlobal.OnLevelEnd -= MGlobal_OnLevelEnd;
	}

	void MGlobal_OnLevelEnd()
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
	
	// Update is called once per frame
	void Update () {
        if (null == mParent)
            return;

        var dist = this.transform.position - mParent.transform.position;
        dist *= .075f * Time.timeScale;
        //dist *= .075f;
        this.transform.position -= dist;
	}

    public void SetParent(GameObject parent)
    {
		if (null != MyTailObject)
			return;

        this.mParent = parent;
		if (this.transform.localScale.x > .5f)
		{
			var go = Instantiate(TailPrefab);
			go.transform.position = this.transform.position;
            go.transform.localScale *= .75f;
			MyTailObject = go.GetComponent<TailBehavior>();
			MyTailObject.SetParent(this.gameObject);
		}
    }

	public void SetChase(bool chasing)
	{
		UpdateChase(chasing);
	}

	void UpdateChase(bool c)
	{
		if (c == isChase)
			return;

		isChase = c;
		_spriteRenderer.color = (isChase ? Color.red : Color.white);
		if (null != MyTailObject)
			MyTailObject.SetChase(isChase);
	}
}
