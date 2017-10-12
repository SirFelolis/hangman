using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurtbox : MonoBehaviour
{
    public Attack attack;


    private void OnTriggerStay2D( Collider2D collision )
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<ActorHealth>().TakeDamage(attack.damage);
            Debug.Log("Damaging enemy");
        }
    }
}
