using UnityEngine;

[CreateAssetMenu(menuName = "Background/Parallax Background")]
public class ParallaxBackground : ScriptableObject
{
    public Vector2 scrollFactor = new Vector2(1, 1);

    public Vector2 offset;
}
