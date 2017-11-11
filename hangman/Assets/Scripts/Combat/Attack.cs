using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack : ScriptableObject
{
    new public string name = "";

    public AnimationClip animation;

    public int damage;

    public Vector2 moveDirection;

    public bool stopJump;

    public bool stopMove;

    public bool changeVelocity;

    public abstract bool Condition(Vector2 input, bool isGrounded, int facingDir);
}
