using UnityEngine;

[System.Serializable]
public class GodData
{
    public string godName;
    public Sprite godIcon;
    public Sprite godImage;
    [TextArea(2, 6)] public string godDescription;
}
