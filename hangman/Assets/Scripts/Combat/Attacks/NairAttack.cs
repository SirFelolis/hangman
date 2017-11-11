using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Attacks/Nair")]
public class NairAttack : Attack
{
    public override bool Condition( Vector2 input, bool isGrounded, int facingDir )
    {
        return (Mathf.Round(input.y) != -1) && !isGrounded;
    }
}
