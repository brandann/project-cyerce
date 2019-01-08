using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyBehavior : MonoBehaviour {

    public enum Keys {Red, Orange, Yellow, Green, Blue, Purple}
    public Keys mCurrentKey;

    private const float _135 = 0.53f;
    private const float _080 = 0.31f;
    private const float _255 = 1f;
    private const float _000 = 0f;
    //private Image KeyIcon;
    //public Sprite GotKeyImage;
    //public Sprite InactiveKeyImage;

    // RED | ORANGE | YELLOW | GREEN | BLUE | PURPLE
    public static Color RedColor = new Color(_255, _000, _080);
    public static Color OrangeColor = new Color(_255, _135, _000);
    public static Color YellowColor = new Color(_255, _255, _135);
    public static Color GreenColor = new Color(_135, _255, _000);
    public static Color BlueColor = new Color(_000, _135, _255);
    public static Color PurpleColor = new Color(_135, _000, _255);

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D c)
	{
		if (c.gameObject.tag.Contains("Player"))
		{
            //c.gameObject.GetComponent<HeroBehavior>().PickupKey(mCurrentKey, this.transform.position);
            Global.mGlobal.TriggerOnKeyPickup(this.mCurrentKey);
			Destroy(this.gameObject);
		}
	}
}
