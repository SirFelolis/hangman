using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Attacks/Nair")]
public class NairAttack : Attack
{
    public override bool Condition( Vector2 input, bool isGrounded, int facingDir )
    {
        return (Mathf.Round(Mathf.Abs(input.y)) == 0 && Mathf.Round(Mathf.Abs(input.x)) == 0) && !isGrounded;
    }
}
