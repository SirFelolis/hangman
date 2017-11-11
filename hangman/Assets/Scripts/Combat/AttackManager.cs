using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private Animator animator;

    private Attack attack;

    public AttackSet attacks;

    public Collider2D hitbox;

    private bool stoppingMovement;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void CheckAttack() // TODO use a loop instead of animation events
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("playerAttack");

            foreach (var atk in attacks.moves)
            {
                if (atk.Condition(input, GetComponent<PlayerActor>().IsGrounded, (int)transform.localScale.x))
                {
                    attack = atk;
                    if (!stoppingMovement)
                        StartCoroutine(StopMovement(attack.animation.length));
                    Debug.Log(attack.name);
                }
            }

/*            if (Mathf.Round(input.y) == 0 && Mathf.Round(Mathf.Abs(input.x)) > 0) // Dash Attack
            {
                Debug.Log("Dash");
                attack = attacks.dashAttack;
            }

            if (Mathf.Round(input.y) == 0 && Mathf.Round(Mathf.Abs(input.x)) == 0) // Jab attack
            {
                attack = attacks.jab;
            }

            if (Mathf.Round(input.y) != 0 && Mathf.Round(Mathf.Abs(input.x)) == 0) // Nair attack
            {
                attack = attacks.nair;
            }*/

            Hurtbox hb = GetComponentInChildren<Hurtbox>();

            hb.attack = attack;
        }
    }

    public void AttackMove()
    {
        if (attack == null)
        {
            return;
        }

        RaycastHit2D hit1 = Physics2D.Raycast(transform.position - Vector3.up * 16, Vector2.right * transform.localScale.x, 32f, 1 << LayerMask.NameToLayer("Ground"));
        RaycastHit2D hit2 = Physics2D.Raycast(transform.position - Vector3.up * 16, Vector2.right * transform.localScale.x, 32f, 1 << LayerMask.NameToLayer("Ground"));

        if (!hit1 || !hit2)
        {
            if (attack.changeVelocity)
            {
                Rigidbody2D rb = GetComponent<Rigidbody2D>();
                rb.velocity = new Vector2(attack.moveDirection.x * transform.localScale.x, attack.moveDirection.y);
            }
            else
            {
                transform.position += new Vector3(attack.moveDirection.x * transform.localScale.x, attack.moveDirection.y);
            }
        }
        else if (!attack.changeVelocity)
        {
            transform.position += new Vector3((hit1.distance - 16) * transform.localScale.x, 0);
        }
    }

    private IEnumerator StopMovement(float time)
    {
        stoppingMovement = true;

        PlayerActor pa = GetComponent<PlayerActor>();

        pa.canJump = !attack.stopJump;
        pa.canMove = !attack.stopMove;

        hitbox.gameObject.SetActive(false);
        yield return new WaitForSeconds(time);
        hitbox.gameObject.SetActive(true);
        pa.canJump = true;
        pa.canMove = true;

        stoppingMovement = false;
    }
}
