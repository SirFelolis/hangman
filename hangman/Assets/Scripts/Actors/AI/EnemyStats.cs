using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float moveSpeed = 100f;
    public float lookRange = 32 * 5;

    public float lookRayVerticalOffset;

    public int attackDamage = 2;

    public int maxHealth = 4;

}
