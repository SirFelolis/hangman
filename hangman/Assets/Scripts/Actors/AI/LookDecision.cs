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
        bool canSeePlayer = false;

        Transform pc = null;

        for (int i = 0; i < 3; i++)
        {
            Debug.DrawRay(
                controller.transform.position + (Vector3.down * (controller.GetComponent<BoxCollider2D>().size.y / 2 - (i * controller.enemyStats.lookRayVerticalOffset))),
                Vector2.right * controller.facing * controller.enemyStats.lookRange);

            RaycastHit2D sightHit = Physics2D.Raycast(
                controller.transform.position + Vector3.down * 16f,
                Vector2.right * controller.facing, controller.enemyStats.lookRange,
                1 << LayerMask.NameToLayer("Player"));

            if (sightHit && sightHit.collider.CompareTag("Player"))
            {
                pc = sightHit.transform;
                canSeePlayer = true;
            }
        }

        if (canSeePlayer)
        {
            controller.chaseTarget = pc;
            return true;
        }


        return false;
    }
}
