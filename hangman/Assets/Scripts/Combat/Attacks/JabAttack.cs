using UnityEngine;

[CreateAssetMenu (menuName = "Combat/Attacks/Jab")]
public class JabAttack : Attack
{
    public override bool Condition(Vector2 input)
    {
        return Mathf.Round(input.y) == 0 && Mathf.Round(Mathf.Abs(input.x)) == 0;
    }
}
