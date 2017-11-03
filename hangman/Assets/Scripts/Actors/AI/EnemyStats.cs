using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AI/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float moveSpeed = 100f;

    public int attackDamage = 2;

    public int maxHealth = 4;

}
