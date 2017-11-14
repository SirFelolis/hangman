using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Chase")]
public class ChaseAction : Action
{
    public override void Act( StateController controller )
    {
        Chase(controller);
    }

    private static void Chase( StateController controller )
    {
        GameObject pc = GameObject.FindGameObjectWithTag("Player");
        Vector2 targetDirection = (pc.transform.position - controller.transform.position).normalized;

        if (Mathf.Round(targetDirection.x) != controller.facing && Mathf.Round(targetDirection.x) != 0)
            controller.Turn();

        controller.rbody.velocity = targetDirection * controller.enemyStats.moveSpeed;
    }
}
