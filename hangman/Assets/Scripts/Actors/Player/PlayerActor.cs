﻿using UnityEngine;

public class PlayerActor : ActorBase
{
    [SerializeField]
    private Vector2 colliderSizeStanding = new Vector2(1, 2);
    [SerializeField]
    private Vector2 colliderSizeAir = new Vector2(1, 2);

    private bool isCrouching = false;

    public GameObject dustKick;

    public Transform feetTransform;

    private bool lastGrounded;

    public bool canMove = true;

    public bool canJump = true;

    public bool IsGrounded
    {
        get { return isGrounded; }
        set { return; }
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if (collision.CompareTag("EnemyHurtbox"))
        {
            //            GetComponent<ActorHealth>().TakeDamage(2);
        }
    }

    private void Update()
    {
        Vector2 input = GetInput();

        CheckGrounded();

        DoInteraction();

<<<<<<< HEAD
<<<<<<< HEAD
        //        if (canJump)
        //            CheckJump(input);

//        ResizeColliderHeight();
=======
//        if (canJump)
//            CheckJump(input);
>>>>>>> parent of 169bc30... Started working on growing mechanic
=======
//        if (canJump)
//            CheckJump(input);
>>>>>>> parent of 169bc30... Started working on growing mechanic

        CheckCrouch(input);

        if (lastGrounded != isGrounded)
        {
            CreateDustKick();
        }

        Move(input);

        GetComponent<AttackManager>().CheckAttack();

<<<<<<< HEAD
<<<<<<< HEAD
//        UpdateAnimation(input);
=======
=======
>>>>>>> parent of 169bc30... Started working on growing mechanic
        UpdateColliderSize();

        UpdateAnimation(input);
>>>>>>> parent of 169bc30... Started working on growing mechanic

        lastGrounded = isGrounded;
    }

<<<<<<< HEAD
<<<<<<< HEAD
/*    private void ResizeColliderHeight() // Old code that isn't needed anymore
    {
        Vector2 spriteSize = GetComponent<SpriteRenderer>().sprite.bounds.size;
        BoxCollider2D col = GetComponent<BoxCollider2D>();

//        col.size = new Vector2(col.size.x, col.size.y + (spriteSize.y - col.size.y) / 10);
    }*/

=======
>>>>>>> parent of 169bc30... Started working on growing mechanic
=======
>>>>>>> parent of 169bc30... Started working on growing mechanic
    private static Vector2 GetInput()
    {
        return new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    private void CheckCrouch( Vector2 input )
    {
        if (input.y < 0 && isGrounded && Mathf.Abs(input.x) < 0.1f)
            isCrouching = true;
        else
            isCrouching = false;
    }

    public void CreateDustKick()
    {
        var i = Instantiate(dustKick, feetTransform.position, Quaternion.Euler(new Vector3(-90, 0, 90)));
        Destroy(i, i.GetComponent<ParticleSystem>().main.startLifetime.constant);
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
        animator.SetBool("playerGrounded", isGrounded);

        animator.SetFloat("absXInput", Mathf.Abs(input.x));
        animator.SetFloat("yVel", _rb2d.velocity.y);
        animator.SetFloat("yInput", input.y);
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
        if (canMove)
        {
            if (input.x != 0 && isGrounded) facing = (int)input.x;

            _rb2d.velocity = new Vector2(input.x * moveSpeed.x, _rb2d.velocity.y);

            transform.localScale = new Vector3(facing, transform.localScale.y);
        }
    }

    // Check if the player presses the jump button.
    private void CheckJump( Vector2 input )
    {
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) && (isGrounded && !isCrouching))
        {
            animator.SetTrigger("playerJump");
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, jumpHeight);
        }
        if ((Input.GetButtonUp("Jump") || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) && _rb2d.velocity.y > 0)
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, _rb2d.velocity.y / 1.5f);
        }

    }
}