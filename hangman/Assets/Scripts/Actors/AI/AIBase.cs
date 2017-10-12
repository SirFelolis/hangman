using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBase : ActorBase
{
    [SerializeField]
    private bool startLeft = false;

    protected override void Awake()
    {
        base.Awake();

        if (startLeft)
        {
            facing = -1;
        }

        transform.localScale = new Vector3(facing, transform.localScale.y);
        GameManager.instance.globalListEnemies.Add(gameObject);
    }

    private void OnDestroy()
    {
        GameManager.instance.globalListEnemies.Remove(gameObject);
    }

    private void FixedUpdate()
    {
        Vector2 input = new Vector2(facing, 0);

        CheckGrounded();

        TurnOnEdgeOrWall();

        UpdateAnimation(input);

        rb2d.velocity = new Vector2(input.x * moveSpeed.x, rb2d.velocity.y);
    }

    public override void Reset()
    {
        base.Reset();

        if (startLeft)
            facing = -1;
        else
            facing = 1;

        transform.localScale = new Vector3(facing, transform.localScale.y);
    }

    Vector2 FollowPlayer(Vector2 playerPosition)
    {
        return (Vector2)transform.position - playerPosition;
    }

    private void UpdateAnimation( Vector2 input )
    {
        animator.SetBool("isGrounded", isGrounded);

        if (Mathf.Abs(input.x) > 0)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

    }

    private void TurnOnEdgeOrWall()
    {
        Vector2 colliderSize = GetComponent<BoxCollider2D>().size;
        RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Vector2.right * facing, colliderSize.x / 2 + 1, whatIsGround);
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position + new Vector3(colliderSize.x / 2 * facing, 0), Vector2.down, colliderSize.y / 2 + 1, whatIsGround);
        if (isGrounded && (hit1 || !hit2))
        {
            facing *= -1;
            transform.localScale = new Vector3(facing, transform.localScale.y);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        Vector2 colliderSize = GetComponent<BoxCollider2D>().size;

        Gizmos.DrawRay(transform.position, (Vector2.right * facing) * (colliderSize.x / 2 + 1));
        Gizmos.DrawRay(transform.position + new Vector3(colliderSize.x / 2 * facing, 0), Vector2.down * (colliderSize.y / 2 + 1));
    }

}
