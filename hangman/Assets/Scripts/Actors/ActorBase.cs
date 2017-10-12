using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ActorBase : MonoBehaviour
{
    protected Animator animator;

    [SerializeField]
    private int startingHealth = 4;

    [SerializeField]
    protected Vector2 moveSpeed = new Vector2(1, 1);

    [SerializeField]
    protected float jumpHeight;

    [SerializeField]
    protected LayerMask whatIsGround;

    protected int facing = 1;

    protected bool isGrounded = true;

    protected Rigidbody2D rb2d = null;

    protected virtual void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Checks if the character is on the ground and updates the isGrounded variable.
    protected void CheckGrounded()
    {
        RaycastHit2D hit1 = Physics2D.Raycast(
            transform.position - new Vector3((GetComponent<BoxCollider2D>().size.x / 2f) * facing, 0, 0),
            Vector2.down, GetComponent<BoxCollider2D>().size.y / 2 + 0.05f,
            whatIsGround);

        RaycastHit2D hit2 = Physics2D.Raycast(
            transform.position - new Vector3((GetComponent<BoxCollider2D>().size.x / 2f) * -facing, 0, 0),
            Vector2.down, GetComponent<BoxCollider2D>().size.y / 2 + 0.05f,
            whatIsGround);

        if (hit1 || hit2)
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
    
    // Reset the actor. Used on respawn.
    public virtual void Reset()
    {
        GetComponent<ActorHealth>().health = startingHealth;
    }
}