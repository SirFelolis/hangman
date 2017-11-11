using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Actions/Patrol")]
public class PatrolAction : Action
{
    public override void Act( StateController controller )
    {
        CheckRaycasts(controller);
        Patrol(controller);
    }

    private void Patrol( StateController controller )
    {
        controller.rbody.velocity = new Vector2(controller.facing * controller.enemyStats.moveSpeed, controller.rbody.velocity.y);
    }

    private static void CheckRaycasts( StateController controller )
    {
        RaycastHit2D hit1 = Physics2D.Raycast(controller.transform.position, Vector2.left * -controller.facing, 16f, 1 << LayerMask.NameToLayer("Ground"));
        RaycastHit2D hit2 = Physics2D.Raycast(controller.transform.position + new Vector3((controller.facing * 16), 0), Vector2.down, 30f, 1 << LayerMask.NameToLayer("Ground"));

        if (hit1 || !hit2)
        {
            Debug.Log("Turn");
            controller.facing = -controller.facing;
            controller.transform.localScale = new Vector3(controller.facing, controller.transform.localScale.y, controller.transform.localScale.z);
        }
    }
}
