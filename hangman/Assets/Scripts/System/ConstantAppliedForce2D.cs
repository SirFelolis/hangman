using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ConstantAppliedForce2D : MonoBehaviour
{
    private Rigidbody2D rb2d;

    public Vector2 force;

    public Vector2 relativeForce;

    public float torque;

    public ForceMode2D forceMode;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (forceMode == ForceMode2D.Impulse)
        {
            rb2d.AddForce(force, forceMode);
            rb2d.AddRelativeForce(relativeForce, forceMode);
            rb2d.AddTorque(torque, forceMode);
        }
    }

    private void FixedUpdate()
    {
        if (forceMode == ForceMode2D.Force)
        {
            rb2d.AddForce(force, forceMode);
            rb2d.AddRelativeForce(relativeForce, forceMode);
            rb2d.AddTorque(torque, forceMode);
        }
    }
}
