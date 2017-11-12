using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Look")]
public class LookDecision : Decision
{
    public override bool Decide( StateController controller )
    {
        bool targetVisible = Look(controller);
        return targetVisible;
    }

    private bool Look (StateController controller)
    {
        RaycastHit2D sightHit = Physics2D.Raycast(controller.transform.position + Vector3.down * 16f, Vector2.right * controller.facing, controller.enemyStats.lookRange, 1 << LayerMask.NameToLayer("Player"));

        Debug.DrawRay(controller.transform.position + Vector3.down * 16f, Vector2.right * controller.facing * controller.enemyStats.lookRange);

        if (sightHit && sightHit.collider.CompareTag("Player"))
        {
            controller.chaseTarget = sightHit.transform;
            return true;
        }
        return false;
    }
}
