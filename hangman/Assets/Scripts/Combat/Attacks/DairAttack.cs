using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Attacks/Dair")]
public class DairAttack : Attack
{
    public override bool Condition( Vector2 input, bool isGrounded, int facingDir )
    {
        Debug.Log((Mathf.Round(input.y) == -1 && !isGrounded));
        return (Mathf.Round(input.y) == -1 && !isGrounded);
    }
}
