using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyIcon : MonoBehaviour {

    public Sprite ActiveKeyImage;
    public Sprite InactiveKeyImage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetKeyActive(bool active)
    {
        var sprite = this.GetComponent<Image>();
        if (active)
            sprite.sprite = ActiveKeyImage;
        else
            sprite.sprite = InactiveKeyImage;
    }
}
