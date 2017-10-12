using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    private Animator animator;

    public Attack neutralAttack;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger(neutralAttack.name);
            GetComponentInChildren<Hurtbox>().attack = neutralAttack;
        }
    }
}
