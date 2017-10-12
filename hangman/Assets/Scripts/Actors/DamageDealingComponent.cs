using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class DamageDealingComponent : MonoBehaviour
{
    [SerializeField]
    private int damage;

    private void OnTriggerStay2D( Collider2D collision )
    {
        if (collision.GetComponent<ActorHealth>() != null)
        {
            collision.GetComponent<ActorHealth>().TakeDamage(damage);
        }
    }
}
