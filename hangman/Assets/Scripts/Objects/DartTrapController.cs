using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartTrapController : MonoBehaviour
{
    [SerializeField]
    private GameObject dartPrefab;

    [SerializeField]
    private bool leftFacing = true;

    [SerializeField]
    private float shootTime = 0.5f;

    private float shootTimeLeft = 0;

    //    [SerializeField]
    //    private bool startShooting = true;

    [SerializeField]
    private float dartLiveTime = 5;

    private void FixedUpdate()
    {
        if (shootTimeLeft > 0)
        {
            shootTimeLeft -= Time.deltaTime;
        }
        else
        {
            var i = Instantiate(dartPrefab, transform.position, Quaternion.identity);
            if (leftFacing)
            {
                i.GetComponent<ConstantAppliedForce2D>().force = new Vector2(-12, 0);
                i.GetComponent<SpriteRenderer>().flipX = true;
                Destroy(i, dartLiveTime);
            }
            else
            {
                i.GetComponent<ConstantAppliedForce2D>().force = new Vector2(12, 0);
            }

            shootTimeLeft = shootTime;
        }
    }

}
