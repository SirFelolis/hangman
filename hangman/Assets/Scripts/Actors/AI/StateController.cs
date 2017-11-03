using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    public State currentState;

    public EnemyStats enemyStats;

    [HideInInspector]
    public float minTurnDistFromWallOrEdge = 0.5f;
    [HideInInspector]
    public Rigidbody2D rbody;

    private bool aiActive;

    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!aiActive)
            return;

        currentState.UpdateState(this);
    }

    public void SetupAI( bool aiActivationFromAIManager )
    {
        aiActive = aiActivationFromAIManager;
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
