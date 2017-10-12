using UnityEngine;

public class PlayerMotor : ActorBase
{
    [SerializeField]
    private Vector2 colliderSizeStanding = new Vector2(1, 2);
    [SerializeField]
    private Vector2 colliderSizeAir = new Vector2(1, 2);

    private bool isCrouching = false;


    private void OnTriggerEnter2D( Collider2D collision )
    {
        if (collision.CompareTag("EnemyHurtbox"))
        {
            GetComponent<ActorHealth>().TakeDamage(2);
        }
    }

    private void Update()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        CheckGrounded();

        DoInteraction();

        CheckJump(input);

        if (input.y < 0 && isGrounded && Mathf.Abs(input.x) < 0.1f)
            isCrouching = true;
        else
            isCrouching = false;


        Move(input);

        UpdateColliderSize();

        UpdateAnimation(input);
    }


    // Changes the size of the collider based on if you're moving or not.
    private void UpdateColliderSize()
    {
        if (!isGrounded)
        {
            GetComponent<BoxCollider2D>().size = colliderSizeAir;
        }
        else
        {
            GetComponent<BoxCollider2D>().size = colliderSizeStanding;
        }
    }

    private void UpdateAnimation( Vector2 input )
    {
        animator.SetBool("playerCrouching", isCrouching);


        animator.SetFloat("absXInput", Mathf.Abs(input.x));
        animator.SetFloat("yVel", rb2d.velocity.y);
    }
    
    // Check if the player should be interacting with something nearby, if so do it.
    private void DoInteraction()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 96, Vector2.left, 0, 1 << LayerMask.NameToLayer("Interactable"));
        if (hit)
        {

            Interactable interactable = hit.collider.GetComponent<Interactable>();


            if (Input.GetButtonDown("Use"))
            {
                if (interactable != null)
                {
                    interactable.CheckInteraction(transform);
                }
            }
        }
    }

    // Moves the player horizontaly, very basic right now.
    private void Move( Vector2 input )
    {
        if (input.x != 0 && isGrounded) facing = (int)input.x;

        rb2d.velocity = new Vector2(input.x * moveSpeed.x, rb2d.velocity.y);

        transform.localScale = new Vector3(facing, transform.localScale.y);
    }

    // Check if the player presses the jump button.
    private void CheckJump( Vector2 input )
    {
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && (isGrounded && !isCrouching))
        {
            animator.SetTrigger("playerJump");
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpHeight);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        Vector2 colliderSize = GetComponent<BoxCollider2D>().size;

        Gizmos.DrawRay(transform.position - new Vector3((colliderSize.x / 2f) * facing, 0, 0), new Vector2(0, -(colliderSize.y / 2) - 0.05f));
        Gizmos.DrawRay(transform.position - new Vector3((colliderSize.x / 2f) * -facing, 0, 0), new Vector2(0, -(colliderSize.y / 2) - 0.05f));
    }
}