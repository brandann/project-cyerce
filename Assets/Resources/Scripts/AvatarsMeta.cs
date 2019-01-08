using UnityEngine;

public class AvatarsMeta {
    public int Index;
    public string Element;
    public string Name;
    public string Nickname;
    public Color MainColor;
    public Color DarkColor;
    public Color LightColor;

    public AvatarsMeta(int index, string element, string name, string nickname, Color main, Color dark, Color light)
    {
        Index = index;
        Element = element;
        Name = name;
        Nickname = nickname;
        MainColor = main;
        DarkColor = dark;
        LightColor = light;
    }
}
