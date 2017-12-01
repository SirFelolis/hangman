using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IK : MonoBehaviour
{
    public Transform target;

    public GameObject[] bones;

    private void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector2 targetDir = transform.InverseTransformPoint(target.position);
        float angle = (Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg) + 90f;
        transform.Rotate(0, 0, angle);
    }
}
