using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorHealth : MonoBehaviour
{
    public int health = 16;
    //private int maxHealth;

    // Health variable of previous frame;
    private int lastHealth;

    [SerializeField]
    private float invincibleTime = 1;

    private bool invincible = false;

    private SpriteRenderer spriteRend;

    public GameObject hitbox;

    private void Awake()
    {
        spriteRend = GetComponent<SpriteRenderer>();
    }

    //private void Start()
    //{
    //    maxHealth = health;
    //}

    public void TakeDamage( int damage )
    {
//        if (invincible)
//            return;

        health -= damage;

        if (health <= 0)
        {
            Debug.Log(gameObject.name + " has died.");
            KillMe(gameObject);
        }
    }

    private void LateUpdate()
    {
        if (lastHealth != health)
        {
            if (!invincible)
                StartCoroutine(InvincibleTime());
            ShowHealth(2);
        }

        lastHealth = health;

        //      Debug.Log(health);
        if (invincible && gameObject.CompareTag("Player"))
        {
            spriteRend.enabled = !spriteRend.enabled;
        }
        else if (gameObject.CompareTag("Player"))
        {
            if (!spriteRend.enabled)
                spriteRend.enabled = true;
        }
    }

    public IEnumerator InvincibleTime()
    {
        invincible = true;
        hitbox.SetActive(false);
        yield return new WaitForSeconds(invincibleTime);
        hitbox.SetActive(true);
        invincible = false;
    }

    /// <summary>
    /// Shows health for actor in the form of hearts.
    /// </summary>
    /// <param name="time">Time health should be displayed in seconds.</param>
    private void ShowHealth( float time )
    {
        //TODO: Add functionality later
    }

    /// <summary>
    /// Use this function to "kill" an actor.
    /// </summary>
    /// <param name="obj">the GameObject which should be killed</param>
    public static void KillMe( GameObject obj )
    {
        Debug.Log("Destroying object: " + obj.name);
        //        obj.SetActive(false);
        Destroy(obj);
    }
}
