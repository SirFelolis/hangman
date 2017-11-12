using UnityEngine;

[CreateAssetMenu(menuName = "AI/Actions/Stand")]
public class GuardedAction : Action
{
    public override void Act( StateController controller )
    {
        Guard(controller);
    }

    private static void Guard( StateController controller )
    {
        if (Random.Range(0, 1000) <= 1)
        {
            controller.Turn();
        }
    }
}
