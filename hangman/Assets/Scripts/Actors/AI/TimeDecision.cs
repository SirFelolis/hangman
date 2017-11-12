using UnityEngine;

[CreateAssetMenu(menuName = "AI/Decisions/Time Decision")]
public class TimeDecision : Decision
{
    public override bool Decide( StateController controller )
    {
        if (controller.CheckIfCountDownElapsed(4))
            return true;
        else
            return false;
    }
}
