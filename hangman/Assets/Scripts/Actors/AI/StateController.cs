﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public State currentState;
    public State remainState;
    public EnemyStats enemyStats;

    [HideInInspector]
    public float minTurnDistFromWallOrEdge = 0.5f;
    [HideInInspector]
    public Rigidbody2D rbody;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public int facing = 1;
    [HideInInspector]
    public Transform chaseTarget = null;
    [HideInInspector]
    public float stateTimeElapsed;

    private bool aiActive;


    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        
    }

    private void OnTriggerStay2D( Collider2D collision )
    {
        if (collision.CompareTag("PlayerHitbox"))
        {
            collision.GetComponentInParent<ActorHealth>().TakeDamage(enemyStats.attackDamage);
        }
    }

    private void Update()
    {
        if (!aiActive)
            return;

        currentState.UpdateState(this);

        if (animator != null)
            animator.SetFloat("xVelAbs", Mathf.Abs(rbody.velocity.x));
    }

    public void Turn()
    {
        facing = -facing;
        transform.localScale = new Vector3(facing, transform.localScale.y, transform.localScale.z);
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }

    public bool CheckIfCountDownElapsed( float duration )
    {
        stateTimeElapsed += Time.deltaTime;
        return stateTimeElapsed >= duration;
    }

    public void TransitionToState( State nextState )
    {
        if (nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    public void SetupAI( bool aiActivationFromAIManager )
    {
        aiActive = aiActivationFromAIManager;

        GetComponent<ActorHealth>().health = enemyStats.maxHealth;

        GameManager.instance.FindEnemies();
    }

    private void OnDrawGizmos()
    {
        if (currentState != null)
        {
            Gizmos.color = currentState.sceneGizmoColour;
            Gizmos.DrawWireSphere(transform.position, 10f);
        }
    }
}
