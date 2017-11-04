using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private Animator animator;

    public Attack attack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger(attack.name);
            GetComponentInChildren<Hurtbox>().attack = attack;
        }
    }

    public void AttackMove()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position - Vector3.up * 16, Vector2.right * transform.localScale.x, 32f, 1 << LayerMask.NameToLayer("Ground"));

        Debug.DrawRay(transform.position, Vector2.right * transform.localScale.x * 32f);

        if (!hit)
        {
            transform.position += new Vector3(attack.moveDirection.x * transform.localScale.x, attack.moveDirection.y);
        }
        else
        {
            transform.position += new Vector3((hit.distance - 16) * transform.localScale.x, 0);
        }
    }
}
