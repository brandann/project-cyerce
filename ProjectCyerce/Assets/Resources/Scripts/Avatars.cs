using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatars : MonoBehaviour {

    private readonly AvatarsMeta[] _meta;
    private Avatars _avartars;
	public Avatars Instance
    {
        get {
            if (null == _avartars)
                _avartars = new Avatars();
            return _avartars;
        }
        private set
        {
            _avartars = value;
        }
    }

    public Avatars()
    {
        _meta = new AvatarsMeta[5];

        _meta[0] = new AvatarsMeta(0, "Earth", "Terraverdice", "Terra", new Color(118 / 255, 178 / 255, 110 / 255), new Color(58 / 255, 146 / 255, 46 / 255), new Color(178 / 255, 210 / 255, 174 / 255));

        _meta[1] = new AvatarsMeta(1, "Air", "Kitrinaria", "Aria", new Color(255 / 255, 233 / 255, 127 / 255), new Color(195 / 255, 201 / 255, 63 / 255), new Color(255 / 255, 255 / 255, 191 / 255));

        _meta[2] = new AvatarsMeta(2, "Fire", "Eldurlaleigh", "Eldur", new Color(255 / 255, 76 / 255, 76 / 255), new Color(195 / 255, 44 / 255, 12 / 255), new Color(255 / 255, 108 / 255, 140 / 255));

        _meta[3] = new AvatarsMeta(3, "Water", "Mizunila", "Mizu", new Color(0 / 255, 148 / 255, 255 / 255), new Color(0 / 255, 116 / 255, 191 / 255), new Color(60 / 255, 180 / 255, 255 / 255));

        _meta[4] = new AvatarsMeta(4, "Spirit", "Lillageesi", "Lilla", new Color(180 / 255, 95 / 255, 191 / 255), new Color(120 / 255, 63 / 255, 127 / 255), new Color(240 / 255, 127 / 255, 255 / 255));

    }

    public AvatarsMeta[] GetAvatarsMeta()
    {
        return _meta;
    }

    public AvatarsMeta GetAvatarsMeta(int index)
    {
        return _meta[index];
    }

}
