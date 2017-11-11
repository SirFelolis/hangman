using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Combat/Attacks/Bair")]
public class BairAttack : Attack
{
    public override bool Condition( Vector2 input, bool isGrounded, int facingDir )
    {
        return (Mathf.Round(input.x) == -1 * facingDir) && !isGrounded;
    }
}
