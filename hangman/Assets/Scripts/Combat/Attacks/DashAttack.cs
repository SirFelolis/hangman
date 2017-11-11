using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Attacks/Dash")]
public class DashAttack : Attack
{
    public override bool Condition(Vector2 input)
    {
        return Mathf.Round(input.y) == 0 && Mathf.Round(Mathf.Abs(input.x)) > 0;
    }
}
