using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public abstract class TriggerBase : MonoBehaviour
{
    protected BoxCollider2D trigger;

    protected void Awake()
    {
        trigger = GetComponent<BoxCollider2D>();
    }
}
