using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Attacks/Dash")]
public class DashAttack : Attack
{
    public override bool Condition( Vector2 input, bool isGrounded )
    {
        return (Mathf.Round(input.y) == 0 && Mathf.Round(Mathf.Abs(input.x)) > 0) && isGrounded;
    }
}
